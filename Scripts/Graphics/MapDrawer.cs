using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MapDrawer : MonoBehaviour {
    public List<GameObject> mapObjects;
    public TileSet tileSet;

    public GameObject blockPrefab;


    void Awake()
    {
        mapObjects = new List<GameObject>();
    }

    public void UpdateMap(Map map)
    {
        foreach(GameObject obj in mapObjects)
        {
            Destroy(obj);
        }
        mapObjects.RemoveRange(0, mapObjects.Count);

        for(int i = 0; i < map.XSize; i++)
        {
            for(int j = 0; j < map.YSize; j++)
            {
                if (map.filled[i][j])
                {
                    GameObject obj = Instantiate(blockPrefab);
                    Tile tile = tileSet.getFittingSprite(i, j, map);
                    if (tile.canRotate)
                    {
                        int rot = Random.Range(0, 4);
                        obj.transform.rotation = Quaternion.Euler(0, 0, rot * 90);
                    }
                    obj.GetComponent<SpriteRenderer>().sprite = tile.sprite;
                    obj.transform.position = new Vector2(i, j);
                    mapObjects.Add(obj);
                }
            }
        }
    }

}
