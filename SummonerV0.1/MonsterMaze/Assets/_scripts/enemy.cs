using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class enemy : MonoBehaviour
{
    private EnemyContainer currEnemy;
    private NavMeshAgent agent;
    private Animator anim;
    private GameObject player;
    private AllyContainer currAlly = null;
    public string Opponent;
    public CustomPlayer customPlayer;

    [SerializeField]
    private Image HealthB;
    private float Maxhealth;

    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("player").gameObject;
        customPlayer = this.GetComponent<CustomPlayer>();
        Maxhealth = currEnemy.getMaxHP();
    }

    public void setContainer(EnemyContainer ec)
    {
        this.currEnemy = ec;
    }

    public void takeDamage(int amount)
    {
        this.currEnemy.takeDamage(amount);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != Opponent)
            return;
        //Debug.Log("enemy Hit!");
        currEnemy.takeDamage(Random.Range(4, 10));
       // Debug.Log(" Curr hp of Enemy " + currEnemy.getCurrHP());
        HealthB.fillAmount = (currEnemy.getCurrHP()) / Maxhealth;
    }

    void Update()
    {
        if (this.currEnemy.isDead())
        {
            this.currAlly = null;		    									//Death animation and despawn
            this.agent.enabled = false;
            sceneManager.Death(this.gameObject, this.anim);                     //Remove ally character from the scene
            Destroy(this.gameObject,2.5f);
        }
        
        else 																// If the ally character is dead find another ally character for enemy to attack.
        {
             AllyContainer closest = sceneManager.findClosestAllyTesting(this.gameObject);

            if (closest != null)
            {
                this.currAlly = closest;                            //Current ally character to attack

                //Attack player if under 6 units
                if (Vector3.Distance(this.transform.position, player.transform.position) < 6)
                {
                    this.agent.isStopped = false;
                    sceneManager.gokillPlayer(anim, this.agent, this.gameObject, closest);
                }

                else if ((Vector3.Distance(this.transform.position, closest.getGO().transform.position) > 4.2) && ((Vector3.Distance(this.transform.position, player.transform.position) > 10)))
                {
                    // Making enemy character walk towards ally character && player to attack
                    this.agent.isStopped = false;
                    agent.SetDestination(closest.getGO().transform.position);
                    sceneManager.setMovementAnimation(this.anim, this.gameObject, closest);
                } 
                
                else if (!this.currAlly.isDead())
                {
                    sceneManager.setMovementAnimation(this.anim, this.gameObject, closest);
                    this.agent.isStopped = true;
                }
            }
            else 																//If all the ally characters are dead; go and kill the player.	
            {
                this.agent.isStopped = false;
                sceneManager.gokillPlayer(anim, this.agent, this.gameObject, closest);
            }
        }
    }
}
