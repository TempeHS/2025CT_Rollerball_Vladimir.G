using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public float speed;
    public float hop;



    void Start()
    {
        SetCountText();
        Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        rb = GetComponent<Rigidbody>();
        count = 0;
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }


    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 20)
        {
            winTextObject.SetActive(true);
        }

    }

    void Update()
    {
        bool shouldJump = GetComponent<Rigidbody>().transform.position.y <= 1.6250001f;
        if (Input.GetKeyDown("space") && shouldJump)
        {
            Vector3 jump = new Vector3(0.0f, 500.0f, 0.0f);

            GetComponent<Rigidbody>().AddForce(jump);
        }
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);

        GetComponent<Rigidbody>().AddForce(movement * speed * Time.deltaTime);



    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyObject"))
        {
            // Destroy the current object
            Destroy(gameObject);
            // Update the winText to display "You Lose!"
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }
    }

}