using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MapDrawer : MonoBehaviour {
    public List<GameObject> mapObjects;
    public TileSet tileSet;

    public GameObject blockPrefab;
	public GameObject AestheticPrefab;
	public float aestheticsDensity = 0.9f;

	public List<Sprite> AestheticSpritesBig;
	public List<Sprite> AestheticSpritesSmall;

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
			yCoordinates = getYcoordinates(map.filled[i]);
			for (int j = 0; j < yCoordinates.Count; j++) {
				if (Random.value < aestheticsDensity / 2) {
					GameObject obj = Instantiate (AestheticPrefab);
					obj.GetComponent<SpriteRenderer> ().sprite = AestheticSpritesBig[Random.Range(0, AestheticSpritesBig.Count)];
					obj.transform.position = new Vector2 (i, yCoordinates [j]);
					mapObjects.Add (obj);
				}

				if (Random.value < aestheticsDensity) {
					GameObject obj = Instantiate (AestheticPrefab);
					obj.GetComponent<SpriteRenderer> ().sprite = AestheticSpritesSmall[Random.Range(0, AestheticSpritesSmall.Count)];
					obj.transform.position = new Vector2 (i, yCoordinates [j]);
					mapObjects.Add (obj);
				}
			}
        }

    }

	private List<int> getYcoordinates(bool[] boolMap) {
		List<int> res = new List<int>();
		for (int i = 1; i < boolMap.Length- 1; i++) {
			if (boolMap [i] == true && boolMap [i + 1] == false) {
				res.Add(i+1);
			}
		}
		return res;
	}

}
