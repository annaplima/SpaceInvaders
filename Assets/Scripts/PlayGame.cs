using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    public GUISkin layout;
    void OnGUI()
    {
        GUI.skin = layout;
        float centerX = (Screen.width / 2)-60;
        float bottomY = Screen.height - 83; // Ajuste a posição vertical conforme necessário
        float buttonWidth = 120;
        float buttonHeight = 53;

    float buttonX = centerX - (buttonWidth / 2);

        if (GUI.Button(new Rect(centerX, bottomY, buttonWidth, buttonHeight), "PLAY GAME"))
        {
            SceneManager.LoadScene("SpaceInvaders");
        }
    }
}