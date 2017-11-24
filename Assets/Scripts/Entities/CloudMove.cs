using UnityEngine;
using System.Collections;

namespace ZigZag
{
	public class CloudMove : MonoBehaviour
	{
		//Set these variables to whatever you want the slowest and fastest speed for the clouds to be, through the inspector.
		//If you don't want clouds to have randomized speed, just set both of these to the same number.
		//For Example, I have these set to 2 and 5
		public float minSpeed;
		public float maxSpeed;

		//Set these variables to the lowest and highest y values you want clouds to spawn at.
		//For Example, I have these set to 1 and 4
		public float minY;
		public float maxY;

		//Set this variable to how far off screen you want the cloud to spawn, and how far off the screen you want the cloud to be for it to despawn. You probably want this value to be greater than or equal to half the width of your cloud.
		//For Example, I have this set to 4, which should be more than enough for any cloud.
		public float buffer;

		float speed;
		float camWidth;
		Transform cameraTransform;

		void Start()
		{
			//Set camWidth. Will be used later to check whether or not cloud is off screen.
			camWidth = Camera.main.orthographicSize * Camera.main.aspect;

			//Set Cloud Movement Speed, and Position to random values within range defined above
			speed = Random.Range(minSpeed, maxSpeed);
			transform.position = new Vector3(-camWidth - buffer, Random.Range(minY, maxY), transform.position.z);

			cameraTransform = Camera.main.transform;
		}

		// Update is called once per frame
		void Update() 
		{
			//Translates the cloud to the right at the speed that is selected
			transform.Translate(speed * Time.deltaTime, 0, 0);

			transform.position = new Vector3 (transform.position.x, cameraTransform.position.y, transform.position.z);

			//If cloud is off Screen, Destroy it.
			if(transform.position.x - buffer > camWidth)
			{
				Destroy(gameObject);
			}
		}
	}
}