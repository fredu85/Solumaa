using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagr : MonoBehaviour
{
    Player playerScript;
    GameObject player;

    public static SceneManagr instance = null;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }
    }

    public void loadnewScene(int sceneindex)
    {
        Debug.Log("Loading scene: " + sceneindex);
        SceneManager.LoadScene(sceneindex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
