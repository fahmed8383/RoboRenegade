using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPanel: MonoBehaviour {
    public GameObject Panel;
    public void PanelOpener() {
        if (Panel != null) {
            bool isActive = Panel.activeSelf;
            Panel.SetActive(true);
        }
    }
}