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
    }

    private void ScaleButton(Vector3 vector)
    {
        buttonLetsGoToMars.transform.DOScale(vector, 1).SetEase(Ease.OutElastic);
    }

    public IEnumerator ButtonEvent()
    {
        ScaleButton(Vector3.zero);
        yield return new WaitForSeconds(1);
        StartCoroutine(PassengerMoveToRocket());
        yield return new WaitForSeconds(2);
        StartCoroutine(GameManager.Instance.LocateCamera(cameraPosition[0].transform, 2));
        yield return new WaitForSeconds(1);
        ScaleButton(Vector3.one);
    }

    IEnumerator PassengerMoveToRocket()
    {
        for (int i = 0; i < passenger.Count; i++)
        {
            passenger[i].PlayAnim(AnimationType.TurnLeft90);
            yield return new WaitForSeconds(1f);
            passenger[i].transform.DORotate(new Vector3(0, -45, 0), .2f);
            passenger[i].PlayAnim(AnimationType.Walking);
            passenger[i].transform.DOMove(passengerLocation1.position, 2);
            yield return new WaitForSeconds(.05f);
        }
        yield return new WaitForSeconds(1f); 
    }

    public void RocketLiftOff()
    {
        ScaleButton(Vector3.zero);
        gameObject.GetComponent<ConstantForce>().force = Vector3.up * liftSpeed;
        gameObject.GetComponent<ConstantForce>().relativeTorque = new Vector3(0, turnSpeed, 0);
        for (int i = 0; i < rocketParticles.Count; i++)
        {
            rocketParticles[i].Play();
        }
        StartCoroutine(SmokeParticle());
        StartCoroutine(GameManager.Instance.LocateCamera(cameraPosition[3],8));
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
            //elonMusk.PlayAnim(AnimationType.Yelling);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            StartCoroutine(ButtonEvent());
        }

        //Camera.main.transform.LookAt(this.gameObject.transform);
    }

    IEnumerator FollowTheObject(GameObject gameObject)
    {
        while (true)
        {
            Camera.main.transform.LookAt(gameObject.transform);
            yield return new WaitForSeconds(.01f);
        }
    }

    private IEnumerator CameraMovement()
    {
        StartCoroutine(GameManager.Instance.LocateCamera(cameraPosition[1], 2));
        yield return new WaitForSeconds(5);
        StartCoroutine(GameManager.Instance.LocateCamera(cameraPosition[2], 2));
        yield return new WaitForSeconds(3);
        ScaleButton(Vector3.one);
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
