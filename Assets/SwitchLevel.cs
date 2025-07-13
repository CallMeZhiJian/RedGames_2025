using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchLevel : MonoBehaviour
{
    // Load the main menu scene
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Load the gameplay scene
    public void GoToGameplay()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
