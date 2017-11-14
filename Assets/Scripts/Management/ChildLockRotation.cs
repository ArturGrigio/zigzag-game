using UnityEngine;
using System.Collections;

namespace ZigZag
{
	/// <summary>
	/// This class prevents a child object to rotate with its parent object.
	/// </summary>
	public class ChildLockRotation : MonoBehaviour
	{
		private Quaternion m_rotation;

		private void Awake()
		{
			m_rotation = transform.rotation;
		}

		private void LateUpdate()
		{
			transform.rotation = m_rotation;
		}
	}
}
