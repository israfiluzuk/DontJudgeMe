using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Human : Animations
{
    private Rigidbody _rb;
    public int EvilDegree;
    protected Rigidbody rb
    {
        get
        {
            if (_rb == null)
            {
                _rb = GetComponent<Rigidbody>();
            }
            return _rb;
        }
    }

    public IEnumerator PlayMixamoAnimation(AnimationType animationType)
    {
        yield return new WaitForSeconds(.3f);
        PlayAnim(animationType,.3f,true,1);
    }

    public IEnumerator PlayDefaultAnimation(AnimationType animationType)
    {
        yield return new WaitForSeconds(.3f);
        PlayAnim(animationType,.3f,false,1);
    }

    public IEnumerator Standing()
    {
        PlayAnim(AnimationType.Standing);
        yield return new WaitForSeconds(.3f);
    }
    
    public IEnumerator Sitting()
    {
        PlayAnim(AnimationType.Sitting,.3f,true);
        yield return new WaitForSeconds(.3f);
    }


    public IEnumerator ThrowHandGrenade()
    {
        yield return new WaitForSeconds(.3f);
        PlayAnim(AnimationType.ThrowHandGrenade,.38f,true,1);
        yield return new WaitForSeconds(3f);
        StartCoroutine(Standing());
    }

}
