using UnityEngine;
using System.Collections.Generic;

namespace ZigZag
{
	public class SkillManager : MonoBehaviour
	{
		public GameObject currentShape;
		private List<ISkill> m_skills;

		// Use this for initialization
		void Awake ()
		{
			m_skills = new List<ISkill> (currentShape.GetComponents<ISkill> ());
		}
	}
}
