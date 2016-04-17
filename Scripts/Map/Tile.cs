using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
    //0 - top, 1 - right, 2 - bottom, 3 - left
    public bool[] openEnd;
	public bool canRotate;

    public Sprite sprite;

    public bool fits(int X, int Y, Map map)
    {
        if (map.filled[X][Y + 1] == openEnd[0] )
        {
            //Debug.Log("TOP");
            return false;
        }
        if (map.filled[X + 1][Y] == openEnd[1])
        {
            //Debug.Log("RIGHT");
            return false;
        }
        if (map.filled[X][Y - 1] == openEnd[2])
        {
            //Debug.Log("BOTT");
            return false;
        }
        if (map.filled[X - 1][Y] == openEnd[3])
        {
            //Debug.Log("LEFTs");
            return false;
        }
        return true;
    }
}
