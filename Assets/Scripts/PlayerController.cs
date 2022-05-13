using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    private float strength = 5;
    private bool spaceWasPressed;
    private bool isGrounded;
    private int count;

    private void Start()
    {
        // zp��stup�n� objektu hr��e
        rb = GetComponent<Rigidbody>();

        count = 0;
    }

    private void Update()
    {
        // sk�k�n� po stisknut� mezern�ku
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceWasPressed = true;      
        }
    }

    // uplat�uje se fyzika
    private void FixedUpdate()
    {
        //pohyb kamery v z�vislosti na pohybu hr��e
        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");
        rb.AddForce(new Vector3(moveH, 0.0f, moveV) * speed);

        // o�et�en� n�kolikn�sobn�ho skoku - skok povolen jen, kdy� je hr�� na zemi
        if (!isGrounded)
        {
            return;
        }

        // skok po stisknut� mezern�ku
        if (spaceWasPressed)
        {
            rb.AddForce(Vector3.up * strength, ForceMode.VelocityChange);
            spaceWasPressed = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Coin")) 
        {
            other.gameObject.SetActive(false);
            count++;
        }
        if (other.gameObject.CompareTag("Goal"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
