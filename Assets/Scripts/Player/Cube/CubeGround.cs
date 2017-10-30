using UnityEngine;
using System.Collections;

namespace ZigZag
{
	public class CubeGround : Skill
	{
		#region Public Methods

		/// <summary>
		/// Activate the skill and begin any sort of animations or movements.
		/// </summary>
		public override void Activate()
		{
			StartCoroutine (MoveUpCoroutine ());
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

		#region Private/Protected Methods

		/// <summary>
		/// Initialize member variables.
		/// </summary>
		protected override void Awake ()
		{
			base.Awake ();
			m_skillType = SkillTypeEnum.Ground;
		}

		/// <summary>
		/// The action of the skill is performed only if it is active.
		/// </summary>
		private void Update()
		{
			if (m_isActive)
			{
				transform.Translate (new Vector2 (0f, 0.1f));
			}
		}

		/// <summary>
		/// Moves up coroutine.
		/// </summary>
		/// <returns>The up coroutine.</returns>
		private IEnumerator MoveUpCoroutine()
		{
			m_isActive = true;
			GetComponent<Renderer> ().material.color = Color.green;
			m_rigidbody2D.velocity = Vector2.zero;

			yield return new WaitForSeconds (5f);
			Deactivate ();
		}

		#endregion
	}
}
