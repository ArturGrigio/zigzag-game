using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ZigZag {

	public enum Surface {Ground, Ceiling, LeftWall, RightWall, Other}

	[RequireComponent(typeof(Collider2D))]
	public class SurfaceDetector : MonoBehaviour
	{

		#region Public Variables

		public delegate void SurfaceEnterHandler(Collision2D collision, Surface surface);
		public delegate void SurfaceExitHandler(Collision2D collision, Surface surface);

		public event SurfaceEnterHandler OnSurfaceEnter;
		public event SurfaceExitHandler OnSurfaceExit;

		[Tooltip("Trigger events even when the object is already in contact with another surface of the same type")]
		public bool TriggerDuplicateSurfaces = false;

		[Tooltip("Determines which layers count as part of a surface")]
		public LayerMask Layers = 0x800; //Default to Layer 11 (Foreground)
		#endregion

		#region Private/Protected Variables
		private int[] m_surfaceCount;
		private Dictionary<Collider2D, Surface> m_collisions;
		#endregion

		#region Properties
		#endregion

		#region Public Methods

		/// <summary>
		/// Get the collied surface based on the given normal vector.
		/// </summary>
		/// 
		/// <param name="normal">
		/// Normal vector.
		/// </param>
		/// 
		/// <returns>
		/// The surface which is identified by the normal vector
		/// <returns>
		public static Surface SurfaceFromNormal(Vector2 normal)
		{
			//Debug.Log ("Check normal: " + normal.ToString ());
			if (normal == Vector2.up)
			{
				return Surface.Ground;
			}
			else if (normal == Vector2.down)
			{
				return Surface.Ceiling;
			}
			else if (normal == Vector2.left)
			{
				return Surface.LeftWall;
			}
			else if (normal == Vector2.right)
			{
				return Surface.RightWall;
			}

			return Surface.Other;
		}

		public static Surface SurfaceFromCollision2D(Collision2D collision)
		{
			ContactPoint2D[] contact = new ContactPoint2D[2];
			collision.GetContacts (contact);

			return SurfaceDetector.SurfaceFromNormal (contact[0].normal);
		}

		public bool IsOnSurface(Surface surface)
		{
			return m_surfaceCount[(int)surface] > 0;
		}
		#endregion

		#region Private/Protected Methods
		private void initSurfaces()
		{
			m_surfaceCount = new int[System.Enum.GetValues (typeof(Surface)).Length];
		}
		#endregion

		#region Unity Methods
		protected virtual void Awake()
		{
			m_collisions = new Dictionary<Collider2D, Surface> ();
			initSurfaces ();
		}

		void OnCollisionEnter2D(Collision2D collision)
		{
			if (0 !=  (Layers & (1 << collision.gameObject.layer)))
			{
				Surface surface = SurfaceFromCollision2D (collision);
				bool isNewSurface = m_surfaceCount [(int)surface] == 0 ? true : false;
				++m_surfaceCount [(int)surface];
				m_collisions [collision.collider] = surface;
				//Debug.Log ("Surface Enter (" + collision.gameObject.name + "): surface=" + surface.ToString () + ", count=" + m_surfaceCount [(int)surface]);
				if (OnSurfaceEnter != null && (isNewSurface || TriggerDuplicateSurfaces == false))
				{
					OnSurfaceEnter.Invoke (collision, surface);
				}
			}
		}

		void OnCollisionExit2D(Collision2D collision)
		{
			if (0 != (Layers & (1 << collision.gameObject.layer)))
			{
				Surface surface = m_collisions [collision.collider];
				m_collisions.Remove (collision.collider);
				--m_surfaceCount [(int)surface];
				bool isLastSurface = m_surfaceCount [(int)surface] == 0 ? true : false;
				//Debug.Log ("Surface Exit: surface=" + surface.ToString () + ", count=" + m_surfaceCount [(int)surface]);
				if (OnSurfaceExit != null && (TriggerDuplicateSurfaces || isLastSurface))
				{
					OnSurfaceExit.Invoke (collision, surface);
				}
			}
		}
		#endregion
	}
}
