using UnityEngine;
using Mirror;

[DisallowMultipleComponent]
public class DNA : NetworkBehaviourNonAlloc
{
    [SyncVar] public string dna = "player";

}