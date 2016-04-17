using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level : MonoBehaviour {
    public List<GameObject> enemyPrefabs;


    public GameObject getSpawn()
    {
        return enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

    }
}
