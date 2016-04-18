using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraScript : MonoBehaviour {
    public ControllerPlayer player;
    public float z = -10;
    public float y = 5;

    public List<GameObject> BG1;
    public List<GameObject> BG2;

    public List<float> BGLength;
    public List<float> BGSpeed;

    public float yOffSet;
    public Camera cam;
	
	// Update is called once per frame
	void Update () {
        float xPos = Screen.width;
        float yPos = Screen.height - 200;
             
        cam.pixelRect = new Rect(Screen.width - xPos, Screen.height - yPos, Screen.width, Screen.height - 200);
        if (player.trample)
        {
            yOffSet = Mathf.Sin(Time.time*20) / 3.0f * Mathf.Pow(Mathf.Abs(player.movement) / Mathf.Max(0.1f,player.speed*2), 3);
        }
        else yOffSet = yOffSet * 0.9f;
        this.transform.position = new Vector3(player.transform.position.x, Mathf.Max(y, player.transform.position.y) + yOffSet, z);
        this.transform.rotation = Quaternion.EulerAngles(yOffSet/20.0f, 4*yOffSet/23.0f, yOffSet/23.0f);

        for(int i = 0; i < BG1.Count; i++)
        {
            float curDistance = this.transform.position.x - this.transform.position.x * BGSpeed[i];
            curDistance = curDistance % BGLength[i];
            BG1[i].transform.position = new Vector3(this.transform.position.x - curDistance, BG1[i].transform.position.y, BG1[i].transform.position.z);
            BG2[i].transform.localPosition = BG1[i].transform.localPosition + new Vector3(BGLength[i], 0, 0);
        }
	}


}
