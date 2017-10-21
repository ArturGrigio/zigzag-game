using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour, IMovement
{
	public Transform playerTransform;
	private NavMeshAgent m_navMeshAgent;

	private void Awake()
	{
		m_navMeshAgent = GetComponent<NavMeshAgent> ();
	}

	public void Update()
	{
		Move (0f);
	}

	public void Move(float velocityX)
	{
		m_navMeshAgent.SetDestination (playerTransform.position);
	}

	public void Jump(bool jumpFlag)
	{
	}
}
