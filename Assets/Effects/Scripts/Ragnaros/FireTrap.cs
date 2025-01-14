﻿using System;
using UnityEngine;
using System.Collections;


public class FireTrap : SkillItemsBehaviourController
{
    static int maxInstanceNum = 20;
    public RagnarosDamage damage;

    protected override void Awake()
    {
        base.Awake();
        damage = new RagnarosDamage(30, DamageType.Fire, gameObject.layer);
        flameIniPos = flame.transform.localPosition;
    }
    override protected void OnEnable()
    {
        base.OnEnable();
        time_ = 0;
        fireWave.SetActive(false);
        flame.SetActive(false);
        fts = firetrapState.Waiting;
    }

    public GameObject flame;
    public GameObject trap;
    Vector3 flameIniPos;
    public GameObject fireWave;
    float continuingtime = 8;
    enum firetrapState
    {
        Waiting,
        trapping,
        holding,
        destorying
    };
    [SerializeField]
    firetrapState fts;
    private firetrapState fts_
    {
        get
        {
            return fts;
        }

        set
        {
            fts = value;
            switch (value)
            {
                case firetrapState.Waiting:
                    trap.GetComponent<ParticleSystem>().Play();
                    break;
                case firetrapState.destorying:
                    StartCoroutine(selfDestroy(0));
                    break;
                case firetrapState.trapping:
                   // Register.instance.heros[0].GetComponent<HeroState>().Mana++;
                    break;
            }
        }
    }
    public void hold()
    {
        fts_ = firetrapState.holding;
    }
    public void restart()
    {
        fts_ = firetrapState.Waiting;
    }
    public bool IsWaiting()
    {
        if (fts == firetrapState.Waiting)
            return true;
        else
            return false;
    }
    public float time_;
    void Update()
    {
        if (fts_ != firetrapState.holding)
            time_ += Time.deltaTime;
        if (time_ >= continuingtime && fts_ == firetrapState.Waiting)
        {
            fts_ = firetrapState.destorying;
        }
    }
    IEnumerator selfDestroy(float waitingtime)
    {
        yield return new WaitForSeconds(1);
        trap.GetComponent<ParticleSystem>().Stop();
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        flame.transform.SetParent(transform);
        flame.transform.localPosition = flameIniPos;
        DestoryByPool(this);
    }
    private void OnTriggerEnter(Collider collision)
    {
        int enemyLayer = gameObject.layer == 8 ? 9 : 8;
        if (fts_ != firetrapState.trapping && collision.gameObject.layer == enemyLayer)
        {
            collision.GetComponent<State>().TakeSkillContent(damage);
            collision.gameObject.GetComponent<Hero>().state.Stun(0.6f);//!!!!!!!!!!!!!!!!not only hero!!!!!!
            if (collision.tag == "Heros")
               ((Ragnaros)speller).state.Mana++;
            selfDestroy();
        }
    }
    public void selfDestroy()
    {
        fts_ = firetrapState.trapping;
        flame.SetActive(true);
        StartCoroutine(selfDestroy(5));
    }
    public void BeTrumped()
    {
        fts_ = firetrapState.trapping;
        fireWave.SetActive(true);
        if (flame.transform.parent == gameObject.transform)
        {
            flame.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        }
        StartCoroutine(selfDestroy(1.3f));
    }
    public bool isWaiting()
    {
        if (fts_ == firetrapState.Waiting)
            return true;
        else
            return false;
    }
    public override int GetMaxInstance()
    {
        return maxInstanceNum;
    }
}


