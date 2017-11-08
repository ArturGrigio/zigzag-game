using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Huy
{
	/// <summary>
	/// Handle the titan size skill.
	/// </summary>
	public class TitanFormSkill : Skill
	{
		#region Public Variables

		/// <summary>
		/// The scaled size of the titan form.
		/// </summary>
		[Tooltip("The scaled size of the titan form")]
		public float Size = 2f;

		/// <summary>
		/// The speed of the titan-size sphere.
		/// </summary>
		[Tooltip("The speed of the titan-size sphere")]
		public float Speed = 1f;

		/// <summary>
		/// The mass of the titan form.
		/// </summary>
		[Tooltip("The mass of the titan form")]
		public float Mass = 2f;

		/// <summary>
		/// The number of particles to emit when transform
		/// </summary>
		[Tooltip("The number of particles to emit when transform")]
		public int ParticleCount = 100;

		/// <summary>
		/// Reference to the error Text component.
		/// </summary>
		[Tooltip("Reference to the error Text component")]
		public Text ErrorText;

		#endregion

		#region Private Variables

		/// <summary>
		/// The original scale of the sphere.
		/// </summary>
		private Vector3 m_originalScale;

		/// <summary>
		/// The original mass of the sphere.
		/// </summary>
		private float m_originalMass;

		/// <summary>
		/// The circle collider 2D component.
		/// </summary>
		private CircleCollider2D m_circleCollider2D;

		/// <summary>
		/// Flag indicating whether the display error courtine is running.
		/// </summary>
		private bool m_errorCoroutineRunning;

		/// <summary>
		/// The particle system component.
		/// </summary>
		private ParticleSystem m_particleSystem;

		#endregion

		#region Public Methods

		/// <summary>
		/// Activate the skill and begin any sort of animations or movements.
		/// </summary>
		public override bool Activate ()
		{
//			if (m_isEnabled)
//			{
//				StartCoroutine (growCoroutine ());
//			}
			return false;
		}

		/// <summary>
		/// Deactivate the skill and stop the player during the skill executation
		/// or when the skill is done executing.
		/// </summary>
		public bool Deactivate ()
		{
			m_isActive = false;

			// Prevent accidental shrinking beyond the original size
			if (m_originalScale != transform.localScale)
			{
//				m_rigidbody2D.velocity = new Vector2 (0f, m_rigidbody2D.velocity.y);
//				transform.localScale /= Size;
//
//				m_rigidbody2D.mass = m_originalMass;
			}

			return false;
		}

		#endregion

		#region Unity Methods

		/// <summary>
		/// Initialize member variables.
		/// </summary>
		protected override void Awake ()
		{
			base.Awake ();

//			m_skillType = SkillTypeEnum.Attack;
			m_originalScale = transform.localScale;
			m_circleCollider2D = GetComponent<CircleCollider2D> ();

			m_errorCoroutineRunning = false;
//			m_originalMass = m_rigidbody2D.mass;
			m_particleSystem = GetComponentInChildren<ParticleSystem> ();
		}

		/// <summary>
		/// Update physic movement in the titan form.
		/// </summary>
		private void FixedUpdate()
		{
//			if (m_isActive && m_isEnabled)
//			{
//				float velocityX = (m_player.FacingRight) ? Speed : -Speed;
//				m_rigidbody2D.velocity = new Vector2 (velocityX, m_rigidbody2D.velocity.y);
//			}
		}

		#endregion

		#region Private/Protected Methods

		/// <summary>
		/// The grow coroutine in which transform the sphere
		/// into titan form.
		/// </summary>
		/// 
		/// <returns>The coroutine.</returns>
		private IEnumerator growCoroutine()
		{
			// Set the active flag and get the position in titan form
			m_isActive = true;
			Vector2 titanFormPosition = transform.position + new Vector3 (0f, Size / 2f, 0f);
			float radius = m_circleCollider2D.radius * Size;

			// Check if there are any colliders in all directions
			RaycastHit2D raycast = Physics2D.CircleCast (titanFormPosition, radius, Vector2.one);
			if (raycast.collider != null)
			{
				// Do not enlarge the sphere if there is not enough space to do it
				if (raycast.distance < radius)
				{
					if (!m_errorCoroutineRunning)
					{
						StartCoroutine (displayErrorCoroutine ());
					}

					Deactivate ();
					yield break;
				}
			}
				
			m_particleSystem.Emit (ParticleCount);

			transform.position = titanFormPosition;
			transform.localScale *= Size;
//			m_rigidbody2D.mass = Mass;

			yield return new WaitForSeconds (5.0f);
			Deactivate ();
		}

		private IEnumerator displayErrorCoroutine()
		{
			m_errorCoroutineRunning = true;
			ErrorText.text = "Error: Not enough space to transform";
			ErrorText.transform.parent.gameObject.SetActive (true);

			yield return new WaitForSeconds (3f);

			ErrorText.transform.parent.gameObject.SetActive (false);
			m_errorCoroutineRunning = false;

			// Fade out effect...
//			while (ErrorText.color.a > 0f)
//			{
//				ErrorText.color = new Color (ErrorText.color.r, ErrorText.color.g, ErrorText.color.b, ErrorText.color.a - (Time.deltaTime / 1f));
//				yield return null;
//			}
		}

		#endregion
	}
}
