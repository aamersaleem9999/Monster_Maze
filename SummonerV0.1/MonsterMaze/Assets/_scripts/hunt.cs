using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class hunt : MonoBehaviour
{
	private NavMeshAgent navAgent;

	//Locate the character to attack on the map.
	void Start ()
	{
		this.navAgent = this.GetComponent<NavMeshAgent>();
	}

}
