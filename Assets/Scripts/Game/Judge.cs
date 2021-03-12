using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judge : Human
{
    // Start is called before the first frame update
    void Start()
    {

    }

    internal IEnumerator JudgeHit()
    {
        PlayAnim(AnimationType.ManHitTable);
        yield return new WaitForSeconds(1);
        transform.DORotate(new Vector3(0,0,0),.1f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
