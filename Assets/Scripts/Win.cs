using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public GUISkin layout;
    void OnGUI()
    {
        GUI.skin = layout;
        float centerX = Screen.width / 2;
        float centerY = Screen.height / 2;

        float buttonWidth = 120;
        float buttonHeight = 53;

        float buttonX = centerX - (buttonWidth / 2);
        float buttonY = centerY - (buttonHeight / 2);

        if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "MENU"))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}