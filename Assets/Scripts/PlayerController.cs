using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public float jumpForce = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private Rigidbody rb;
    private float movementX;
    private float movementZ;
    private int count;
    private Keyboard keyboard;

    void Start()
    {
        count = 0;
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 20.0f;
        keyboard = Keyboard.current;
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementZ = movementVector.y;
    }

    /*void OnJump(InputValue inputValue)
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }*/

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 8)
        {
            winTextObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementZ, 0, -movementX);
        rb.AddTorque(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        Vector3 collisionPoint = collisionInfo.GetContact(0).point;
        Vector3 collisionOffset = collisionPoint - transform.position;
        //if(keyboard.spaceKey.wasPressedThisFrame)
        {
            rb.AddForce(collisionOffset.normalized * jumpForce, ForceMode.Force);
        }
    }
}
