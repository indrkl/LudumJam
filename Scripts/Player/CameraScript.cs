using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
    public ControllerPlayer player;
    public float z = -10;
    public float y = 5;

    public float yOffSet;
	
	// Update is called once per frame
	void Update () {

        if (player.trample)
        {
            yOffSet = Mathf.Sin(Time.time*20) / 3.0f * Mathf.Pow(Mathf.Abs(player.movement) / player.speed, 3);
        }
        else yOffSet = yOffSet * 0.9f;
        this.transform.position = new Vector3(player.transform.position.x, Mathf.Min(y, player.transform.position.y) + yOffSet, z);
        this.transform.rotation = Quaternion.EulerAngles(yOffSet/17.0f, yOffSet/17.0f, yOffSet/17.0f);
	}


}
