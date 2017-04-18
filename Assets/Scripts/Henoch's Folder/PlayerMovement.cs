using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

 //   public float movementSpeed;
	//public float jumpingForce;
	//public float groundDist;


 //   // Use this for initialization
 //   void Start()
 //   {
 //       groundDist = this.transform.GetComponent<Collider>().bounds.extents.y;

 //   }

 //   // Update is called once per frame
 //   void Update()
 //   {
 //       Movement();

 //   }

 //   void Movement()
 //   {
 //       //move left to right
 //       //float horizontal = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
 //       if (Input.GetKey(KeyCode.W))
 //           rb.AddForce(transform.forward * movementSpeed);
 //       else if (Input.GetKey(KeyCode.S))
 //           rb.AddForce(transform.forward * -movementSpeed);

 //       //move front to back
 //       // float vertical = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
 //       if (Input.GetKey(KeyCode.D))
 //           rb.AddForce(transform.right * movementSpeed);
 //       else if (Input.GetKey(KeyCode.A))
 //           rb.AddForce(transform.right * -movementSpeed);

 //       if (rb.velocity.magnitude > movementSpeed)
 //           rb.velocity = rb.velocity.normalized * movementSpeed;

 //       //jumping part
 //       if (Input.GetKey(KeyCode.Space) && CheckGrounded() == true)
 //       {

 //           rb.AddForce(0, jumpingForce, 0);
 //       }
 //   }

 //   public bool CheckGrounded()
 //   {
 //       return Physics.Raycast(this.transform.position, -Vector3.up, groundDist + 0.1f);
 //   }

}
