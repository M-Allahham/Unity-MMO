﻿using UnityEngine;
using Mirror;

[RequireComponent(typeof(PlayerInventory))]
[DisallowMultipleComponent]
public class PlayerPetControl : NetworkBehaviourNonAlloc
{
    [Header("Components")]
    public Player player;
    public PlayerInventory inventory;

    [Header("Pet")]
    [SyncVar] GameObject _activePet;
    public Pet activePet
    {
        get { return _activePet != null  ? _activePet.GetComponent<Pet>() : null; }
        set { _activePet = value != null ? value.gameObject : null; }
    }
    // pet's destination should always be right next to player, not inside him
    // -> we use a helper property so we don't have to recalculate it each time
    // -> we offset the position by exactly 1 x bounds to the left because dogs
    //    are usually trained to walk on the left of the owner. looks natural.
    public Vector3 petDestination
    {
        get
        {
            Bounds bounds = player.collider.bounds;
            return transform.position - transform.right * bounds.size.x;
        }
    }

    // pet /////////////////////////////////////////////////////////////////////
    // helper function for command and UI
    public bool CanUnsummon()
    {
        // only while pet and owner aren't fighting
        return activePet != null &&
               (   player.state == "IDLE" ||    player.state == "MOVING") &&
               (activePet.state == "IDLE" || activePet.state == "MOVING");
    }

    [Command]
    public void CmdUnsummon()
    {
        // validate
        if (CanUnsummon())
        {
            // destroy from world. item.summoned and activePet will be null.
            NetworkServer.Destroy(activePet.gameObject);
        }
    }

    // combat //////////////////////////////////////////////////////////////////
    [Server]
    public void OnDamageDealtTo(Entity victim)
    {
        // let pet know that we attacked something
        if (activePet != null && activePet.autoAttack)
            activePet.OnAggro(victim);
    }

    [Server]
    public void OnKilledEnemy(Entity victim)
    {
        // killed a monster
        if (victim is Monster)
        {
            // give pet the experience without sharing it in party or similar,
            // but balance it
            // => AFTER player exp reward! pet can only ever level up to player
            //    level, so it's best if the player gets exp and level-ups
            //    first, then afterwards we try to level up the pet.
            if (activePet != null)
                activePet.experience.current += Experience.BalanceExpReward(((Monster)victim).rewardExperience, activePet.level.current, victim.level.current);
        }
    }

    // drag & drop /////////////////////////////////////////////////////////////
    void OnDragAndDrop_InventorySlot_NpcReviveSlot(int[] slotIndices)
    {
        // slotIndices[0] = slotFrom; slotIndices[1] = slotTo
        if (inventory.slots[slotIndices[0]].item.data is SummonableItem)
            UINpcRevive.singleton.itemIndex = slotIndices[0];
    }

    void OnDragAndClear_NpcReviveSlot(int slotIndex)
    {
        UINpcRevive.singleton.itemIndex = -1;
    }
}
