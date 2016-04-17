using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MapDrawer : MonoBehaviour {
    public List<GameObject> mapObjects;
    public TileSet tileSet;

    public GameObject blockPrefab;
	public GameObject AestheticPrefab;
	public float aestheticsDensity = 0.9f;

    public List<Aesthetic> AestheticSprites;

	private List<int> yCoordinates;


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

			// Generate random scene aesthetic objects
			for (int j = 1; j < map.YSize; j++) {
                if (map.filled[i][j])
                    continue;
                if(Random.value < aestheticsDensity)
                {
                    List<Aesthetic> options = new List<Aesthetic>();
                    int room = Aesthetic.getRoomMultiplier(i, j, map);
                    foreach(Aesthetic ae in AestheticSprites)
                    {
                        if (ae.fitsInOptimized(room))
                            options.Add(ae);
                    }
                    if(options.Count > 0)
                    {
                        Aesthetic chosen = options[Random.Range(0, options.Count)];
                        GameObject obj = Instantiate(AestheticPrefab);
                        obj.GetComponent<SpriteRenderer>().sprite = chosen.sprite;
                        obj.transform.position = new Vector2(i, j-chosen.offSet);
                        chosen.placeAesthetic(i, j, map);
                        mapObjects.Add(obj);
                    }
                }
			}
        }

    }
}
