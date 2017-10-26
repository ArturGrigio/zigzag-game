using UnityEngine;
using System.Collections;

namespace ZigZag
{
	public abstract class AirSkill : MonoBehaviour, ISkill
	{
		public string airSkillName = "air";
		protected bool m_isGrounded;
		protected bool m_isActive;
		protected bool m_isEnabled;

		public bool IsActive 
		{
			get { return m_isActive; }
		}

		public bool IsEnabled 
		{
			get { return m_isEnabled; }
			set { m_isEnabled = value; }
		}

		public abstract void Activate();
		public abstract void Deactivate();
	}
}
