using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    int experience = 0;
    int level = 1;

    [SerializeField] List<UpgradeData> upgrades;
    [SerializeField] UpgradePanelManager upgradePanel;

    int TO_LEVEL_UP {
        get {
            return level * 1000;
        }
    }

    public void AddExperience(int amount) {
        experience += amount;
        CheckLevelUp();
    }

    public void CheckLevelUp() {
        if(experience >= TO_LEVEL_UP) {
            LevelUp();
        }
    }

    private void LevelUp() {
        upgradePanel.OpenPanel(GetUpgrades(3));
        experience -= TO_LEVEL_UP;
        level += 1;
    }

    public List<UpgradeData> GetUpgrades(int count) {
        List<UpgradeData> upgradeList = new List<UpgradeData>();

        if (count > upgrades.Count) {
            count = upgrades.Count;
        }

        for(int i = 0; i < count; i++) {
            upgradeList.Add(upgrades[Random.Range(0, upgrades.Count)]);
        }
        
        return upgradeList;
    }
}
