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
        XSize = X;
        YSize = Y;
        for(int i = 0; i < X; i++){
            filled.Add(new bool[Y]);
            for(int j = 1; j < Y; j++)
            {
                filled[i][j] = false;
                if (j == 1 && i != 0 && i != X - 1)
                    filled[i][j] = true;
            }
        }
    }
}
