using UnityEngine;
using System.Collections.Generic;

namespace Huy
{
	public class EnemyManager : MonoBehaviour
	{
		public Vector2 blueSlimeSpawnPosition;
		public Vector2 yellowSlimeSpawnPosition;
		public Vector2 greenSlimeSpawnPosition;
		public Vector2 redSlimeSpawnPosition;

		private List<Enemy> m_blueSlimes;
		private List<Enemy> m_yellowSlimes;
		private List<Enemy> m_greenSlimes;
		private List<Enemy> m_redSlimes;

		private void Awake()
		{
			m_blueSlimes = new List<Enemy> ();
			m_yellowSlimes = new List<Enemy> ();
			m_greenSlimes = new List<Enemy> ();
			m_redSlimes = new List<Enemy> ();
		}

		public void SpawnBlueSlime()
		{
		}

		public void SpawnYellowSlime()
		{
		}

		public void SpawnGreenSlime()
		{
		}

		public void SpawnRedSlime()
		{
		}
	}
}