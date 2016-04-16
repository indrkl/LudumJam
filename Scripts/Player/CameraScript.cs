using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
    public ControllerPlayer player;
    public float z = -10;
    public float y = 5;
    
	
	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector3(player.transform.position.x, y, z);
	}


}
