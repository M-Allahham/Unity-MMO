﻿// put all mount control code into a separate component.
// => for consistency with PetControl
// => if we need mount inventories, then we can put the Cmds in here too later
using UnityEngine;
using Mirror;

[DisallowMultipleComponent]
public class PlayerMountControl : NetworkBehaviourNonAlloc
{
    [Header("Mount")]
    public Transform meshToOffsetWhenMounted;
    public float seatOffsetY = -1;

    // 'Mount' can't be SyncVar so we use [SyncVar] GameObject and wrap it
    [SyncVar] GameObject _activeMount;
    public Mount activeMount
    {
        get { return _activeMount != null  ? _activeMount.GetComponent<Mount>() : null; }
        set { _activeMount = value != null ? value.gameObject : null; }
    }

    void LateUpdate()
    {
        // follow mount's seat position if mounted
        // (on server too, for correct collider position and calculations)
        ApplyMountSeatOffset();
    }

    public bool IsMounted()
    {
        return activeMount != null && activeMount.health.current > 0;
    }

    void ApplyMountSeatOffset()
    {
        if (meshToOffsetWhenMounted != null)
        {
            // apply seat offset if on mount (not a dead one), reset otherwise
            if (activeMount != null && activeMount.health.current > 0)
                meshToOffsetWhenMounted.transform.position = activeMount.seat.position + Vector3.up * seatOffsetY;
            else
                meshToOffsetWhenMounted.transform.localPosition = Vector3.zero;
        }
    }
}
