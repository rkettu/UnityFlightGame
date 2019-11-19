using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticVariables
{
    private static int points;
    private static string time;

    public static int PlayerCollectedCoins
    {
        get
        {
            return points;
        }
        set
        {
            points = value;
        }
    }

    public static string PlayerCompletionTime
    {
        get
        {
            return time;
        }
        set
        {
            time = value;
        }
    }
}
