using System;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UMA.CharacterSystem;

[Serializable]
public partial struct EquipmentInfo
{
    public string requiredCategory;
    public Transform location;
    public ScriptableItemAndAmount defaultItem;
}

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerInventory))]

public class PlayerEquipment : Equipment
{
    [Header("Components")]
    public Player player;
    public Animator animator;
    public PlayerInventory inventory;
    public DynamicCharacterAvatar avatar;

    [Header("Equipment Info")]
    public EquipmentInfo[] slotInfo = {
        new EquipmentInfo{requiredCategory="Weapon", location=null, defaultItem=new ScriptableItemAndAmount()},
        new EquipmentInfo{requiredCategory="Head", location=null, defaultItem=new ScriptableItemAndAmount()},
        new EquipmentInfo{requiredCategory="Chest", location=null, defaultItem=new ScriptableItemAndAmount()},
        new EquipmentInfo{requiredCategory="Legs", location=null, defaultItem=new ScriptableItemAndAmount()},
        new EquipmentInfo{requiredCategory="Shield", location=null, defaultItem=new ScriptableItemAndAmount()},
        new EquipmentInfo{requiredCategory="Shoulders", location=null, defaultItem=new ScriptableItemAndAmount()},
        new EquipmentInfo{requiredCategory="Hands", location=null, defaultItem=new ScriptableItemAndAmount()},
        new EquipmentInfo{requiredCategory="Feet", location=null, defaultItem=new ScriptableItemAndAmount()}
    };

    // cached SkinnedMeshRenderer bones without equipment, by name
    Dictionary<string, Transform> skinBones = new Dictionary<string, Transform>();

    private Dictionary<int, string> wardrobe = new Dictionary<int, string>();

    WeaponHolderSlot leftSlot;
    WeaponHolderSlot rightSlot;

    void Awake()
    {
        // 0 WEAPON
        wardrobe.Add(0, "AlternateHands");
        // 1 HEAD
        wardrobe.Add(1, "Helmet");
        // 2 CHEST
        wardrobe.Add(2, "Chest");
        // 3 LEGS
        wardrobe.Add(3, "Legs");
        // 4 SHIELD
        wardrobe.Add(4, "AlternateHands");
        // 5 SHOULDERS
        wardrobe.Add(5, "Shoulders");
        // 6 HANDS
        wardrobe.Add(6, "Hands");
        // 7 FEET
        wardrobe.Add(7, "Feet");

        avatar = gameObject.GetComponent<DynamicCharacterAvatar>();

        WeaponHolderSlot[] slots = GetComponentsInChildren<WeaponHolderSlot>();

        foreach (WeaponHolderSlot weaponSlot in slots) {
            if (weaponSlot.isLeft)
            {
                leftSlot = weaponSlot;
            }
            else if (weaponSlot.isRight) 
            {
                rightSlot = weaponSlot;
            }
        }

    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        for (int i = 0; i < slots.Count; ++i)
            RefreshLocation(i);
    }

    public override void OnStartClient()
    {
        // setup synclist callbacks on client. no need to update and show and
        // animate equipment on server
        slots.Callback += OnEquipmentChanged;

        // refresh all locations once (on synclist changed won't be called
        // for initial lists)
        // -> needs to happen before ProximityChecker's initial SetVis call,
        //    otherwise we get a hidden character with visible equipment
        //    (hence OnStartClient and not Start)
        for (int i = 0; i < slots.Count; ++i)
            RefreshLocation(i);
        
    }

    void OnEquipmentChanged(SyncListItemSlot.Operation op, int index, ItemSlot oldSlot, ItemSlot newSlot)
    {
        // at first, check if the item model actually changed. we don't need to
        // refresh anything if only the durability changed.
        // => this fixes a bug where attack animations were constantly being
        //    reset for no obvious reason. this happened because with durability
        //    items, any time we get attacked the equipment durability changes.
        //    this causes the OnEquipmentChanged hook to be fired, which would
        //    then refresh the location and rebind the animator, causing the
        //    animator to start at the entry state again (hence restart the
        //    animation).
        //
        // note: checking .data is enough. we don't need to check as deep as
        //       .data.model. this way we avoid the EquipmentItem cast.
        ScriptableItem oldItem = oldSlot.amount > 0 ? oldSlot.item.data : null;
        ScriptableItem newItem = newSlot.amount > 0 ? newSlot.item.data : null;
        if (oldItem != newItem)
        {
            // update the model
            RefreshLocation(index);
        }
    }

    // Update for implementation with UMA, have it add wardrobe recipes based on slot data, could potentially have weapons in hand be gameobjects tied to parent. idk
    public void RefreshLocation(int index)
    {
        ItemSlot slot = slots[index];
        EquipmentInfo info = slotInfo[index];

        // valid category and valid location? otherwise don't bother
        if (info.requiredCategory != "" && info.location != null)
        {
            // clear previous one in any case (when overwriting or clearing)
            if (info.location.childCount > 0) Destroy(info.location.GetChild(0).gameObject);

            //  valid item?
            if (slot.amount > 0)
            {
                // has a model? then set it
                EquipmentItem itemData = (EquipmentItem)slot.item.data;
                if (avatar.activeRace.name.Contains("Male"))
                {
                    if (itemData.recipeStringMale != null)
                    {
                        avatar.SetSlot(itemData.slotName, itemData.recipeStringMale);
                        avatar.BuildCharacter();
                    }
                }

                else if (avatar.activeRace.name.Contains("Female"))
                {
                    avatar.SetSlot(itemData.slotName, itemData.recipeStringFemale);
                    avatar.BuildCharacter();
                }
                else {
                    print("STINKYY");
                }

                // FemaleLongHair_Recipe
                // FemalePonyTail_Recipe

                // WARRIOR GLOVES
                // MaleGloves

                // WARRIOR HELMET
                // MaleRobe Hood

                // WARRIOR BOOTS
                // MaleRobe Shoes
                // TallShoes_Black_Recipe

                // HANDLE EQUIPMENT HERE
                //CHANGE TO THE NEW WEAPONSLOTHANDLER SCRIPT
                /*
                Transform weaponHolder = gameObject.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(2).GetChild(1).GetChild(1).GetChild(3);
                if (index == 0)
                {
                    print(itemData.recipeStringMale);
                    if (weaponHolder.childCount > 0)
                    {

                    }
                    else
                    {
                        Instantiate(itemData.modelPrefab, gameObject.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(2).GetChild(1).GetChild(1).GetChild(3).transform);
                    }
                }*/
                if (index == 0)
                    rightSlot.LoadWeaponModel(itemData);
            }
            else {
                avatar.ClearSlot(wardrobe[index]);
                avatar.BuildCharacter();
            }
        }
    }

    // swap inventory & equipment slots to equip/unequip. used in multiple places
    [Server]
    public void SwapInventoryEquip(int inventoryIndex, int equipmentIndex)
    {
        // validate: make sure that the slots actually exist in the inventory
        // and in the equipment
        if (inventory.InventoryOperationsAllowed() &&
            0 <= inventoryIndex && inventoryIndex < inventory.slots.Count &&
            0 <= equipmentIndex && equipmentIndex < slots.Count)
        {
            // item slot has to be empty (unequip) or equipabable
            ItemSlot slot = inventory.slots[inventoryIndex];
            if (slot.amount == 0 ||
                slot.item.data is EquipmentItem itemData &&
                itemData.CanEquip(player, inventoryIndex, equipmentIndex))
            {
                // swap them
                ItemSlot temp = slots[equipmentIndex];
                slots[equipmentIndex] = slot;
                inventory.slots[inventoryIndex] = temp;
            }
        }
    }

    [Command]
    public void CmdSwapInventoryEquip(int inventoryIndex, int equipmentIndex)
    {
        SwapInventoryEquip(inventoryIndex, equipmentIndex);
    }

    [Server]
    public void MergeInventoryEquip(int inventoryIndex, int equipmentIndex)
    {
        if (inventory.InventoryOperationsAllowed() &&
            0 <= inventoryIndex && inventoryIndex < inventory.slots.Count &&
            0 <= equipmentIndex && equipmentIndex < slots.Count)
        {
            // both items have to be valid
            // note: no 'is EquipmentItem' check needed because we already
            //       checked when equipping 'slotTo'.
            ItemSlot slotFrom = inventory.slots[inventoryIndex];
            ItemSlot slotTo = slots[equipmentIndex];
            if (slotFrom.amount > 0 && slotTo.amount > 0)
            {
                // make sure that items are the same type
                // note: .Equals because name AND dynamic variables matter (petLevel etc.)
                if (slotFrom.item.Equals(slotTo.item))
                {
                    // merge from -> to
                    // put as many as possible into 'To' slot
                    int put = slotTo.IncreaseAmount(slotFrom.amount);
                    slotFrom.DecreaseAmount(put);

                    // put back into the list
                    inventory.slots[inventoryIndex] = slotFrom;
                    slots[equipmentIndex] = slotTo;
                }
            }
        }
    }

    [Command]
    public void CmdMergeInventoryEquip(int equipmentIndex, int inventoryIndex)
    {
        MergeInventoryEquip(equipmentIndex, inventoryIndex);
    }

    [Command]
    public void CmdMergeEquipInventory(int equipmentIndex, int inventoryIndex)
    {
        if (inventory.InventoryOperationsAllowed() &&
            0 <= inventoryIndex && inventoryIndex < inventory.slots.Count &&
            0 <= equipmentIndex && equipmentIndex < slots.Count)
        {
            // both items have to be valid
            ItemSlot slotFrom = slots[equipmentIndex];
            ItemSlot slotTo = inventory.slots[inventoryIndex];
            if (slotFrom.amount > 0 && slotTo.amount > 0)
            {
                // make sure that items are the same type
                // note: .Equals because name AND dynamic variables matter (petLevel etc.)
                if (slotFrom.item.Equals(slotTo.item))
                {
                    // merge from -> to
                    // put as many as possible into 'To' slot
                    int put = slotTo.IncreaseAmount(slotFrom.amount);
                    slotFrom.DecreaseAmount(put);

                    // put back into the list
                    slots[equipmentIndex] = slotFrom;
                    inventory.slots[inventoryIndex] = slotTo;
                }
            }
        }
    }

    // durability //////////////////////////////////////////////////////////////
    public void OnDamageDealtTo(Entity victim)
    {
        // reduce weapon durability by one each time we attacked someone
        int weaponIndex = GetEquippedWeaponIndex();
        if (weaponIndex != -1)
        {
            ItemSlot slot = slots[weaponIndex];
            slot.item.durability = Mathf.Clamp(slot.item.durability - 1, 0, slot.item.maxDurability);
            slots[weaponIndex] = slot;
        }
    }

    public void OnReceivedDamage(Entity attacker, int damage)
    {
        // reduce durability by one in each equipped item
        for (int i = 0; i < slots.Count; ++i)
        {
            if (slots[i].amount > 0)
            {
                ItemSlot slot = slots[i];
                slot.item.durability = Mathf.Clamp(slot.item.durability - 1, 0, slot.item.maxDurability);
                slots[i] = slot;
            }
        }
    }

    // drag & drop /////////////////////////////////////////////////////////////
    void OnDragAndDrop_InventorySlot_EquipmentSlot(int[] slotIndices)
    {
        // slotIndices[0] = slotFrom; slotIndices[1] = slotTo

        // merge? check Equals because name AND dynamic variables matter (petLevel etc.)
        // => merge is important when dragging more arrows into an arrow slot!
        if (inventory.slots[slotIndices[0]].amount > 0 && slots[slotIndices[1]].amount > 0 &&
            inventory.slots[slotIndices[0]].item.Equals(slots[slotIndices[1]].item))
        {
            CmdMergeInventoryEquip(slotIndices[0], slotIndices[1]);
        }
        // swap?
        else
        {
            CmdSwapInventoryEquip(slotIndices[0], slotIndices[1]);
        }
    }

    void OnDragAndDrop_EquipmentSlot_InventorySlot(int[] slotIndices)
    {
        // slotIndices[0] = slotFrom; slotIndices[1] = slotTo

        // merge? check Equals because name AND dynamic variables matter (petLevel etc.)
        // => merge is important when dragging more arrows into an arrow slot!
        if (slots[slotIndices[0]].amount > 0 && inventory.slots[slotIndices[1]].amount > 0 &&
            slots[slotIndices[0]].item.Equals(inventory.slots[slotIndices[1]].item))
        {
            CmdMergeEquipInventory(slotIndices[0], slotIndices[1]);
        }
        // swap?
        else
        {
            CmdSwapInventoryEquip(slotIndices[1], slotIndices[0]); // reversed
        }
    }

    // validation
    void OnValidate()
    {
        // it's easy to set a default item and forget to set amount from 0 to 1
        // -> let's do this automatically.
        for (int i = 0; i < slotInfo.Length; ++i)
            if (slotInfo[i].defaultItem.item != null && slotInfo[i].defaultItem.amount == 0)
                slotInfo[i].defaultItem.amount = 1;
    }
}
