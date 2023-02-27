using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvSlot : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Image icon;

    public void Set(UpgradeData upgradeData) {
        icon.sprite = upgradeData.icon;
    }
}
