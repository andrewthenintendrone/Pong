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

public class PlatformIcon : Image
{
    public Sprite windowsIcon;
    public Sprite androidIcon;

    protected override void Start()
    {

#if UNITY_STANDALONE_WIN

        sprite = windowsIcon;

#endif

#if UNITY_ANDROID

        sprite = androidIcon;

#endif

#if UNITY_EDITOR
        if (!UnityEditor.EditorApplication.isPlaying)
        {
            sprite = null;
        }
#endif

        base.Awake();
    }
}

