using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Huy
{
	/// <summary>
	/// This class contains all the skills of a shape 
	/// and handles the skills' execution
	/// </summary>
	public sealed class SkillManager : MonoBehaviour
	{
		#region Public Variables

		public PlayerManager playerManager;

		#endregion

		#region Private/Protected Variables

		private List<Skill> m_skills;
		private Skill m_attack;
		private Skill m_ground;
		private Skill m_air;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets a value indicating whether any skill is active.
		/// </summary>
		public bool IsSkillActive
		{
			get
			{
				return false; //TODO: Add skills to player objects and remove this line.
				//return (m_attack.IsActive || m_ground.IsActive || m_air.IsActive);
			}
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Invoke the attack skill of the current shape.
		/// </summary>
		public void Attack()
		{
			if (m_attack != null)
			{
				m_attack.Activate ();
			}
		}

		/// <summary>
		/// Invoke the ground skill of the current shape.
		/// </summary>
		public void Ground()
		{
			if (m_ground != null)
			{
				m_ground.Activate ();
			}
		}

		/// <summary>
		/// Invoke the air skill of the current shape.
		/// </summary>
		public void Air()
		{
			if (m_air != null)
			{
				m_air.Activate ();
			}
		}

		/// <summary>
		/// Sets the current shape and updates internal parameters to handle it.
		/// </summary>
		/// <param name="p">Player component.</param>
		public void SetCurrentShape(Player p)
		{
			updateSkills ();
		}

		#endregion

		#region Private/Protected Methods

		/// <summary>
		/// Update the current shape and the list of skills.
		/// </summary>
		private void updateSkills ()
		{
			m_skills = new List<Skill> (playerManager.CurrentShape.GetComponents<Skill> ());

			m_attack = m_skills.Find (skill => skill.SkillType == SkillTypeEnum.Attack);
			m_ground = m_skills.Find (skill => skill.SkillType == SkillTypeEnum.Ground);
			m_air = m_skills.Find (skill => skill.SkillType == SkillTypeEnum.Air);
		}
			
		#endregion
	}
}
