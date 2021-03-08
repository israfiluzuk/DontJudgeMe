using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AnimationType
{
    Walking,
    Standing,
    Sitting,
    ThrowHandGrenade,
    Running,
    Afraid,
    Surprised,
    SitToStand,
    TurnLeft45,
    Yelling,
}

public class AnimationReferencer : LocalSingleton<AnimationReferencer>
{

    [System.Serializable]
    public class MyAnimation
    {
        public AnimationType animationType;
        public AnimationClip animation;
    }

    public MyAnimation[] animations;
    public Avatar human;

}
