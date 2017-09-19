using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHide : MonoBehaviour
{
    // hides the mouse on windows
	void Start ()
    {
#if UNITY_STANDALONE_WIN
        Cursor.visible = false;
#endif
    }
}
