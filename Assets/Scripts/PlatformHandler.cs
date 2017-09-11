using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformHandler : MonoBehaviour
{
    [SerializeField]
    private bool exiting = false;
    [SerializeField]
    private float amount = 0.0f;
    [SerializeField]
    private float fadeTime = 3.0f;
    [SerializeField]
    private Image fader;

    void Start()
    {
#if UNITY_STANDALONE_WIN

        Cursor.visible = false;

#endif

#if UNITY_EDITOR

        Cursor.visible = false;

#endif
    }

    void Update ()
    {
        HandleExit();
        if(exiting)
        {
            amount += Time.deltaTime / fadeTime;
            fader.color = new Color(0, 0, 0, amount);
        }
    }

    // handles exiting for each platform
    void HandleExit()
    {

        // windows quits with esc
#if UNITY_STANDALONE_WIN

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            exiting = true;
        }
        if(amount >= 1.0f)
        {
            Application.Quit();
        }

#endif


        // editor quits with escape
#if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            exiting = true;
        }
        if(amount >= 1.0f)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }

#endif
    }
}
