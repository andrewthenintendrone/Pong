using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;

// this makes the text show up in the editor
[CustomEditor(typeof(PlatformText))]
public class PlatformTextEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        PlatformText t = (PlatformText)target;
    }
}
#endif

// PlatformText inherits from Text
public class PlatformText : Text
{
    // base text
    public string baseText;

    protected override void Start()
    {
        // set our text to the base text plus the platform name
#if UNITY_STANDALONE_WIN

        text = baseText + "windows";

#endif

#if UNITY_ANDROID

        text = baseText + "android";

#endif

#if UNITY_EDITOR

        // reset the text while in the editor and not playing
        if (!UnityEditor.EditorApplication.isPlaying)
        {
            text = "sample text";
        }
#endif

        base.Start();
    }
}
