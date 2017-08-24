using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class hunt : MonoBehaviour
{
    private NavMeshAgent navAgent;

    // Use this for initialization
    void Start ()
    {
        this.navAgent = this.GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //this.navAgent.SetDestination(sceneManager.findClosestEnemy(this.gameObject).transform.position);
	}
}
