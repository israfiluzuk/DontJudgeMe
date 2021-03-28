using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Human : Animations
{
    private Rigidbody _rb;
    public int EvilDegree;
    public bool isHumanGuilty = false;
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
        PlayAnim(animationType,.3f,1);
    }

    public IEnumerator PlayDefaultAnimation(AnimationType animationType, float time = 1)
    {
        yield return new WaitForSeconds(.3f);
        PlayAnim(animationType,.3f, time);
    }

    public IEnumerator Standing()
    {
        PlayAnim(AnimationType.Standing);
        yield return new WaitForSeconds(.3f);
    }
    
    public IEnumerator Sitting()
    {
        PlayAnim(AnimationType.Sitting,.3f);
        yield return new WaitForSeconds(.3f);
    }


    public IEnumerator ThrowHandGrenade()
    {
        yield return new WaitForSeconds(.3f);
        PlayAnim(AnimationType.ThrowHandGrenade,.38f,1);
        yield return new WaitForSeconds(3f);
        StartCoroutine(Standing());
    }

}
