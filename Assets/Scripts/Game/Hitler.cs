using EpicToonFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitler : Human
{
    [SerializeField] GameObject sceneHitler;
    // Start is called before the first frame update
    void Start()
    {

    }

    internal IEnumerator PlaySittingAnimation()
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
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (sceneHitler.activeInHierarchy)
            {
                StartCoroutine(PlaySittingAnimation());
            }
        }
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
