﻿using UnityEngine;
using System.Collections;

namespace Huy
{
	public class CubeAttack: Skill
	{
		#region Public Methods

		/// <summary>
		/// Activate the skill and begin any sort of animations or movements.
		/// </summary>
		public override void Activate()
		{
			StartCoroutine (RotateCoroutine ());
		}

		/// <summary>
		/// Deactivate the skill and stop the player during the skill executation
		/// or when the skill is done executing.
		/// </summary>
		public override void Deactivate()
		{
			m_isActive = false;
		}

		#endregion

		#region Unity Methods

		/// <summary>
		/// Initialize member variables.
		/// </summary>
		protected override void Awake ()
		{
			base.Awake ();
			m_skillType = SkillTypeEnum.Attack;
		}

		/// <summary>
		/// The action of the skill is performed only if it is active.
		/// </summary>
		private void Update()
		{
			if (m_isActive)
			{
				transform.Rotate (new Vector3 (0.0f, 0.0f, 10.0f));
			}
		}

		#endregion

		#region Private/Protected Methods

		/// <summary>
		/// The rotation coroutine.
		/// </summary>
		/// <returns>The coroutine.</returns>
		private IEnumerator RotateCoroutine()
		{
			m_isActive = true;
			GetComponent<Renderer> ().material.color = Color.red;
			m_rigidbody2D.velocity = Vector2.zero;

			yield return new WaitForSeconds (5f);

			Deactivate ();
		}

		#endregion
	}
}
