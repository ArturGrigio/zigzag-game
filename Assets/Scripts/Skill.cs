using System;
using UnityEngine;

namespace ZigZag
{
	/// <summary>
	/// Abstract base class for all derived Skill classes.
	/// </summary>
	public abstract class Skill : MonoBehaviour
	{
		#region Public Variables

		/// <summary>
		/// The name of the air skill.
		/// </summary>
		[Tooltip("Name of the skill")]
		public string skillName = "skill";

		#endregion

		#region Private/Protected Variables
		/// <summary>
		/// Flag indicating if the character is on the ground or not.
		/// </summary>
		protected bool m_isGrounded;

		/// <summary>
		/// Flag indicating if the skill is currently being executed.
		/// </summary>
		protected bool m_isActive;

		/// <summary>
		/// Flag indicating if the skill is usable or not.
		/// </summary>
		protected bool m_isEnabled;

		/// <summary>
		/// The type of the skill.
		/// </summary>
		protected SkillTypeEnum m_skillType;

		/// <summary>
		/// The 2D rigidbody componenet.
		/// </summary>
		protected Rigidbody2D m_rigidbody2D;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets a value indicating whether the skill is active.
		/// </summary>
		public bool IsActive 
		{
			get { return m_isActive; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether the skill is enabled.
		/// </summary>
		public bool IsEnabled 
		{
			get { return m_isEnabled; }
			set { m_isEnabled = value; }
		}

		/// <summary>
		/// Gets a value indicating whether the character is grounded.
		/// </summary>
		public bool IsGrounded 
		{
			get { return m_isGrounded; }
		}

		/// <summary>
		/// Gets the type of the skill.
		/// </summary>
		/// <value>The type of the skill.</value>
		public SkillTypeEnum SkillType 
		{
			get { return m_skillType; }
		}

		#endregion

		#region Public Methods
		/// <summary>
		/// Activate the skill and begin any sort of animations or movements.
		/// </summary>
		public abstract void Activate();

		/// <summary>
		/// Deactivate the skill and stop the player during the skill executation
		/// or when the skill is done executing.
		/// </summary>
		public abstract void Deactivate();

		#endregion

		#region Unity Methods

		/// <summary>
		/// Initialize member variables.
		/// </summary>
		protected virtual void Awake()
		{
			m_isActive = false;
			m_isEnabled = false;
			m_isGrounded = true;
			m_rigidbody2D = GetComponent<Rigidbody2D> ();
		}

		#endregion
	}
}