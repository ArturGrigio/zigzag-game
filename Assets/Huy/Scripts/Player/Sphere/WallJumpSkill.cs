using UnityEngine;
using System.Collections;

namespace Huy
{
	/// <summary>
	/// Handle the wall jump mechanic.
	/// </summary>
	public class WallJumpSkill : Skill
	{
		public override void Activate ()
		{
			if (m_player.WallCollided() && m_isEnabled)
			{
				m_isActive = true;
				m_player.IsGrounded = true;
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
