﻿using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class FireHighBall : SkillItemsBehaviourController
{
    public GameObject spellAudio;
    public GameObject collisionAudio;
    public GameObject releaseAudio;
    public new GameObject light;

    static int maxInstanceNum = 5;
    protected override void Awake()
    {
        base.Awake();
    }
    public FireTrap fireTrap;
    public GameObject FireBall;
    public GameObject FireExplosion;
    public AudioClip dieInsect;
    public RagnarosDamage damage;
    bool IsTrap;

    protected override void OnEnable()
    {
        IsTrap = false;
        light.SetActive(true);
        releaseAudio.SetActive(false);
        spellAudio.SetActive(true);
        FireExplosion.SetActive(false);
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        base.OnEnable();
    }
    public void PlaySoundWhenBeReleased()
    {
        releaseAudio.SetActive(true);
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer != gameObject.layer && col.tag != "weapon")
        {
            light.SetActive(false);
            spellAudio.SetActive(false);
            if (col.gameObject.layer == 9)
            {
                Ragnaros r = speller as Ragnaros;
                r.audioCtrler.PlaySound(dieInsect);
                col.GetComponent<State>().TakeSkillContent(damage);              
            }
            StopEmission(FireBall);
            FireExplosion.SetActive(true);
        }
        // FireBall.SetActive(false);
    }
    void StopEmission(GameObject particle)
    {
        var systems = particle.GetComponentsInChildren<ParticleSystem>();
        FireExplosion.transform.SetParent(GameController.instance.transform);
        FireBall.transform.SetParent(GameController.instance.transform);
        foreach (ParticleSystem system in systems)
        {
            ParticleSystem.CollisionModule cm = system.collision;
            cm.enabled = true;
            system.Stop();
        }
        if (!IsTrap)
        {
            StartCoroutine(DestroyParticle());
            Vector3 p = transform.localPosition;
            p = new Vector3(p.x, 0.1f, p.z);
            FireTrap go = InstantiateByPool(fireTrap,p, GameController.instance.transform, gameObject.layer);
            go.speller = speller;
            IsTrap = true;
        }
    }
    IEnumerator DestroyParticle()
    {
        yield return new WaitForSeconds(6);
        gameObject.SetActive(false);
        FireBall.transform.SetParent(transform);
        FireExplosion.transform.SetParent(transform);
        FireBall.transform.localPosition = Vector3.zero;
        FireExplosion.transform.localPosition = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        DestoryByPool(this);
    }
    public override int GetMaxInstance()
    {
        return maxInstanceNum;
    }
}


