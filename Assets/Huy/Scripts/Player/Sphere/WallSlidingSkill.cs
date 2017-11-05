using UnityEngine;
using System.Collections;

namespace Huy
{
	/// <summary>
	/// Handle the wall sliding mechanic.
	/// </summary>
	public class WallSlidingSkill : Skill
	{
		#region Public Variables

		/// <summary>
		/// The sliding multiplier that determines the speed of sliding down against a wall.
		/// </summary>
		[Tooltip("The sliding multiplier that determines the speed of sliding down against a wall.")]
		[Range(0f, 1.0f)]
		public float SlidingMultiplier = 0.001f;

		#endregion

		#region Public Methods

		/// <summary>
		/// Activate the skill and begin any sort of animations or movements.
		/// </summary>
		public override void Activate ()
		{
			if (m_player.WallCollided() && m_isEnabled)
			{
				m_isActive = true;
				m_rigidbody2D.velocity = new Vector2 (m_rigidbody2D.velocity.x, -Time.deltaTime * SlidingMultiplier);
				m_player.IsSliding = true;
			}
		}

		/// <summary>
		/// Deactivate the skill and stop the player during the skill executation
		/// or when the skill is done executing.
		/// </summary>
		public override void Deactivate ()
		{
		}

		#endregion

		#region Unity Methods

		/// <summary>
		/// Initialize member variables.
		/// </summary>
		protected override void Awake ()
		{
			base.Awake ();
			m_skillType = SkillTypeEnum.Other;
		}

		/// <summary>
		/// Raises the collision enter 2D event.
		/// </summary>
		/// 
		/// <remarks>
		/// Activate the skill here.
		/// </remarks>
		/// <param name="collision">Collision.</param>
		private void OnCollisionEnter2D(Collision2D collision)
		{
			Activate ();
		}

		#endregion
	}
}
