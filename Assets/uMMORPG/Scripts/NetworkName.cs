// Synchronizing an entity's name is crucial for components that need the proper
// name in the Start function (e.g. to load the skillbar by name).
//
// Simply using OnSerialize and OnDeserialize is the easiest way to do it. Using
// a SyncVar would require Start, Hooks etc.
using UnityEngine;
using Mirror;

[DisallowMultipleComponent]
public class NetworkName : NetworkBehaviourNonAlloc
{
    // server-side serialization
    public override bool OnSerialize(NetworkWriter writer, bool initialState)
    {
        writer.WriteString(name);
        return true;
    }

    // client-side deserialization
    public override void OnDeserialize(NetworkReader reader, bool initialState)
    {
        name = reader.ReadString();
    }
}
