using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EarthMovementController : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 50f;

    float x, y;
    private void OnMouseDrag()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        x = Input.GetAxis("Mouse X") * rotationSpeed * Mathf.Deg2Rad;
        y = Input.GetAxis("Mouse Y") * rotationSpeed * Mathf.Deg2Rad;

        transform.RotateAround(Vector3.up, -x);
        transform.RotateAround(Vector3.right, y);

    }

    public void DownButton()
    {

    }

}
