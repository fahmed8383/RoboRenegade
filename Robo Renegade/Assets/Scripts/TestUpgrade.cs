using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUpgrade : MonoBehaviour
{
    [SerializeField] List<UpgradeData> upgrades;
    [SerializeField] UpgradePanelManager upgradePanel;

    private void TestLevelUp() {
        upgradePanel.OpenPanel(TestGetUpgrades(3));
    }

    public List<UpgradeData> TestGetUpgrades(int count) {
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
