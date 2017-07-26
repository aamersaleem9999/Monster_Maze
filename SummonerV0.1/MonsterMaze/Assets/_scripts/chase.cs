using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class chase : MonoBehaviour
{
    public Transform player;
    private Animator anim;
    private int walkHash;
    private int attackHash;
    private int idleHash;
    private NavMeshAgent navAgent;

	// Use this for initialization
	void Start ()
    {
        this.navAgent = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponent<Animator>();
        this.walkHash = Animator.StringToHash("isWalking");
        this.attackHash = Animator.StringToHash("isAttacking");
        this.idleHash = Animator.StringToHash("isIdle");
    }

    // Update is called once per frame
    void Update ()
    {
        Vector3 direction = this.player.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);
        if (Vector3.Distance(this.player.position, this.transform.position) < 20)
        {

            direction.y = 0;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.5f);
            if (direction.magnitude > 2.2)
            {
                this.navAgent.enabled = true;
                anim.SetTrigger(this.walkHash);
                this.navAgent.SetDestination(this.player.position);
            }
            else
            {
                //this.transform.LookAt(this.player);
                this.navAgent.enabled = false;
                //this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.LookRotation(direction), 1f);
                anim.SetTrigger(this.attackHash);
            }
        }
        else
        {
            anim.SetTrigger(this.idleHash);
        }
	}
}
