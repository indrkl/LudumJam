using UnityEngine;
using System.Collections;

public class Aesthetic : MonoBehaviour {
    int XSize;
    int YSize;
    Sprite sprite;


    public bool fitsIn(int X, int Y, Map map)
    {
        if (X + XSize >= map.XSize || Y + YSize >= map.YSize)
            return false;
        for(int i = X; i < X + XSize; i++)
        {
            if (!map.filled[i][Y - 1])
                return false;
            for(int j = Y; j < Y + YSize; j++)
            {
                if (map.filled[i][j] || map.aestheticFilled[i][j])
                    return false;

            }
        }

        return true;
    }

    public void placeAesthetic(int X, int Y, Map map)
    {
        //Instantiate

        for (int i = X; i < X + XSize; i++)
        {
            for (int j = Y; j < Y + YSize; j++)
            {
                map.aestheticFilled[i][j] = true;
            }
        }

    }
}
