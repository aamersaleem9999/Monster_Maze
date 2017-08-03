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
    public GameObject healthBar;
    private GameObject player;
    private float hbMaxScale = 0f;
    private AllyContainer currAlly = null;
	public string Opponent;

	void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponent<Animator>();
		//this.anim.enabled = false;
        player = GameObject.FindGameObjectWithTag("player").gameObject;
        this.hbMaxScale = this.healthBar.transform.localScale.y;
    }

    public void setContainer(EnemyContainer ec)
    {
        this.currEnemy = ec;
    }

    public void takeDamage(int amount)
    {
        this.currEnemy.takeDamage(amount);
    }

    private void updateHealth()
    {
        float percent = this.currEnemy.getPercentHealthLeft();
        Vector3 hbScale = this.healthBar.transform.localScale;
		//this.anim.enabled = false;
        this.healthBar.transform.localScale = new Vector3(hbScale.x, this.hbMaxScale * percent, hbScale.z);
		//this.anim.enabled = true;
	}
    
    private void despawn()
    {
        sceneManager.Destroy(this.gameObject);
    }
		
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag != Opponent)
			return;
		Debug.Log ("enemy Hit!");
		currEnemy.takeDamage (5);

	}
																					//Attack Opponent Function
    private void attackOpponent()
    {

//		Debug.Log (" in attackOpponent");
     
        int amount = Random.Range(4, 10);

       // this.currAlly.takeDamage(amount);
		if (this.currAlly != null) {
			if (!this.currAlly.isDead ()) {
				// Debug.Log(" Attacking ally in attackOpponenet method ==> : ");
				Invoke ("attackOpponent", 0.5f);
				//Calling attack opponent function recursively is ally is not dead
			}
		}
      }




    void Update ()
	{
		if(this.currEnemy.isDead())
        {
            this.currAlly = null;												//Assigns enemy attacking character to null if the ally character is dead.

            																	//Death animation and despawn
            this.agent.enabled = false;
            sceneManager.Kill(this.gameObject, this.anim);
            this.healthBar.GetComponent<MeshRenderer>().enabled = false;
          																		 //Remove ally character from the scene
            Invoke("despawn", 2.5f);
        }

        else 																	// If the ally character is dead find another ally character for enemy to attack.
        {
			// consitional check on trigger then update health

            this.updateHealth();

			// 
            AllyContainer closest = sceneManager.findClosestAlly(this.gameObject);
								
            if (closest != null) {
                this.currAlly = closest;										//Current ally character to attack

                if (Vector3.Distance(this.transform.position, closest.getGO().transform.position) > 6.2)
                {													
																			// Making enemy character walk towards ally character to attack
					transform.GetComponent<NavMeshAgent> ().Resume();		// Toggles NavMesh ON/Off																	
                    agent.SetDestination(closest.getGO().transform.position);
                    sceneManager.setMovementAnimation(this.anim, this.gameObject, closest);

                }

                else if(!this.currAlly.isDead())
                    {
                    sceneManager.setMovementAnimation(this.anim, this.gameObject, closest);
					transform.GetComponent<NavMeshAgent> ().Stop();			// Toggles NavMesh ON/Off	
					this.attackOpponent();
                }
            }
			else 																//If all the ally characters are dead; go and kill the player.	
            {
              this.goKillPlayer();
            }

        } 
    }
	private void goKillPlayer()
    {

        if (Vector3.Distance(this.transform.position, player.transform.position) > 2.2)
        {
            //Debug.Log("  In gokillplayer method: value of player transform position "+(this.transform.position- player.transform.position));
            agent.SetDestination(player.transform.position);
            sceneManager.setMovementToPlayer(this.anim, this.gameObject, player);//Walking towards player
        }

        else
        {
            // Debug.Log("crap");
			sceneManager.setMovementToPlayer(this.anim, this.gameObject, player);
            this.attackPlayer();												//Attacking Player

            
        }
        
    }

    private void attackPlayer()
    {
        int amount = Random.Range(5, 10);										//Attack damage for player

        playerStats.takeDamage(amount);

        if(!playerStats.isDead())
		{
            Invoke("attackPlayer",3);
        }

		else
        {
            																	//Player is dead
            playerStats.despawn();
        }
        
    }

}
