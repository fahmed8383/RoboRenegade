using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanelManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    PauseManager pauseManager;

    [SerializeField] List<UpgradeButton> upgradeButtons;
    [SerializeField] List<UpgradeData> upgrades;

    private void Awake() {
        pauseManager = GetComponent<PauseManager>();
    }

    public void preOpenPanel() {
        OpenPanel(TestGetUpgrades(3));
    }

    public void OpenPanel(List<UpgradeData> upgradeDatas) {
        pauseManager.PauseGame();
        panel.SetActive(true);
        for(int i = 0; i < upgradeDatas.Count; i++) {
            upgradeButtons[i].Set(upgradeDatas[i]);
        }
    }

    public void ClosePanel() {
        pauseManager.UnPauseGame();
        panel.SetActive(false);
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
