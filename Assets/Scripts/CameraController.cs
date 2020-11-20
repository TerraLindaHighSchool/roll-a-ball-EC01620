using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private GameObject followCam;
    private Vector3 offset;
    private float maxFollowDist;

    void Start()
    {
        followCam = transform.GetChild(0).gameObject;
        offset = followCam.transform.position;
        maxFollowDist = offset.magnitude;
    }

    void OnLook(InputValue lookValue)
    {
        Vector2 lookVector = lookValue.Get<Vector2>();
        transform.rotation = Quaternion.Euler(transform.rotation.x + -lookVector.y, transform.rotation.y + lookVector.x, transform.rotation.z);
        Mathf.Clamp(transform.rotation.x, -180, 180);
        Debug.Log("poggers");
    }

    void LateUpdate()
    {
        transform.position = player.transform.position;
        offset = followCam.transform.position;
        /*RaycastHit hit;
        Physics.Raycast(transform.position, offset.normalized, out hit, maxFollowDist);
        followCam.transform.position = hit.point;*/
    }
}
