using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;

// this makes the sprites show up in the editor
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

public class PlatformText : Text
{
    public string baseText;

    protected override void Start()
    {
#if UNITY_STANDALONE_WIN

        text = baseText + "windows";

#endif

#if UNITY_ANDROID

        text = baseText + "android";

#endif

#if UNITY_EDITOR
        if(!UnityEditor.EditorApplication.isPlaying)
        {
            text = "";
        }
#endif

        base.Start();
    }
}
