using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanelManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    PauseManager pauseManager;

    [SerializeField] List<UpgradeButton> upgradeButtons;
    List<UpgradeData> upgradeChoices;

    private void Awake()
    {
        pauseManager = GetComponent<PauseManager>();
    }

    private void Start()
    {
        HideButtons();
    }

    public void OpenPanel(List<UpgradeData> upgradeDatas) 
    {
        Clean();
        pauseManager.PauseGame();
        panel.SetActive(true);

        for (int i = 0; i < upgradeDatas.Count; i++)
        {
            upgradeButtons[i].gameObject.SetActive(true);
            upgradeButtons[i].Set(upgradeDatas[i]);
        }
        upgradeChoices = upgradeDatas;
    }

    public void Clean() 
    {
        for (int i = 0; i < upgradeButtons.Count; i++) 
        {
            upgradeButtons[i].Clean();
        }
    }

    public void Upgrade(int pressedButtonID) 
    {
        GameState.instance.playerTransform.GetComponent<Level>().Upgrade(pressedButtonID, upgradeChoices);
        ClosePanel();
    }

    public void ClosePanel()
    {
        HideButtons();

        pauseManager.UnPauseGame();
        panel.SetActive(false);
    }

    private void HideButtons()
    {
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].gameObject.SetActive(false);
        }
    }
}


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class UpgradePanelManager : MonoBehaviour
// {
//     [SerializeField] GameObject panel;
//     PauseManager pauseManager;
//     // [SerializeField] Level upgradePanel;

//     [SerializeField] List<UpgradeButton> upgradeButtons;
//     [SerializeField] List<UpgradeData> upgrades;

//     private void Awake() {
//         pauseManager = GetComponent<PauseManager>();
//     }

//     public void OpenPanel(List<UpgradeData> upgradeDatas) {
//         Clean();
//         pauseManager.PauseGame();
//         panel.SetActive(true);

//         for(int i = 0; i < upgradeDatas.Count; i++) {
//             upgradeButtons[i].gameObject.SetActive(true);
//             upgradeButtons[i].Set(upgradeDatas[i]);
//         }
//     }

//     public void Clean() {
//         for(int i = 0; i < upgradeButtons.Count; i++) {
//             upgradeButtons[i].Clean();
//         }
//     }

//     public void ClosePanel() {
//         for(int i = 0; i < upgradeButtons.Count; i++) {
//             upgradeButtons[i].gameObject.SetActive(false);
//         }
//         pauseManager.UnPauseGame();
//         panel.SetActive(false);
//     }

//     public void Upgrade(int pressedButtonID) {
//         GameState.instance.playerTransform.GetComponent<Level>().Upgrade(pressedButtonID);
//         ClosePanel();
//     }

//     // public List<UpgradeData> TestGetUpgrades(int count) {
//     //     List<UpgradeData> upgradeList = new List<UpgradeData>();

//     //     if (count > upgrades.Count) {
//     //         count = upgrades.Count;
//     //     }

//     //     for(int i = 0; i < count; i++) {
//     //         upgradeList.Add(upgrades[Random.Range(0, upgrades.Count)]);
//     //     }
        
//     //     return upgradeList;
//     // }
// }
