using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LockTranslation : MonoBehaviour
{
	public float LockPositionX;
	public float LockPositionY;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		transform.position = new Vector3 (transform.position.x, LockPositionY, transform.position.z);
	}
}
