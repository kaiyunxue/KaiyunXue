﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstHatredCurve : MonoBehaviour {
    public static ConstHatredCurve instance;
    [SerializeField]
    HatredCurveTemplate template;
    private void Awake()
    {
        instance = this;
    }
    public AnimationCurve GetTankCurve()
    {
        return template.tankCurve;
    }
    public AnimationCurve GetHeroCurve()
    {
        return template.heroCurve;
    }
    public AnimationCurve GetMobCurve()
    {
        return template.mobsCurve;
    }
}
