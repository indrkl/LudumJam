using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMaster : MonoBehaviour {
    public GameObject DeathObjectPrefab;
    
    public MapGenerator generator;
    public MapDrawer mapDrawer;

    public static GameMaster instance;

    public List<Level> levels;
    public Level curLevel;

    void Awake()
    {
        instance = this;
        levels = new List<Level>(Resources.LoadAll<Level>("Levels"));
    }

    void Start()
    {
        curLevel = levels[0];
        startLevel();

    }

    public void startLevel()
    {
        Map map = generator.generateMap();
        mapDrawer.UpdateMap(map);
    }
}
