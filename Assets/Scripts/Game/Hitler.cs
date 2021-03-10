using EpicToonFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitler : Human
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlaySittingAnimation());
    }

    IEnumerator PlaySittingAnimation()
    {
        yield return new WaitForSeconds(.4f);
        PlayAnim(AnimationType.SittingAndAngry, .3f);
        yield return new WaitForSeconds(2f);
        PlayAnim(AnimationType.SittingAndPointing, .3f);
        yield return new WaitForSeconds(2f);
        StartCoroutine(PlaySittingAnimation());
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(SecondUpdate());
    }

    IEnumerator SecondUpdate()
    {
        while (true)
        {
            EvilDegree = ETFXFireProjectile.hitlerEvilDegree;
            yield return new WaitForSeconds(1);
        }
    }
}
