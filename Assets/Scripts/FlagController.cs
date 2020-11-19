using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class FlagController : MonoBehaviour
{
    Vector3 originalPosition;
    Vector3 moveTarget;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        moveTarget = transform.up + transform.position;
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rb.MovePosition(moveTarget);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rb.MovePosition(moveTarget);
        }
    }
}
