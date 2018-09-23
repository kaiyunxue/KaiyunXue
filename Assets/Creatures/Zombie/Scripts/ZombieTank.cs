﻿//Programer: KevinX
//code date:9/22/2018
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZombieTank : CreatureBehavuourController
{
    protected override void Awake()
    {
        base.Awake();
        hatredCurve = ConstHatredCurve.instance.GetTankCurve();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }
	private void Start()
    {
        SetTarget(getMaxHatredObject().gameObject);
        StartCoroutine(startBehave());
        StartCoroutine(switchTarget());
    }

    IEnumerator startBehave()
    {
        yield return new WaitForSeconds(4f); //the time the mob will wait for the birth animation;
        Debug.Log("start");                                     //do something
        StartCoroutine(behaveUpdate()); //update
    }
    IEnumerator behaveUpdate()
    {
        yield return new WaitForEndOfFrame();
        //do something
        StartCoroutine(behaveUpdate());
    }


    public override IEnumerator Die()
    {
        return base.Die();
    }
    protected override IEnumerator Live()
    {
        return base.Live();
    }
    public override void SetTarget(GameObject target)
    {
        base.SetTarget(target);
    }
    protected override KOFItem getMaxHatredObject()
    {
        return base.getMaxHatredObject();
    }
    public override int GetMaxInstance()
    {
        return base.GetMaxInstance();
    }
	protected override IEnumerator switchTarget()
    {
        return base.switchTarget();
    }
}
