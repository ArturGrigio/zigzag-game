using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag 
{		
	public class PlayerManager : MonoBehaviour {

		#region Public Variables

		[Tooltip("Sets the starting player object when set in a scene. Indicates the active player object at runtime.")]
		public Player CurrentShape;

		#endregion

		#region Private/Protected Variables

		private int m_activeIndex = 0;

		private Rigidbody2D m_rb2D;

		private List<Player> m_players = new List<Player>();

		#endregion

		#region Unity Methods

		// Use this for initialization
		// Update is called once per frame
		void Start()
		{
			//Disable collision between player objects and set active player
			foreach (Transform t in transform) {
				Player player = t.gameObject.GetComponent<Player> ();
				if (player != null) 
				{
					for (int i = 0; i < m_players.Count; ++i) 
					{
						Physics2D.IgnoreCollision (m_players [i].GetComponent<Collider2D> (), player.GetComponent<Collider2D> ());
					}
					if (player == CurrentShape) 
					{
						m_activeIndex = m_players.Count;
					}
					m_players.Add (player);
				}
			}
			changePlayer (m_activeIndex);
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Changes the active player object to the next in the list.
		/// Rotates back to the first object when the end is reached.
		/// </summary>
		public void NextPlayer()
		{
			changePlayer ((m_activeIndex + 1) % m_players.Count);
		}

		#endregion

		#region Private/Protected Methods

		/// <summary>
		/// Sets the active player object.
		/// </summary>
		/// <param name="index">Index of desired player object.</param>
		private void changePlayer(int index) {
			m_activeIndex = index;
			CurrentShape = m_players [m_activeIndex];
			m_rb2D = CurrentShape.GetComponent<Rigidbody2D> ();
		}

		#endregion
	}
}