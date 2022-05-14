using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    private float strength = 5;
    private bool spaceWasPressed;
    private bool isGrounded;
    private int score;
    private int scoreDelta = 10;
    public Text scoreText;
    public Text goalText;
    private bool isEnd;

    private void Start()
    {
        Cursor.visible = false;
        // zp��stup�n� objektu hr��e
        rb = GetComponent<Rigidbody>();
        // skore na za��tku hry
        score = 0;
        SetScore();
        goalText.text = "";
        isEnd = false; 
    }

    private void Update()
    {
        // sk�k�n� po stisknut� mezern�ku --> p�ejde do FixedUpdate
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceWasPressed = true;      
        }
    }

    // v t�to metod� se uplat�uje fyzika
    private void FixedUpdate()
    {
        // kamera sleduje pohyb hr��e
        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");
        rb.AddForce(new Vector3(moveH, 0.0f, moveV) * speed);

        // skok  hr��e jen, kdy� je hr�� na zemi --> nelze se odr�et ve vzduchu
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

    // p�ep�na�e, jestli je hr�� ve vzduchu nebo na hern� plo�e - zde je na plo�e
    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }
    // p�ep�na�e, jestli je hr�� ve vzduchu nebo na hern� plo�e - zde je ve vzduchu
    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    // interakce hr��e s dal��mi objekty
    private void OnTriggerEnter(Collider other)
    {
        // hr�� sebere minci a ta "zmiz�"
        if(other.gameObject.CompareTag("Coin")) 
        {
            other.gameObject.SetActive(false);
            // po��t�n� skore
            score = score + scoreDelta;
            SetScore();
        }

        // ove�uje konec hry, pokud nen� konec hry, tak se z kodu vysko��,
        if (!isEnd)
        {
            return;
        }
        
        
        // vykon� se, pokud je konec hry 
        // kdy� hr�� dojde do c�le, zmiz� "goal"
        if (other.gameObject.CompareTag("Goal"))
        {
            other.gameObject.SetActive(false);
            EndOfTheGame();
        }
        
    }

    private void SetScore()
    {
        scoreText.text = "Skore: " + score.ToString();
        if (score >= 220)
        {
            isEnd = true;
        }
    }

    private void EndOfTheGame()
    {
        if (isEnd)
        {
            goalText.text = "Konec hry";
        }
    }

}
