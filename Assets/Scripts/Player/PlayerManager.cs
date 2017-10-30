using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag 
{		
	public class PlayerManager : MonoBehaviour {
		public Player CurrentShape;
		private int m_activeIndex = 0;
		private Rigidbody2D m_rb2D;
		private List<Player> m_players = new List<Player>();

		// Use this for initialization
		// Update is called once per frame
		void Start()
		{
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
		public void NextPlayer()
		{
			changePlayer ((m_activeIndex + 1) % m_players.Count);
		}
		private void changePlayer(int index) {
			m_activeIndex = index;
			CurrentShape = m_players [m_activeIndex];
			m_rb2D = CurrentShape.GetComponent<Rigidbody2D> ();
		}
	}
}