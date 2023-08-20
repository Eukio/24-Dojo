using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonManager : MonoBehaviour
{
    public void OnClickExit()
    {

    }
    public void OnClickPlay()
    {

    }
    public void OnClickReady()
    {

    }

    public void OnClickStartGame()
    {

    }
    public void OnClickMultiplayer()
    {
        SceneManager.LoadScene("Multiplayer");

    }
    public void OnClickSingleplayer()
    {
        SceneManager.LoadScene("Singleplayer");
    }
}
