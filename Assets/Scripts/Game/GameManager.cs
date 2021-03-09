using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BadManGameState
{
    FirstCar,
    SecondCar,
    Police,
}

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
    [SerializeField] List<Transform> carJumpPos;
    [SerializeField] ParticleSystem fireRed;
    [SerializeField] ParticleSystem fireYellow;
    [SerializeField] List<Transform> fireLocation;
    [SerializeField] List<Transform> badManPosition;
    [SerializeField] Transform mainCamera;
    [SerializeField] List<Transform> cameraPosition;
    [SerializeField] Transform policeCar;
    [SerializeField] Transform policeCarStopLocation;
    [SerializeField] List<Police> police;
    [SerializeField] List<Transform> policeStopPosition;
    [SerializeField] Button runButton;
    [SerializeField] List<Transform> pryingManPosition;

    public BadManGameState badManGameState;
    public float power;
    public float radius;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(badMan.Standing());
        StartCoroutine(pryingMan.Sitting());
        Rescale(police[0].transform, Vector3.zero);
        Rescale(police[1].transform, Vector3.zero);
        runButton.onClick.AddListener(call: () => RunButton());
    }

    private void RunButton()
    {
        StartCoroutine(BadManEscaping());
    }

    IEnumerator BadManEscaping()
    {
        Vector3 vector = new Vector3(0,-82,0);
        //runButton.transform.DOScale(Vector3.zero, .3f).SetEase(Ease.OutElastic);
        runButton.gameObject.SetActive(false);
        StartCoroutine(LocateCamera(cameraPosition[3], .4f));
        yield return new WaitForSeconds(.5f);
        Time.timeScale = 1;
        StartCoroutine(badMan.PlayMixamoAnimation(AnimationType.Walking));
        badMan.transform.DOMove(badManPosition[2].position, 2);
        yield return new WaitForSeconds(1.8f);
        StartCoroutine(badMan.PlayDefaultAnimation(AnimationType.Crouching));
        yield return new WaitForSeconds(.5f);
        StartCoroutine(badMan.PlayMixamoAnimation(AnimationType.CrouchTurnLeft90));
        if (badMan.transform.rotation.y != -82f)
            badMan.transform.localEulerAngles = vector;
        yield return new WaitForSeconds(2.4f);
        StartCoroutine(badMan.PlayMixamoAnimation(AnimationType.CrouchingIdle));
        StartCoroutine(LocateCamera(cameraPosition[4], .5f));
        //yield return new WaitForSeconds(.5f);
        //StartCoroutine(LocateCamera(cameraPosition[5], .5f));
    }

    private void Rescale(Transform scaleTransform, Vector3 vector3)
    {
        scaleTransform.localScale = vector3;
    }

    private void Awake()
    {
        grenadeParticleEffect.Stop();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            badManGameState = BadManGameState.FirstCar;
            StartCoroutine(ThrowHandGrenade(badMan, grenadeExplosionPosition[0], fireLocation[0], badManGameState));

        }
        if (Input.GetKeyDown(KeyCode.W))
            StartCoroutine(Walking(badMan, badManPosition[1], true));
        if (Input.GetKeyDown(KeyCode.E))
        {
            badManGameState = BadManGameState.SecondCar;
            StartCoroutine(ThrowHandGrenade(badMan, grenadeExplosionPosition[1], fireLocation[1], badManGameState));
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            badManGameState = BadManGameState.Police;
            StartCoroutine(RunningState());
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            badManGameState = BadManGameState.Police;
            StartCoroutine(LookAtPolice());
        }
    }
    IEnumerator PoliceRunning(Human human, Transform endLocation, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        StartCoroutine(human.PlayDefaultAnimation(AnimationType.Walking, 1));
        human.transform.DOMove(endLocation.position, 5);
        yield return new WaitForSeconds(5);
        StartCoroutine(human.PlayDefaultAnimation(AnimationType.TurningRight90, 1));
        yield return new WaitForSeconds(1);
        StartCoroutine(human.PlayMixamoAnimation(AnimationType.PistolIdle));
        human.transform.DORotate(new Vector3(0,45,0),.4f);
        yield return new WaitForSeconds(.5f);
        StartCoroutine(Begging());
    }
    IEnumerator Begging()
    {
        StartCoroutine(pryingMan.PlayMixamoAnimation(AnimationType.Moving));
        pryingMan.transform.DOMove(pryingManPosition[0].position,1);
        //pryingMan.transform.DOMove(pryingMan.transform.position, .2f);
        yield return new WaitForSeconds(1);
        StartCoroutine(pryingMan.PlayMixamoAnimation(AnimationType.Begging));
        pryingMan.transform.DOMove(pryingMan.transform.position, .2f);
        pryingMan.transform.DORotateQuaternion(pryingMan.transform.rotation, .2f);
        yield return new WaitForSeconds(1);
        StartCoroutine(pryingMan.PlayMixamoAnimation(AnimationType.BeggingIdle));
        pryingMan.transform.DOMove(pryingManPosition[1].position, .4f);
        pryingMan.transform.DORotateQuaternion(pryingManPosition[1].rotation, .4f);
    }

    IEnumerator PoliceCar(Transform carTransform, Transform endLocation, float time)
    {
        carTransform.DOMove(endLocation.position, time);
        yield return new WaitForSeconds(.5f);
        Rescale(police[0].transform, new Vector3(1.2f, 1.2f, 1.2f));
        Rescale(police[1].transform, new Vector3(1.2f, 1.2f, 1.2f));
        StartCoroutine(PoliceRunning(police[0], policeStopPosition[0], .3f));
        StartCoroutine(PoliceRunning(police[1], policeStopPosition[1], .5f));
    }

    IEnumerator LookAtPolice()
    {
        StartCoroutine(badMan.PlayDefaultAnimation(AnimationType.TurnLeft90, .5f));
        StartCoroutine(LocateCamera(cameraPosition[2], .58f));
        yield return new WaitForSeconds(.4f);
        runButton.transform.DOScale(Vector3.one, .2f).SetEase(Ease.OutElastic);
        Time.timeScale = .1f;
    }

    IEnumerator RunningState()
    {
        yield return new WaitForSeconds(1);
        StartCoroutine(PoliceCar(policeCar, policeCarStopLocation, 1));
    }

    internal IEnumerator ThrowHandGrenade(Human human, Transform explosionPosition, Transform firePosition, BadManGameState badManGameState)
    {
        StartCoroutine(human.ThrowHandGrenade());
        yield return new WaitForSeconds(.58f);
        StartCoroutine(CreateGrenade(explosionPosition, firePosition, badManGameState));
    }

    private void LeaveTheGrenade(GameObject gameObject, Transform explosionPosition)
    {
        gameObject.transform.DOJump(explosionPosition.position, 1, 3, 1);
    }

    internal IEnumerator CreateGrenade(Transform explosionPosition, Transform firePosition, BadManGameState badManGameState)
    {
        yield return new WaitForSeconds(.3f);
        GameObject myGrenade = Instantiate(grenadePrefab, grenadeHandParent.position, Quaternion.identity);
        grenadePrefab.transform.localScale = new Vector3(.15f, .12f, .12f);
        myGrenade.transform.SetParent(grenadeHandParent);
        grenadePrefab.transform.localPosition = new Vector3(0, 0.72f, 0);
        yield return new WaitForSeconds(.3f);
        myGrenade.transform.SetParent(null);
        LeaveTheGrenade(myGrenade, explosionPosition);
        yield return new WaitForSeconds(.58f);
        StartCoroutine(PlayEffect(grenadeParticleEffect, explosionPosition));
        myGrenade.AddComponent<Rigidbody>();
        myGrenade.AddComponent<SphereCollider>();
        StartCoroutine(GrenadeExplosion(myGrenade, firePosition));
        if (firePosition == fireLocation[0])
            StartCoroutine(CarMovement(carTransform[0], carJumpPos[0]));
        else
            StartCoroutine(CarMovement(carTransform[1], carJumpPos[1]));
        yield return new WaitForSeconds(.4f);
        myGrenade.transform.localScale = Vector3.zero;
        badMan.EvilDegree++;
        StartCoroutine(PryingManReaction(badManGameState));
    }

    IEnumerator PryingManReaction(BadManGameState badManGameState)
    {
        if (badManGameState == BadManGameState.FirstCar)
        {
            StartCoroutine(pryingMan.PlayMixamoAnimation(AnimationType.SitToStand));
            yield return new WaitForSeconds(2);
            StartCoroutine(pryingMan.PlayDefaultAnimation(AnimationType.TurnLeft45));
            yield return new WaitForSeconds(.1f);
            pryingMan.transform.DOMoveY(10.61f, .4f);
            yield return new WaitForSeconds(.4f);
            StartCoroutine(pryingMan.PlayMixamoAnimation(AnimationType.Yelling));
        }
        else if (badManGameState == BadManGameState.SecondCar)
        {
            StartCoroutine(pryingMan.PlayDefaultAnimation(AnimationType.TurnRight45));
            yield return new WaitForSeconds(.1f);
            pryingMan.transform.DOMoveY(10.61f, .4f);
            yield return new WaitForSeconds(.4f);
            StartCoroutine(pryingMan.PlayMixamoAnimation(AnimationType.Yelling));
        }
    }

    internal IEnumerator Turning(Human human, AnimationType animationType)
    {
        StartCoroutine(human.PlayDefaultAnimation(animationType));
        yield return new WaitForSeconds(.3f);
    }

    internal IEnumerator Walking(Human human, Transform endTransform, bool isTurnActive = false)
    {
        if (isTurnActive)
        {
            StartCoroutine(Turning(human, AnimationType.TurnLeft90));
            yield return new WaitForSeconds(.8f);
            human.transform.DORotate(new Vector3(0, -90, 0),.3f);
        }

        StartCoroutine(LocateCamera(cameraPosition[1]));
        StartCoroutine(human.PlayDefaultAnimation(AnimationType.Walking));
        human.transform.DOMove(endTransform.position, 2);
        yield return new WaitForSeconds(1.9f);
        if (isTurnActive)
        {
            StartCoroutine(Turning(human, AnimationType.TurnRight45));
            yield return new WaitForSeconds(.78f);
            human.transform.DORotate(new Vector3(0,0,0), .2f);
        }
    }

    private IEnumerator GrenadeExplosion(GameObject gameObject, Transform fireLocation)
    {
        Rigidbody rb = new Rigidbody();
        rb = gameObject.GetComponent<Rigidbody>();
        Vector3 explositonPos = gameObject.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explositonPos, radius);
        if (rb != null)
        {
            rb.AddExplosionForce(power, explositonPos, radius, .3f);
        }
        yield return new WaitForSeconds(.3f);
        ParticleSystem fireRedParticle = Instantiate(fireRed, fireLocation.position, Quaternion.identity);
        fireRedParticle.transform.localScale = new Vector3(3, 3, 4);
    }
    internal IEnumerator PlayEffect(ParticleSystem particleSystem, Transform particleLocation)
    {
        yield return new WaitForSeconds(.3f);
        particleSystem.transform.position = particleLocation.position;
        particleSystem.Play();

    }
    internal IEnumerator LocateCamera(Transform cameraPosition, float time = 2)
    {
        mainCamera.transform.DOMove(cameraPosition.position, time);
        mainCamera.transform.DORotateQuaternion(cameraPosition.rotation, time);
        yield return new WaitForSeconds(.4f);
    }

    internal IEnumerator CarMovement(Transform transform, Transform carJumpPos)
    {
        yield return new WaitForSeconds(.3f);
        transform.DOJump(carJumpPos.position, 2, 1, .5f);
    }
}
