using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map {
    public int XSize;
    public int YSize;
	public int bgap = 6;
	public int dgap = 2;
	public int blength = 4;
	public int dlength = 2;

	private int baseHeight = 2;
	private int bufferZone = 3;


    public Map()
    {
        filled = new List<bool[]>();
    }

    public List<bool[]> filled;

    public void initiate(int X, int Y)
    {

		int currentHeight = baseHeight;
        XSize = X;
        YSize = Y;

		// Height of the bedrock
        int nextUpdate = Random.Range(4, 8);
        for(int i = 0; i < X; i++){
            if (i > nextUpdate)
            {
				currentHeight = Mathf.Max(baseHeight, Mathf.Min(5, currentHeight + Random.Range(-1, 2)));
                nextUpdate = i + Random.Range(1, 3);
            }
            filled.Add(new bool[Y]);
            for(int j = 1; j < Y; j++)
            {
                filled[i][j] = false;
                if (j!= 0 && j <= currentHeight && i != 0 && i != X - 1)
                    filled[i][j] = true;
            }
        }

		// layer 1 platforms
		int x0 = 0;
		int x1 = 0;
		int y0 = baseHeight + bufferZone;
		int y1  = baseHeight + bufferZone;

		for (int i = 1; i < X - 1; i++) 
		{
			// generate current gap, length, and init slope
			int gap = Random.Range(bgap - dgap, bgap + dgap);
			int  length = Random.Range(blength -  dlength, blength + dlength); 

			// if j is in between the platform coordinates
			if (i >= x1 && i <= x1 + length)
			{
				filled[i][y1] = true;

				// if we have reached the x coordinate of platforms end; calculate new params
				if(i == x1 + length)
				{
					x0 = x1;
					y0 = y1;
					gap = Random.Range(bgap - dgap, bgap + dgap); 
					length = Random.Range(blength -  dlength, blength + dlength);
					x1 = x0 + length + gap;
					y1 = Mathf.Max (4, Mathf.Min (Random.Range ((x1 - x0) / 2 - y0, (x1 - x0) * 2 - y0), Y - 1));
					Debug.Log (x1 + " " + y1 + "TEST");

				}
			}
		} 
    }
}
