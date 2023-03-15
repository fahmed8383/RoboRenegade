using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    int level = 1;
    int experience = 0;
    static int passiveLevel;
    static int activeLevel;
    static int buffLevel;
    static int active2Level;
    int numAbility = 0;
    public GameObject nanobots;
    [SerializeField] UpgradePanelManager upgradePanel;

    [SerializeField] List<UpgradeData> upgrades;
    List<UpgradeData> selectedUpgrades;

    [SerializeField] List<UpgradeData> acquiredUpgrades;
    [SerializeField] List<UpgradeButton> invSlots;

    void Start()
    {
        
        nanobots.SetActive(false);
        passiveLevel = 0;
        activeLevel = 0;
        buffLevel = 0;
        active2Level = 0;
        Debug.Log(active2Level);
    }

    int TO_LEVEL_UP
    {
        get 
        {
            return level * 1000;
        }
    }

    public void AddExperience(int amount) 
    {
        experience += amount;
        CheckLevelUp();
    }

    public void Upgrade(int selectedUpgradeId, List<UpgradeData> choices)
    {
        UpgradeData upgradeData = choices[selectedUpgradeId];

        if (acquiredUpgrades == null) { acquiredUpgrades = new List<UpgradeData>(); }

        if (!acquiredUpgrades.Contains(upgradeData)) {
            addItem(upgradeData);
            numAbility += 1;
        }

        // Debug.Log("upgradeData.name = " + upgradeData.name);

        switch (upgradeData.name) {
            case "PassiveUpgrade":
                LevelUpPassive();
                // Debug.Log("passiveLevel = " + passiveLevel);
                break;
            case "ActiveUpgrade":
                activeLevel += 1;
                // Debug.Log("activeLevel = " + activeLevel);
                break;
            case "BuffUpgrade":
                buffLevel += 1;
                PlayerMovement.moveSpeed += 0.3f;
                // Debug.Log("buffLevel = " + buffLevel);
                break;
            case "Active2Upgrade":
                active2Level += 1;
                Debug.Log(active2Level);
                // Debug.Log("evolutionLevel = " + evolutionLevel);
                break;
        }

        acquiredUpgrades.Add(upgradeData);
        // upgrades.Remove(upgradeData);
    }

    public static int getPassiveLevel() {
        return passiveLevel;
    }

    public static int getActiveLevel() {
        return activeLevel;
    }

    public static int getActive2Level() {
        return active2Level;
    }

    public static int getBuffLevel() {
        return buffLevel;
    }

    public void CheckLevelUp() 
    {
        if (experience >= TO_LEVEL_UP)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        if (selectedUpgrades == null) { selectedUpgrades = new List<UpgradeData>(); }
        selectedUpgrades.Clear();
        selectedUpgrades.AddRange(GetUpgrades(3));

        upgradePanel.OpenPanel(selectedUpgrades);
        experience -= TO_LEVEL_UP;
        level += 1;
        FindObjectOfType<AudioManager>().Play("LevelUp");
    }

    public List<UpgradeData> GetUpgrades(int count) 
    {
        List<UpgradeData> upgradeList = new List<UpgradeData>();

        if (count > upgrades.Count) 
        {
            count = upgrades.Count;
        }

        for (int i = 0; i < count; i++) 
        {
            UpgradeData newUpgrade = upgrades[Random.Range(0, upgrades.Count)];
            while (upgradeList.Contains(newUpgrade)) {
                newUpgrade = upgrades[Random.Range(0, upgrades.Count)];
            }
            upgradeList.Add(newUpgrade);
        }

        return upgradeList;
    }

    public void addItem(UpgradeData ability) {
        invSlots[numAbility].Set(ability);
    }

    void LevelUpPassive()
    {
        if (passiveLevel == 0)
        {
            Debug.Log("nanobots activated");
            nanobots.SetActive(true);
        }
        else
        {
            Debug.Log("nanobots levelup");
            nanobots.GetComponent<NanobotScript>().dmg += 5;
        }
        passiveLevel += 1;
    }
}