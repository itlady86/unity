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
        // zpøístupìní objektu hráèe
        rb = GetComponent<Rigidbody>();
        // skore na zaèátku hry
        score = 0;
        SetScore();
        goalText.text = "";
        isEnd = false; 
    }

    private void Update()
    {
        // skákání po stisknutí mezerníku --> pøejde do FixedUpdate
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceWasPressed = true;      
        }
    }

    // v této metodì se uplatòuje fyzika
    private void FixedUpdate()
    {
        // kamera sleduje pohyb hráèe
        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");
        rb.AddForce(new Vector3(moveH, 0.0f, moveV) * speed);

        // skok  hráèe jen, když je hráè na zemi --> nelze se odrážet ve vzduchu
        if (!isGrounded)
        {
            return;
        }

        // skok po stisknutí mezerníku
        if (spaceWasPressed)
        {
            rb.AddForce(Vector3.up * strength, ForceMode.VelocityChange);
            spaceWasPressed = false;
        }
    }

    // pøepínaèe, jestli je hráè ve vzduchu nebo na herní ploše - zde je na ploše
    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }
    // pøepínaèe, jestli je hráè ve vzduchu nebo na herní ploše - zde je ve vzduchu
    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    // interakce hráèe s dalšími objekty
    private void OnTriggerEnter(Collider other)
    {
        // hráè sebere minci a ta "zmizí"
        if(other.gameObject.CompareTag("Coin")) 
        {
            other.gameObject.SetActive(false);
            // poèítání skore
            score = score + scoreDelta;
            SetScore();
        }

        // oveøuje konec hry, pokud není konec hry, tak se z kodu vyskoèí,
        if (!isEnd)
        {
            return;
        }
        
        
        // vykoná se, pokud je konec hry 
        // když hráè dojde do cíle, zmizí "goal"
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
