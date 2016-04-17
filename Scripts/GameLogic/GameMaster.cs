using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {
    public MapGenerator generator;
    public MapDrawer mapDrawer;

    void Start()
    {
        startLevel();

    }

    public void startLevel()
    {
        Map map = generator.generateMap();
        mapDrawer.UpdateMap(map);
    }
}
