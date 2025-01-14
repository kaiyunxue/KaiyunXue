﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Creature
{
    [Header("HeroVal")]
    public Weapon weapon;
    public Transform head;
    public Animator animator;
    public ISkillManager skillManager;
    public StatusBox statusBox;
    public AudioController audioCtrler;
    public InputListener inputListener;
    [SerializeField]
    protected HerosRegistrar heroRegister;

    public HatredCurveTemplate hatredTemplate;

    public FrontTest test;

    public State state;
    public HerosRegistrar HeroRegister
    {
        get
        {
            return this.heroRegister;
        }
    }
    protected override void Awake()
    {
        base.Awake();
        test = new FrontTest(this);
        test.StartForntTest();
        this.hatredCurve = hatredTemplate.heroCurve;
        if (gameObject.layer == 8)
            inputListener = GameController.LeftInputListener;
        else
            inputListener = GameController.RightInputListener;
        animator = gameObject.GetComponent<Animator>();
        skillManager = gameObject.GetComponent<ISkillManager>();
        heroRegister = gameObject.GetComponent<HerosRegistrar>();
        state = gameObject.GetComponent<State>();
        audioCtrler = GetComponent<AudioController>();
    }
}
