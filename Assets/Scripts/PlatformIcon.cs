using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;

// this makes the sprites show up in the editor
[CustomEditor(typeof(PlatformIcon))]
public class PlatformIconEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        PlatformIcon t = (PlatformIcon)target;
    }
}
#endif

// PlatformIcon inherits from Image
public class PlatformIcon : Image
{
    // icon to display on windows
    public Sprite windowsIcon;
    // icon to display on android
    public Sprite androidIcon;

    protected override void Start()
    {
        // change our sprite to match the platform
#if UNITY_STANDALONE_WIN

        sprite = windowsIcon;

#endif

#if UNITY_ANDROID

        sprite = androidIcon;

#endif

#if UNITY_EDITOR

        // reset the sprite while in the editor and not playing
        if (!UnityEditor.EditorApplication.isPlaying)
        {
            sprite = null;
        }

#endif

        base.Start();
    }
}

