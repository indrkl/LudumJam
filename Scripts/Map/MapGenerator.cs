﻿using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {
    public int Xsize;
    public int Ysize;

    public Map generateMap()
    {
        Map r = new Map();
        r.initiate(Xsize, Ysize);
        return r;
    }
}
