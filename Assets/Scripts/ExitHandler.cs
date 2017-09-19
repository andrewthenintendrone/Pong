using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// requires Image component for fade effect
[RequireComponent(typeof(Image))]
public class ExitHandler : MonoBehaviour
{
    [SerializeField]
    private bool exiting = false; // is the game exiting?
    [SerializeField]
    private float fadeAmount = 0.0f; // how much of the fade is complete
    [SerializeField]
    private float fadeTime = 0.0f; // how long it takes to fade out

    // reference to our Image component
    private Image fadeImage;

    void Start()
    {
        // store our image component
        fadeImage = GetComponent<Image>();
    }

    void Update ()
    {
        // check for exit from player
        HandleExit();

        if(exiting)
        {
            // increase fadeAmount and update image alpha
            fadeAmount += Time.deltaTime / fadeTime;
            fadeImage.color = new Color(0, 0, 0, fadeAmount);
        }
    }

    // handles exiting for each platform
    void HandleExit()
    {

        // windows quits with the escape key
#if UNITY_STANDALONE_WIN

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            exiting = true;
        }
        // exit game when the fade is complete
        if (fadeAmount >= 1.0f)
        {
            Application.Quit();
        }

#endif
        
        // editor quits with the escape key
#if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            exiting = true;
        }
        // exit play mode when the fade is complete
        if(fadeAmount >= 1.0f)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }

#endif

        // on android you just close the app there is no fade effect
    }
}
