using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Huy 
{

	[RequireComponent (typeof (Agent))]
	public abstract class Skill : MonoBehaviour 
	{
		#region Public Variables
		public enum ActivatorModes {Activate, Toggle, Hold};
		public enum ActivatorTypes {Button, Axis};
		#endregion

		#region Private/Protected Variables

		private Agent m_agent;

		[SerializeField]
		protected string m_activator = "";

		protected ActivatorModes m_activatorMode = ActivatorModes.Activate;
		protected ActivatorTypes m_activatorType = ActivatorTypes.Button;

		protected bool m_canCancel = false;
		protected bool m_isActive = false;

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
		/// <value>For example, the name of the button used to activate a player skill.</value>
		public string Activator
		{
			get { return m_activator; }
		}

		public ActivatorModes ActivatorMode
		{
			get { return m_activatorMode; }
		}

		public ActivatorTypes ActivatorType
		{
			get { return m_activatorType; }
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

		public virtual void Continue ()
		{
			throw new NotImplementedException ();
		}

		public virtual bool Cancel()
		{
			if (m_canCancel == false)
				return false;
			m_isActive = false;
			return true;
		}

		#endregion

		#region Private/Protected Methods
		#endregion

		#region Unity Methods
		protected virtual void Awake()
		{
			m_agent = GetComponent<Agent> ();
		}
		#endregion
	}
}