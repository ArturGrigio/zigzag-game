using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag
{
	public enum ProjectileMode { Additive, Absolute }
	public enum ProjectileDeath { Destroy, Deactivate }

	[RequireComponent(typeof(Collider2D))]
	[RequireComponent(typeof(Rigidbody2D))]
	public class Projectile : Attacker
	{
		#region Public Variables
		public float VelocityX = 0f;
		public float VelocityY = 0f;
		public ProjectileMode Mode = ProjectileMode.Absolute;
		public ProjectileDeath Death = ProjectileDeath.Destroy;
		public Agent Parent = null;
		#endregion

		#region Private/Protected Variables
		private Rigidbody2D m_rigidbody2D;
		private Collider2D m_collider = null;
		#endregion

		#region Properties
		#endregion

		#region Public Methods

		public void Kill()
		{
			if (Death == ProjectileDeath.Destroy)
			{
				Destroy (this);
			} 
			else
			{
				gameObject.SetActive (false);
				m_collider.enabled = false;
			}
		}

		public void Enable()
		{
			gameObject.SetActive (true);
			m_collider.enabled = true;
		}
		#endregion

		#region Private/Protected Methods
		protected override void OnCollisionEnter2D(Collision2D collision)
		{
			base.OnCollisionEnter2D (collision);
			Kill ();
		}
		#endregion

		#region Unity Methods
		protected override void Awake()
		{
			base.Awake ();
			m_rigidbody2D = GetComponent<Rigidbody2D> ();
			if (Death == ProjectileDeath.Deactivate)
			{
				m_collider = GetComponent<Collider2D> ();
			}
		}

		protected virtual void Start() 
		{
			switch (Mode)
			{
			case ProjectileMode.Absolute:
				m_rigidbody2D.velocity = new Vector2 (VelocityX, VelocityY);
				break;
			case ProjectileMode.Additive:
				Vector2 parentVelocity = Parent.GetVelocity ();
				m_rigidbody2D.velocity = new Vector2 (VelocityX + parentVelocity.x, VelocityY + parentVelocity.y);
				break;
			}
		}
		#endregion
	}
}
