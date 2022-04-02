// Strength Attribute that grants extra health.
// IMPORTANT: SyncMode=Observers needed to show other player's health correctly!
using System;
using UnityEngine;

[DisallowMultipleComponent]
public class Finesse : PlayerAttribute, ICombatBonus
{
    // 1 point means 1% of max crit
    public float critBonusPercentPerPoint = 0.01f;

    public float GetCriticalChanceBonus() => value * .01f;

    public int GetDefenseBonus() => 0;

    public float GetBlockChanceBonus() => 0;

    public int GetDamageBonus() => 0;
}
