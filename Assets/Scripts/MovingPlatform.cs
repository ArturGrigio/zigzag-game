using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
	public Vector2 min_offset = new Vector2 (0, 0);
	public Vector2 max_offset = new Vector2 (0, 0);
	public Vector2 speed = new Vector2(1,1);

	private Vector2 min, max;
	private Rigidbody2D rb2D;
	// Use this for initialization
	void Start () 
	{
		rb2D = transform.GetComponent<Rigidbody2D> ();
		min = (Vector2)transform.position - min_offset;
		max = (Vector2)transform.position + max_offset;
		if (min_offset != Vector2.zero || max_offset != Vector2.zero) 
		{
			rb2D.velocity = speed;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		Vector2 set_velocity = rb2D.velocity;
		if (transform.position.x > max.x || transform.position.x < min.x) 
		{
			set_velocity.x = -set_velocity.x;
		}
		if (transform.position.y > max.y || transform.position.y < min.y) 
		{
			set_velocity.y = -set_velocity.y;
		}
		rb2D.velocity = set_velocity;
	}
}
