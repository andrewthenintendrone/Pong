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
        for(int i = 0; i < options.Length; i++)
        {
            for(int j = 0; j < options[i].Number; j++)
            {
                Instantiate(options[i].Object);
            }
        }	
	}
}
