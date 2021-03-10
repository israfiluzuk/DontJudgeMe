﻿using System.Collections;
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
    TurnLeft90,
    TurningRight90,
    TurnRight45,
    Crouching,
    CrouchTurnLeft90,
    CrouchingIdle,
    PistolIdle,
    Begging,
    BeggingIdle,
    Moving,
    HitlerSitting,
    BangingFist,
    SittingAndPointing,
    SittingAndAngry,

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
