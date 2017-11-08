using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag
{
	public class Camera2DFollow : MonoBehaviour
	{

		#region Public Variables
		public Transform Target;
		public float Damping = 1;
		public float LookAheadFactor = 3;
		public float LookAheadReturnSpeed = 0.5f;
		public float LookAheadMoveThreshold = 0.1f;
		#endregion

		#region Private/Protected Variables
		private float m_OffsetZ;
		private Vector3 m_LastTargetPosition;
		private Vector3 m_CurrentVelocity;
		private Vector3 m_LookAheadPos;
		#endregion

		#region Properties
		#endregion

		#region Public Methods
		#endregion

		#region Private/Protected Methods
		#endregion

		#region Unity Methods
		// Use this for initialization
		private void Start()
		{
			m_LastTargetPosition = Target.position;
			m_OffsetZ = (transform.position - Target.position).z;
			transform.parent = null;
		}

		// Update is called once per frame
		private void Update()
		{
			// only update lookahead pos if accelerating or changed direction
			float xMoveDelta = (Target.position - m_LastTargetPosition).x;

			bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > LookAheadMoveThreshold;

			if (updateLookAheadTarget)
			{
				m_LookAheadPos = LookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
			}
			else
			{
				m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime*LookAheadReturnSpeed);
			}

			Vector3 aheadTargetPos = Target.position + m_LookAheadPos + Vector3.forward*m_OffsetZ;
			Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, Damping);

			transform.position = newPos;

			m_LastTargetPosition = Target.position;
		}
		#endregion

	}
}