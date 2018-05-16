﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sulfuars : SkillItemsBehaviourController
{
    public ParticleSystem trail;
    public ParticleSystem particles;
    [SerializeField]
    private float flameDamageVal;
    [SerializeField]
    private float phyDamageVal;
    private bool isPhyAttack;
    public GameObject colliderObject;
    public string colliderName;
    public bool isCollide;
    public GameObject Effect;
    public Ragnaros hero;

    public void FromMana2Attack()
    {
        Effect.SetActive(false);
        Effect.SetActive(true);
        FlameDamageVal += 10;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        trail.Stop();
    }

    public float FlameDamageVal
    {
        get
        {
            if (hero.state.Stage != 3)
                return flameDamageVal;
            else
                return flameDamageVal / 5;
        }

        set
        {
            ParticleSystem.EmissionModule e = particles.emission;
            flameDamageVal = value;
            e.rateOverTime = flameDamageVal * 2;
        }
    }

    public float PhyDamageVal
    {
        get
        {
            if (hero.state.Stage != 3)
                return phyDamageVal;
            else
                return phyDamageVal / 5;
        }

        set
        {
            phyDamageVal = value;
        }
    }

    public void TurnOnPhyAttack()
    {
        StopAllCoroutines();
        isPhyAttack = true;
        StartCoroutine(TurnOnPhyAttack_(0.7f));
    }
    public bool IsPhyAttack()
    {
        return isPhyAttack;
    }

    IEnumerator TurnOnPhyAttack_(float time)
    {
        if(flameDamageVal != 0)
            trail.Play();
        yield return new WaitForSeconds(time);
        isPhyAttack = false;
        trail.Stop();
        yield return new WaitForSeconds(5);
    }

    protected void Start()
    {
        isCollide = false;
        FlameDamageVal = 0;
        Effect.SetActive(false);
        trail.Stop();
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if (other.gameObject.layer != 8)
        {
            isCollide = true;
            colliderObject = other.gameObject;
            colliderName = other.name;
            StartCoroutine(WaitingthenDelete());
            if(other.gameObject.layer == 9 && isPhyAttack)
            {
                FlameDamageVal += 2f;
                RagnarosDamage flameDamage = new RagnarosDamage(FlameDamageVal, DamageType.Fire, gameObject.layer);
                Damage phyDamage = new Damage(PhyDamageVal, DamageType.Physical);
                other.GetComponent<Rigidbody>().AddForce(-150,0,0);
                other.GetComponent<State>().TakeSkillContent(phyDamage);
                other.GetComponent<State>().TakeSkillContent(flameDamage);
            }
        }
    }
    IEnumerator WaitingthenDelete()
    {
        yield return new WaitForSeconds(0.1f);
        colliderObject = null;
        colliderName = null;
        isCollide = false;
    }
}
