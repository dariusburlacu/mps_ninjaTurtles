using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondWavePoints : MonoBehaviour {

    //These points represents the static points placed on the second road to the target
    public static Transform[] points;

    void Awake()
    {
        points = new Transform[transform.childCount];

        //Here we map the points lists with the associated objects placed on the map
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
}
