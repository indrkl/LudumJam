using UnityEngine;
using System.Collections;

public class Aesthetic : MonoBehaviour {
    public int XSize;
    public int YSize;
    public Sprite sprite;

    public static int getRoomMultiplier(int X, int Y, Map map)
    {
        int r = 0;
        if (X == 0 || Y == 0)
            return r;
        while (true)
        {
            if (r > 5)
                return r;
            if (X + r >= map.XSize - 1)
                return r;
            if (Y + r >= map.YSize - 1)
                return r;
            for (int i = X; i <= X + r; i++)
            {
                if (map.filled[i][Y + r] || map.aestheticFilled[i][Y + r])
                    return r;
            }
            for(int j = Y; j <= Y + r; j++)
            {
                if (map.filled[X + r][j] || map.aestheticFilled[X + r][j])
                    return r;
            }
            if (!map.filled[X + r][Y - 1])
                return r;
            r++;
        }

        return r;
    }

    public bool fitsInOptimized(int room)
    {
        if (XSize <= room && YSize <= room)
            return true;
        else return false;
    }


    public bool fitsIn(int X, int Y, Map map)
    {
        if (X + XSize >= map.XSize || Y + YSize >= map.YSize || Y == 0)
            return false;
        Debug.Log(X + " " + Y);
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
