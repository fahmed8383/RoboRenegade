using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradePanelManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    PauseManager pauseManager;

    [SerializeField] List<UpgradeButton> upgradeButtons;
    List<UpgradeData> upgradeChoices;

    [SerializeField] List<TMP_Text> upgradeTexts; 

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

            switch (upgradeDatas[i].name) {
            case "PassiveUpgrade":
                upgradeTexts[i].text = "(PASSIVE) A swarm of Nanobots attacks everything near you";
                break;
            case "ActiveUpgrade":
                upgradeTexts[i].text = "(ACTIVE) Press Q to stop all enemies for a brief moment.";
                break;
            case "BuffUpgrade":
                upgradeTexts[i].text = "(BUFF) Increase your movement speed.";
                break;
            case "Active2Upgrade":
                upgradeTexts[i].text = "(ACTIVE) Press E to shoot Lasers all around you!";
                break;
            case "ShieldUpgrade":
                upgradeTexts[i].text = "(PASSIVE) Acquire an invincible shield that appears periodically";
                break;
            }   
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