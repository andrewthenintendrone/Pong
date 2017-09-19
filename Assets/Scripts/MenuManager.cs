using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// used by the play button to change the level
public class MenuManager : MonoBehaviour
{
    // load a level by name
    public void loadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName, LoadSceneMode.Single);
    }

    // load a level by id
    public void loadLevel(int levelID)
    {
        SceneManager.LoadScene(levelID, LoadSceneMode.Single);
    }
}
