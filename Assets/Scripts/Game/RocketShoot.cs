using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketShoot : MonoBehaviour
{
    [SerializeField] Vector3 force, relativeTorque;
    [SerializeField] float liftSpeed, turnSpeed;
    [SerializeField] List<ParticleSystem> rocketParticles;
    [SerializeField] List<ParticleSystem> smokeParticles;
    [SerializeField] List<Transform> cameraPosition;
    [SerializeField] ElonMusk elonMusk;
    [SerializeField] Button buttonLetsGoToMars;
    [SerializeField] Transform passengerLocation1;
    [SerializeField] Transform passengerLocation2;
    [SerializeField] List<Passenger> passenger;
    [SerializeField] Button buttonLiftOff;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < rocketParticles.Count; i++)
            rocketParticles[i].Stop();

        StartCoroutine(CameraMovement());
    }

    private void ScaleButton(Vector3 vector,Button button)
    {
        button.transform.DOScale(vector, 1).SetEase(Ease.OutBounce);
    }

    public IEnumerator ButtonGoMars()
    {
        ScaleButton(Vector3.zero, buttonLetsGoToMars);
        yield return new WaitForSeconds(1);
        StartCoroutine(PassengerMoveToRocket());
        StartCoroutine(GameManager.Instance.LocateCamera(cameraPosition[0].transform, 1));
        ScaleButton(Vector3.one, buttonLiftOff);
    }

    public void ButtonLetsGo()
    {
        StartCoroutine(ButtonGoMars());
    }
    public void ButtonLaunch()
    {
        StartCoroutine(ButtonRocketMove());
    }

    public IEnumerator ButtonRocketMove()
    {
        ScaleButton(Vector3.zero, buttonLiftOff);
        yield return new WaitForSeconds(1);
        RocketLiftOff();
    }

    public IEnumerator ButtonEvent()
    {
        ScaleButton(Vector3.zero,buttonLetsGoToMars);
        yield return new WaitForSeconds(.5f);
        //yield return new WaitForSeconds(2.5f);
        //ScaleButton(Vector3.one);
    }

    IEnumerator PassengerMoveToRocket()
    {
        for (int i = 0; i < passenger.Count; i++)
        {
            passenger[i].PlayAnim(AnimationType.TurnLeft90);
            yield return new WaitForSeconds(.1f);
            passenger[i].transform.DORotate(new Vector3(0, -45, 0), .2f);
            passenger[i].PlayAnim(AnimationType.Walking);
            passenger[i].transform.DOMove(passengerLocation1.position, 1f);
        }
        yield return new WaitForSeconds(1f);
    }

    public void RocketLiftOff()
    {
        ScaleButton(Vector3.zero,buttonLiftOff);
        gameObject.GetComponent<ConstantForce>().force = Vector3.up * liftSpeed;
        gameObject.GetComponent<ConstantForce>().relativeTorque = new Vector3(0, turnSpeed, 0);
        for (int i = 0; i < rocketParticles.Count; i++)
        {
            rocketParticles[i].Play();
        }
        StartCoroutine(SmokeParticle());
        StartCoroutine(GameManager.Instance.LocateCamera(cameraPosition[3],9));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            RocketLiftOff();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            StartCoroutine(CameraMovement());
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            StartCoroutine(ButtonEvent());
        }
    }


    private IEnumerator CameraMovement()
    {
        yield return new WaitForSeconds(1);
        StartCoroutine(GameManager.Instance.LocateCamera(cameraPosition[1], 2));
        yield return new WaitForSeconds(2);
        StartCoroutine(GameManager.Instance.LocateCamera(cameraPosition[2], 2));
        yield return new WaitForSeconds(2);
        ScaleButton(Vector3.one,buttonLetsGoToMars);

    }


    private IEnumerator SmokeParticle()
    {
        yield return new WaitForSeconds(2);
        for (int i = 0; i < smokeParticles.Count; i++)
        {
            smokeParticles[i].Stop();
        }
    }
}
