using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct objectSpawnSettings
{
    public GameObject Object;
    public uint Number;
}

public class Spawner : MonoBehaviour
{
    public objectSpawnSettings[] options;

	// Use this for initialization
	void Start ()
    {
        foreach(objectSpawnSettings currentSettings in options)
        {
            for(int i = 0; i < currentSettings.Number; i++)
            {
                Instantiate(currentSettings.Object);
            }
        }	
	}
}
