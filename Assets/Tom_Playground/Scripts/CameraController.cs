using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	private Vector3 player_offset;
	public PlayerManager player_manager;
	// Use this for initialization
	void Start () 
	{
		player_offset = transform.position - player_manager.active_player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		transform.position = player_manager.active_player.transform.position + player_offset;
	}
}
