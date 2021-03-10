using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneState
{
    Scene1,
    Scene2,
    Scene3
}

public class CameraController : LocalSingleton<CameraController>
{
    public Camera MainCamera;
    public List<Transform> Locations;
    public SceneState sceneState;
    private void Start()
    {
        MainCamera = this.gameObject.GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            StartCoroutine(LocateMainCamera(Locations[0], 1));
            sceneState = SceneState.Scene1;
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            StartCoroutine(LocateMainCamera(Locations[1], 1));
            sceneState = SceneState.Scene2;
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            StartCoroutine(LocateMainCamera(Locations[2], 1));
            sceneState = SceneState.Scene3;
        }
    }

    public IEnumerator LocateMainCamera(Transform changeToThisLocation, float time)
    {
        MainCamera.transform.DOMove(changeToThisLocation.position,time);
        MainCamera.transform.DORotateQuaternion(changeToThisLocation.rotation,time);
        yield return new WaitForSeconds(.5f);
    }
}
