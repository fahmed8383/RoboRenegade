using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuListener : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Map");
    }
}
