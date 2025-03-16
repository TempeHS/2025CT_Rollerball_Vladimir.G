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


 public float speed = 25; 
 public TextMeshProUGUI countText;
 public GameObject winTextObject;

//code for jump
     
     public class Player : MonoBehaviour {
     
         public Vector3 jump;
         public float jumpForce = 2.0f;
     
         public bool isGrounded;
         Rigidbody rb;
         void Start(){
             rb = GetComponent<Rigidbody>();
             jump = new Vector3(0.0f, 2.0f, 0.0f);
         }
     
         void OnCollisionStay()
         {
             isGrounded = true;
         }
         void OnCollisionExit(){
           isGrounded = false;
     }
         void Update(){
             if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
     
                 rb.AddForce(jump * jumpForce, ForceMode.Impulse);
                 isGrounded = false;
             }
         }
     }
//


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
 private void FixedUpdate() 
    {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);

        rb.AddForce(movement * speed); 
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