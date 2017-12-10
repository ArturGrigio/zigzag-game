using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag
{
	/// <summary>
	/// Activator types: Axis receives axis input. The remaining three are button activation.
	/// Instant performs an action immediately and sets the skill inactive.
	/// Toggle will activate an inactive skill or cancel an active skill when the activator is triggered.
	/// Hold will activate a skill when the activator is triggered and cancel when the activator is absent.
	/// </summary>
	public enum SkillTypes {Axis, Instant, Toggle, Hold, Passive};

	[RequireComponent (typeof (Agent))]
	public abstract class Skill : MonoBehaviour 
	{
		#region Public Variables


		#endregion

		#region Private/Protected Variables

		private Agent m_agent;

		[SerializeField]
		[Tooltip("Designator used to activate a skill (ie: name of input used for player skills).")]
		protected string m_activator = "";

		protected SkillTypes m_skillType;

		protected bool m_canCancel = false;
		protected bool m_isActive = false;
		protected bool m_allowMovement = false;

		#endregion

		#region Properties

		/// <summary>
		/// Gets a value indicating whether the skill is active.
		/// </summary>
		/// <value><c>true</c> if this skill is active; otherwise, <c>false</c>.</value>
		public bool IsActive
		{
			get { return m_isActive; }
		}

		/// <summary>
		/// Gets a value indicating whether this skill can be activated.
		/// </summary>
		/// <value><c>true</c> if this instance can activate; otherwise, <c>false</c>.</value>
		public virtual bool CanActivate 
		{ 
			get { return false; } 
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="ZigZag.Skill"/> allows movement skills while it's active.
		/// </summary>
		/// <value><c>true</c> if allow movement; otherwise, <c>false</c>.</value>
		public bool AllowMovement
		{
			get { return m_allowMovement; }
		}

		/// <summary>
		/// Gets a value indicating whether this skill can be cancelled before completion.
		/// </summary>
		/// <value><c>true</c> if this skill can cancel; otherwise, <c>false</c>.</value>
		public bool CanCancel
		{
			get { return m_canCancel; }
		}

		/// <summary>
		/// String determining how the skill is activated.
		/// </summary>
		/// <value>For example, the name of the input used to activate a player skill.</value>
		public string Activator
		{
			get { return m_activator; }
		}

		/// <summary>
		/// Gets the activator type of the skill.
		/// </summary>
		/// <value>The type of the activator.</value>
		public SkillTypes SkillType
		{
			get { return m_skillType; }
		}

		/// <summary>
		/// Gets the agent component.
		/// </summary>
		/// <value>The agent component.</value>
		protected Agent AgentComponent
		{
			get { return m_agent; }
		}

		#endregion

		#region Public Methods

		public virtual bool Activate ()
		{
			return false;
		}

		public virtual bool ActivateAxis(float axis)
		{
			return false;
		}

		public virtual bool Cancel()
		{
			return false;
		}

		#endregion

		#region Private/Protected Methods

		protected void dealDamage(Health recipient, float damage)
		{
			if (recipient != null)
			{
				recipient.ReceiveDamage (damage);
			}
		}

		#endregion

		#region Unity Methods
		protected virtual void Awake()
		{
			m_agent = GetComponent<Agent> ();
		}
		#endregion
	}
}