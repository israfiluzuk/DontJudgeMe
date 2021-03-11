using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passenger : Human
{
    int random;
    // Start is called before the first frame update
    void Start()
    {
        random = Random.Range(0,3);
        print(random);
        if (random == 0)
            StartCoroutine(PlayMixamoAnimation(AnimationType.PassengerReaction1));
        else if (random == 1)
            StartCoroutine(PlayMixamoAnimation(AnimationType.PassengerReaction2));
        else if (random == 2)
            StartCoroutine(PlayMixamoAnimation(AnimationType.PassengerReaction3));
        else
            StartCoroutine(PlayMixamoAnimation(AnimationType.Standing));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
