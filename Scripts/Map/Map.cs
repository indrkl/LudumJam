using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map {
    public int XSize;
    public int YSize;

    public Map()
    {
        filled = new List<bool[]>();
    }

    public List<bool[]> filled;

    public void initiate(int X, int Y)
    {
        int currentHeight = 2;
        XSize = X;
        YSize = Y;
        int nextUpdate = Random.Range(4, 8);
        for(int i = 0; i < X; i++){
            if (i > nextUpdate)
            {
                currentHeight = Mathf.Max(2, Mathf.Min(5, currentHeight + Random.Range(-1, 2)));
                nextUpdate = i + Random.Range(1, 3);
            }
            filled.Add(new bool[Y]);
            for(int j = 1; j < Y; j++)
            {
                filled[i][j] = false;
                if (j!= 0 && j <= currentHeight && i != 0 && i != X - 1)
                    filled[i][j] = true;
            }
        }
    }
}
