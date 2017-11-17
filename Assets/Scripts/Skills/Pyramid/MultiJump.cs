using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag 
{
	public class MultiJump : Jump 
	{
		#region Public Variables


		[Tooltip("Number of jumps allowed by the character")]
		public int JumpCount = 2;
		#endregion

		#region Private/Protected Variables
		private int m_jumpsCompleted = 0;
		#endregion

		#region Properties
		public override bool CanActivate 
		{
			get
			{
				return base.CanActivate || m_jumpsCompleted < JumpCount;
			}
		}
		#endregion

		#region Public Methods

		public override bool Activate ()
		{
			if (AgentComponent.IsGrounded)
			{
				Debug.Log ("GROUND JUMP ACTIVATE");
				return base.Activate ();
			}
			else
			{
				Debug.Log ("AIR JUMP ACTIVATE");
				++m_jumpsCompleted;
				return base.Activate ();
			}
		}

		#endregion

		#region Private/Protected Methods
		protected virtual void OnSurfaceEnter (Collision2D collision, Surface surface)
		{
			if (surface == Surface.Ground)
			{
				m_jumpsCompleted = 0;
			}
		}

		protected virtual void OnSurfaceExit(Collision2D collision, Surface surface)
		{
			if (surface == Surface.Ground && 
				AgentComponent.SurfaceDetectorComponent.IsOnSurface(Surface.Ground) == false)
			{
				++m_jumpsCompleted;
			}
		}

		#endregion

		#region Unity Methods
		protected override void Start()
		{
			base.Start ();
			AgentComponent.SurfaceDetectorComponent.OnSurfaceEnter += OnSurfaceEnter;
			AgentComponent.SurfaceDetectorComponent.OnSurfaceExit += OnSurfaceExit;
		}
		#endregion
	}

}