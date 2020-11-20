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
    

    private Rigidbody rb;
    private float movementX;
    private float movementZ;
    private int count;
    private Keyboard keyboard;
    private bool onGround;
    private Vector3 collisionPoint;

    void Start()
    {
        count = 0;
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 20.0f;
        keyboard = Keyboard.current;
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementZ = movementVector.y;
    }

    void OnJump(InputValue inputValue)
    {
        Vector3 collisionOffset = transform.position - collisionPoint;
        if (onGround)
        {
            rb.AddForce(collisionOffset.normalized * jumpForce, ForceMode.Impulse);
        }
    }

    void SetCountText()
    {
        countText.text = "Points: " + count.ToString();
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

        if (other.gameObject.CompareTag("Speed"))
        {
            rb.AddForce(rb.velocity * 1.5f, ForceMode.Impulse);
        }

        if (other.gameObject.CompareTag("Jump"))
        {
            rb.AddForce(other.gameObject.transform.up * jumpForce * 2, ForceMode.Impulse);
        }
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        onGround = true;
        collisionPoint = collisionInfo.GetContact(0).point;
    }

    private void OnCollisionExit(Collision collision)
    {
        onGround = false;
    }
}
