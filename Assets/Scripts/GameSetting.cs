using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class GameSetting : MonoBehaviour
{
    public string status = "play";
    public GameSetting gameSetting;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public TextMeshProUGUI healthText;
    public GameObject loseTextObject;
    public GameObject pauseTextObject;

    private Rigidbody rb;
    private bool pause;

    private float movementX;
    private float movementY;

    public int count = 0;
    public int wincount = 2;
    public int health = 3;
    public int losehealth = 3;
    public float speed = 10;
    public float jump = 20;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameSetting = FindObjectOfType<GameSetting>();

        pause = false;

        count = 0;
        SetCountText();
        winTextObject.SetActive(false);

        health = losehealth;
        setHealthText();
        loseTextObject.SetActive(false);

        pauseTextObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            // do something
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
        if (Input.GetKeyDown("p"))
        {
            if (pause)
            {
                Time.timeScale = 1;
                pauseTextObject.SetActive(false);
                pause = false;
            }
            else
            {
                Time.timeScale = 0;
                pauseTextObject.SetActive(true);
                pause = true;
            }
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;

    }

    void FixedUpdate()//Terkait Hukum Fisika
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement;

        bool jumpDown = Input.GetKeyDown("space");
        if (jumpDown)
        {
            movement = new Vector3(moveHorizontal, jump, moveVertical);
        }
        else
        {
            movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        }

        rb.AddForce(movement*speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        if (other.gameObject.CompareTag("Obstacle"))
        {
            other.gameObject.SetActive(false);
            health = health - 1;
            setHealthText();
        }
    }

    public void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= wincount)
        {
            winTextObject.SetActive(true);
        }
    }

    public void setHealthText()
    {
        healthText.text = "Health : " + health.ToString();
        if (health <= 0)
        {
            loseTextObject.SetActive(true);
            
        }
    }
}