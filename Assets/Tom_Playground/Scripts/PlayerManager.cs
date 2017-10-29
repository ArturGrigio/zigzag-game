using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
	public PlayerController active_player;
	private int active_index = 0;
	private Rigidbody2D rb2D;
	private List<PlayerController> players = new List<PlayerController>();

	// Use this for initialization
	// Update is called once per frame
	void Start()
	{
		foreach (Transform t in transform) {
			PlayerController player = t.gameObject.GetComponent<PlayerController> ();
			if (player != null) 
			{
				for (int i = 0; i < players.Count; ++i) 
				{
					Physics2D.IgnoreCollision (players [i].GetComponent<Collider2D> (), player.GetComponent<Collider2D> ());
				}
				if (player == active_player) 
				{
					active_index = players.Count;
				}
				players.Add (player);
			}
		}
		change_player (active_index);
	}

	void FixedUpdate () 
	{
		if (active_player.isGrounded () == false) 
		{
			rb2D.AddForce (Physics2D.gravity * rb2D.drag * rb2D.mass);
		}
	}
	void Update()
	{
		if (Input.GetButtonDown ("Swap Character")) 
		{
			change_player ((active_index + 1) % players.Count);
		}
		Vector2 move = new Vector2(active_player.move_force * Input.GetAxis ("Horizontal"), 0.0f);
		if (Input.GetButtonDown ("Jump") && active_player.isGrounded()) {
			move.y = active_player.jump_force;
		}
		rb2D.AddForce (move, ForceMode2D.Impulse);
	}
	private void change_player(int index) {
		active_index = index;
		active_player = players [active_index];
		rb2D = active_player.GetComponent<Rigidbody2D> ();
	}
}
