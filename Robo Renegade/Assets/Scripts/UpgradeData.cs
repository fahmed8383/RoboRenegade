using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType {
    ActiveUnlock,
    PasiveUnlock,
    BuffUnlock,
    EvolutionUnlock,
    ActiveUpgrade,
    PassiveUpgrade,
    BuffUpgrade,
    EvolutionUpgrade
}

[CreateAssetMenu]

public class UpgradeData : ScriptableObject
{
    public UpgradeType upgradeType;
    public string Name;
    public Sprite icon;
}
