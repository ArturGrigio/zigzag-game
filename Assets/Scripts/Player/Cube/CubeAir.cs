using UnityEngine;
using System.Collections;

namespace ZigZag
{
	public class CubeAir : Skill
	{
		#region Public Methods

		/// <summary>
		/// Activate the skill and begin any sort of animations or movements.
		/// </summary>
		public override void Activate()
		{
			StartCoroutine (MoveRightCoroutine ());
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
			m_skillType = SkillTypeEnum.Air;
		}

		/// <summary>
		/// The action of the skill is performed only if it is active.
		/// </summary>
		void Update()
		{
			if (m_isActive)
			{
				transform.Translate (new Vector2 (0.1f, 0f));
			}
		}

		#endregion

		#region Private/Protected Methods

		/// <summary>
		/// Moves the right coroutine.
		/// </summary>
		/// <returns>The right coroutine.</returns>
		private IEnumerator MoveRightCoroutine()
		{
			m_isActive = true;
			GetComponent<Renderer> ().material.color = Color.blue;


			yield return new WaitForSeconds (5f);
			Deactivate ();
		}

		#endregion
	}
}
