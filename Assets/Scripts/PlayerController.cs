using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public GameSetting gameSetting;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameSetting = FindObjectOfType<GameSetting>();

        if (gameSetting == null)
        {
            Debug.LogError("GameSetting not found in the scene!");
        }
    }

    void FixedUpdate()
    {
        if (gameSetting != null && gameSetting.status == "play")
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement;

            bool jumpDown = Input.GetKeyDown(KeyCode.Space);
            if (jumpDown)
            {
                movement = new Vector3(moveHorizontal, gameSetting.jump, moveVertical);
            }
            else
            {
                movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            }

            rb.AddForce(movement * gameSetting.speed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (gameSetting != null)
        {
            if (other.gameObject.CompareTag("PickUp"))
            {
                other.gameObject.SetActive(false);
                gameSetting.count++;
                gameSetting.SetCountText();
            }

            if (other.gameObject.CompareTag("Obstacle"))
            {
                if (gameSetting.health > 0)
                {
                    gameSetting.health--;
                }
                gameSetting.setHealthText();
            }
        }
    }
}
