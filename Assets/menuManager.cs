using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{
    public void onClickQuit()
    {
        Application.Quit();
    }

    public void onClickMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
