using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map {
    public int XSize;
    public int YSize;
	public int bgap = 6;
	public int dgap = 2;
	public int blength = 3;
	public int dlength = 2;

	private int baseHeight = 2;
	private int bufferZone = 4;


    public Map()
    {
    }

    public bool[][] filled;
    public bool[][] aestheticFilled;

    public void initiate(int X, int Y)
    {

		int currentHeight = baseHeight;
        XSize = X;
        YSize = Y;

		// layer 1 platforms
		int x0 = 0;
        int x1 = Random.Range(10, 15) ;
		int y0 = baseHeight + bufferZone;
		int y1  = baseHeight + bufferZone;
		int gap = Random.Range(bgap - dgap, bgap + dgap);
		int  length = Random.Range(blength -  dlength, blength + dlength); 
		int dy = 0;

		// Height of the bedrock
        int nextUpdate = Random.Range(9, 20);
        filled = new bool[X][];
        aestheticFilled = new bool[X][];
        for(int i = 0; i < X; i++){
            if (i > nextUpdate)
            {
				currentHeight = Mathf.Max(baseHeight, Mathf.Min(5, currentHeight + Random.Range(-1, 2)));
                nextUpdate = i + Random.Range(1, 7);
            }
            filled[i] = new bool[Y];
            aestheticFilled[i] = new bool[Y];
            for(int j = 1; j < Y; j++)
            {
                filled[i][j] = false;
                aestheticFilled[i][j] = false;
                if (j!= 0 && j <= currentHeight && i != 0 && i != X - 1)
                    filled[i][j] = true;
            }

			// if j is in between the platform coordinates
			if (i >= x1 && i <= x1 + length && i != 0 && i < X - 1)
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
					dy = Random.Range (1, 3);
					y1 = Mathf.Min (Mathf.Max (currentHeight + bufferZone, Random.Range (y0 - dy, y0 + dy)), Y - 2);

				}
			}
        }
    }
}
