using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileSet : MonoBehaviour {

    public List<Tile> tiles;


    public Sprite getFittingSprite(int X, int Y, Map map)
    {
        List<Tile> options = new List<Tile>();
        foreach(Tile tile in tiles)
        {
            if (tile.fits(X, Y, map))
            {
                options.Add(tile);
            }
        }
        int r = Random.Range(0, options.Count);
        return options[r].sprite;
    }
}
