﻿using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {
    public int Xsize;
    public int Ysize;
    public float enemyChance = 0.2f;

    public GameObject endPrefab;

    public Map generateMap()
    {
        Map r = new Map();
        r.initiate(Xsize, Ysize);
        fillWithEnemies(r);
        Instantiate(endPrefab, new Vector3(Xsize - 2, 0, 0), Quaternion.Euler(0, 0, 0));
        return r;
    }

    public void fillWithEnemies(Map map)
    {
        for (int j = 1; j < map.YSize - 3; j++)
        {
            int chain = 0;
            for (int i = 20; i < map.XSize - 1; i++)
            {
                if (map.filled[i][j - 1] && !map.filled[i][j])
                    chain++;
                else chain = 0;
                if (chain > 3)
                {
                    if(Random.value < enemyChance)
                    {
                        InstantiateRandomEnemy(i - 2, j);
                    }
                    chain = 0;

                }
                    
            }
        }
    }

    public void InstantiateRandomEnemy(int X, int Y)
    {
        Level l = GameMaster.instance.curLevel;
        int random = Random.Range(0, l.enemyPrefabs.Count);
        GameObject enemy = Instantiate(l.enemyPrefabs[random], new Vector3(X, Y, 0), Quaternion.EulerAngles(0, 0, 0)) as GameObject;
    }
}
