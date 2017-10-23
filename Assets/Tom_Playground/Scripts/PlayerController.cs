using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private bool is_grounded = true;
	public float jump_force = 100.0f;
	public float move_force = 10.0f;
	void Start()
	{
		
	}
	void OnCollisionEnter2D(Collision2D other)
	{
		Debug.Log("Collision with: " + other.gameObject.name + ", tag=" + other.gameObject.tag);
		if (other.gameObject.tag == "Platform") 
		{
			is_grounded = true;
			Debug.Log ("Entered Ground");
		}

	}
	void OnCollisionExit2D(Collision2D other)
	{
		if (other.gameObject.tag == "Platform") 
		{
			is_grounded = false;
			Debug.Log ("Left ground");
		}
	}
	public bool isGrounded()
	{
		return is_grounded;
	}
}
