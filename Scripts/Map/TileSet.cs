using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileSet : MonoBehaviour {

    public List<Tile> tiles;


    public Sprite getFittingSprite(int X, int Y, Map map)
    {
        Debug.Log(X + " " + Y);
        List<Tile> options = new List<Tile>();
        foreach(Tile tile in tiles)
        {
            if (tile.fits(X, Y, map))
            {
                options.Add(tile);
            }
        }
        if (options.Count == 0)
        {
            Debug.Log(map.filled[X + 1][Y] + " " + map.filled[X][Y + 1] + " " + map.filled[X][Y - 1] + " " + map.filled[X - 1][Y]);
        }
        int r = Random.Range(0, options.Count);
        return options[r].sprite;
    }
}
