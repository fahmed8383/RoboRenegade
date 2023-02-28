using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    int level = 1;
    int experience = 0;
    static int passiveLevel = 0;
    static int activeLevel = 0;
    static int buffLevel = 0;
    static int active2Level = 0;
    int numAbility = 0;
    [SerializeField] UpgradePanelManager upgradePanel;

    [SerializeField] List<UpgradeData> upgrades;
    List<UpgradeData> selectedUpgrades;

    [SerializeField] List<UpgradeData> acquiredUpgrades;
    [SerializeField] List<UpgradeButton> invSlots;

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
                passiveLevel += 1;
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
            upgradeList.Add(upgrades[Random.Range(0, upgrades.Count)]);
        }

        return upgradeList;
    }

    public void addItem(UpgradeData ability) {
        invSlots[numAbility].Set(ability);
    }
}


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Level : MonoBehaviour
// {
//     int experience = 0;
//     int level = 1;

//     [SerializeField] List<UpgradeData> upgrades;
//     [SerializeField] UpgradePanelManager upgradePanel;
//     List<UpgradeData> selectedUpgrades;

//     [SerializeField] List<UpgradeData> acquiredUpgrades;

//     int TO_LEVEL_UP {
//         get {
//             return level * 1000;
//         }
//     }

//     public void AddExperience(int amount) {
//         Debug.Log("added " + amount + " exp");
//         experience += amount;
//         CheckLevelUp();
//     }

//     public void CheckLevelUp() {
//         if(experience >= TO_LEVEL_UP) {
//             LevelUp();
//         }
//     }

//     public void Upgrade(int selectedUpgradeId)
//     {
//         UpgradeData upgradeData = selectedUpgrades[selectedUpgradeId];

//         if (acquiredUpgrades == null) { acquiredUpgrades = new List<UpgradeData>(); }

//         acquiredUpgrades.Add(upgradeData);
//         upgrades.Remove(upgradeData);
//     }

//     private void LevelUp()
//     {
//         Debug.Log("LevelUp()");
//         if (selectedUpgrades == null) { selectedUpgrades = new List<UpgradeData>(); }
//         selectedUpgrades.Clear();
//         selectedUpgrades.AddRange(GetUpgrades(3));

//         upgradePanel.OpenPanel(selectedUpgrades);
//         experience -= TO_LEVEL_UP;
//         level += 1;
//     }

//     public List<UpgradeData> GetUpgrades(int count) 
//     {
//         List<UpgradeData> upgradeList = new List<UpgradeData>();

//         if (count > upgrades.Count) 
//         {
//             count = upgrades.Count;
//         }

//         for (int i = 0; i < count; i++) 
//         {
//             upgradeList.Add(upgrades[Random.Range(0, upgrades.Count)]);
//         }

//         return upgradeList;
//     }
// }