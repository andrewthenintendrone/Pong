using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// makes this struct show up in the inspector for editing
[System.Serializable]
public struct objectSpawnSettings
{
    public GameObject Object; // reference to a gameobject to spawn
    public uint Number; // number of that object to spawn
}

// spawns things into the level
public class Spawner : MonoBehaviour
{
    // list of object spawn settings
    public List<objectSpawnSettings> options = new List<objectSpawnSettings>();

	void Start ()
    {
        // instantiate all gameobjects from options
        for(int i = 0; i < options.Count; i++)
        {
            for(int j = 0; j < options[i].Number; j++)
            {
                Instantiate(options[i].Object);
            }
        }	
	}
}
