﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrumpStamp1 : SkillItemsBehaviourController
{
    public RagnarosDamage fireDamage;
    public Damage phyDamage;

    protected void Start()
    {
        Sulfuars sul = (Sulfuars)GameController.Register.FindHeroByLayer(gameObject.layer).weapon;
        fireDamage = new RagnarosDamage(3 * sul.FlameDamageVal, DamageType.Fire, gameObject.layer);
        phyDamage = new Damage(3 * sul.PhyDamageVal, DamageType.Physical);
        StartCoroutine(ValidTrigger());
    }
    IEnumerator ValidTrigger()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.GetComponent<Collider>().enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            other.GetComponent<State>().TakeSkillContent(fireDamage);
            other.GetComponent<State>().TakeSkillContent(phyDamage);
            if (other.GetComponent<Rigidbody>() != null)
                other.GetComponent<Rigidbody>().AddForce(0, -500, 0);
        }
    }
    public void LetStun(State state)
    {
    }
}