using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag
{
	/// <summary>
	/// Class representing the Boss enemy.
	/// </summary>
	public class Boss : Agent
	{
		#region Public Variables

		/// <summary>
		/// The arrow prefab.
		/// </summary>
		[Tooltip("The arrow prefab")]
		public GameObject ArrowPrefab;

		/// <summary>
		/// The rate to spawn arrows in seconds.
		/// </summary>
		[Tooltip("The rate to spawn arrows in seconds")]
		public float ArrowSpawnRate = 5f;

		#endregion

		#region Private/Protected Variables

		/// <summary>
		/// List of arrows.
		/// </summary>
		private List<HomingArrow> m_arrows;

		#endregion

		#region Public Methods

		/// <summary>
		/// Remove the arrow objects.
		/// </summary>
		public void RemoveArrowObjects()
		{
			foreach (HomingArrow arrow in m_arrows)
			{
				Destroy (arrow.gameObject);
			}

			m_arrows.Clear ();
		}
			
		#endregion

		#region Private/Protected Methods

		/// <summary>
		/// Create and fire homing arrows.
		/// </summary>
		private void fireArrows()
		{
			HomingArrow arrow = Instantiate (ArrowPrefab).GetComponent<HomingArrow> ();
			arrow.transform.position = transform.position;
			//arrow.ArrowDestroyed += arrowDestroyedHandler;

			m_arrows.Add (arrow);
		}

		/// <summary>
		/// Performs required actions when the agent dies.
		/// </summary>
		protected override void die ()
		{
			base.die ();
			Destroy (gameObject);
			RemoveArrowObjects ();
		}

		#endregion
			
		#region Unity Methods

		/// <summary>
		/// Initialize member variables.
		/// </summary>
		protected override void Awake()
		{
			base.Awake ();
			m_arrows = new List<HomingArrow> ();

			InvokeRepeating ("fireArrows", 3f, ArrowSpawnRate);
		}

		#endregion
	}
}