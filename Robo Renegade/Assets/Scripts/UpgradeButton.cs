using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Image icon;

    public void Set(UpgradeData upgradeData) {
        icon.sprite = upgradeData.icon;
    }

    internal void Clean() {
        icon.sprite = null;
    }
}
