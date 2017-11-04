using UnityEngine;
using System.Collections;

namespace Huy
{
	/// <summary>
	/// Handle the wall sliding mechanic.
	/// </summary>
	public class WallSlidingSkill : Skill
	{
		public override void Activate ()
		{
			if (m_player.WallCollided() && m_isEnabled)
			{
				m_isActive = true;
				m_rigidbody2D.velocity = new Vector2 (m_rigidbody2D.velocity.x, -Time.deltaTime * 0.001f);
				m_player.IsSliding = true;
			}
		}

		public override void Deactivate ()
		{
			m_isActive = false;
		}

		protected override void Awake ()
		{
			base.Awake ();
			m_skillType = SkillTypeEnum.Other;
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			Activate ();
		}
	}
}
