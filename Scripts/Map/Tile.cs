using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
    //0 - top, 1 - right, 2 - bottom, 3 - left
    public bool[] openEnd;

    public Sprite sprite;

    public bool fits(int X, int Y, Map map)
    {
        if (map.filled[X][Y + 1] && !openEnd[0])
            return false;
        if (map.filled[X + 1][Y] && !openEnd[1])
            return false;
        if (map.filled[X][Y - 1] && !openEnd[2])
            return false;
        if (map.filled[X - 1][Y] && !openEnd[3])
            return false;
        return true;
    }
}
