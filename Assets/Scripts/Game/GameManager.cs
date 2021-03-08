using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : LocalSingleton<GameManager>
{
    [SerializeField] BadMan badMan;
    [SerializeField] PryingMan pryingMan;

    [SerializeField] GameObject grenadePrefab;
    [SerializeField] Transform grenadeHandPosition;
    [SerializeField] Transform grenadeHandParent;
    [SerializeField] List<Transform> grenadeExplosionPosition;
    [SerializeField] ParticleSystem grenadeParticleEffect;
    [SerializeField] List<Transform> carTransform;
    [SerializeField] Transform carJumpPos;
    [SerializeField] ParticleSystem fireRed;
    [SerializeField] ParticleSystem fireYellow;
    [SerializeField] List<Transform> fireLocation;

    public float power;
    public float radius;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(badMan.Standing());
        StartCoroutine(pryingMan.Sitting());
    }

    private void Awake()
    {
        grenadeParticleEffect.Stop();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(ThrowHandGrenade(badMan));
        }
    }

    internal IEnumerator ThrowHandGrenade(Human human)
    {
        StartCoroutine(human.ThrowHandGrenade());
        yield return new WaitForSeconds(.58f);
        StartCoroutine(CreateGrenade());
    }

    private void LeaveTheGrenade(GameObject gameObject)
    {
        gameObject.transform.DOJump(grenadeExplosionPosition[0].position, 1, 3, 1);
    }

    internal IEnumerator CreateGrenade()
    {
        yield return new WaitForSeconds(.3f);
        GameObject myGrenade =  Instantiate(grenadePrefab, grenadeHandParent.position, Quaternion.identity);
        grenadePrefab.transform.localScale = new Vector3(.15f,.12f,.12f);
        myGrenade.transform.SetParent(grenadeHandParent);
        grenadePrefab.transform.localPosition = new Vector3(0,0.72f,0);
        yield return new WaitForSeconds(.3f);
        myGrenade.transform.SetParent(null);
        LeaveTheGrenade(myGrenade);
        yield return new WaitForSeconds(.58f);
        StartCoroutine(PlayEffect(grenadeParticleEffect, grenadeExplosionPosition[0].transform));
        myGrenade.AddComponent<Rigidbody>();
        myGrenade.AddComponent<SphereCollider>();
        StartCoroutine(GrenadeExplosion(myGrenade));
        StartCoroutine(CarMovement(carTransform[0]));
        yield return new WaitForSeconds(.4f);
        myGrenade.transform.localScale = Vector3.zero;
        badMan.EvilDegree++;
        StartCoroutine(pryingMan.PlayMixamoAnimation(AnimationType.SitToStand));
        yield return new WaitForSeconds(2);
        StartCoroutine(pryingMan.PlayDefaultAnimation(AnimationType.TurnLeft45));
        yield return new WaitForSeconds(.1f);
        pryingMan.transform.DOMoveY(10.61f,.4f);
        yield return new WaitForSeconds(.4f);
        StartCoroutine(pryingMan.PlayMixamoAnimation(AnimationType.Yelling));
    }

    private IEnumerator GrenadeExplosion(GameObject gameObject)
    {
        Rigidbody rb = new Rigidbody();
        rb = gameObject.GetComponent<Rigidbody>();
        Vector3 explositonPos = gameObject.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explositonPos,radius);
        if (rb!=null)
        {
            rb.AddExplosionForce(power, explositonPos,radius,.3f);
        }
        yield return new WaitForSeconds(.3f);
        ParticleSystem fireRedParticle = Instantiate(fireRed, fireLocation[0].position, Quaternion.identity);
        fireRedParticle.transform.localScale = new Vector3(3,3,4);
    }
    internal IEnumerator PlayEffect(ParticleSystem particleSystem, Transform particleLocation)
    {
        yield return new WaitForSeconds(.3f);
        particleSystem.transform.position = particleLocation.position;
        particleSystem.Play();

    }

    internal IEnumerator CarMovement(Transform transform)
    {
        yield return new WaitForSeconds(.3f);
        transform.DOJump(carJumpPos.position,2,1,.5f);
    }
}
