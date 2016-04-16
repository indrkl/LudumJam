using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {
    MapGenerator generator;

    public void startLevel()
    {
        Map map = generator.generateMap();

    }
}
