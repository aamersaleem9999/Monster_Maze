using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ally : MonoBehaviour {

    private AllyContainer myContainer;
	private NavMeshAgent agent;
   // private NavMeshObstacle obs;
    private Animator anim;
    public GameObject healthBar;
	private GameObject player;
    private float hbMaxScale = 0f;
    private EnemyContainer currEnemy = null;
    private bool isFighting = false;
	public string Opponent;


    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
		anim = this.GetComponent<Animator>();

		//InvokeRepeating("takeShit", 2f,2f);;

      //  obs = this.GetComponent<NavMeshObstacle>();
		this.hbMaxScale = this.healthBar.transform.localScale.y;
		player = GameObject.FindGameObjectWithTag("player").gameObject;

    }
	public void takeShit( ){
		
		myContainer.takeDamage (5);

	}
    public void setContainer(AllyContainer ac)
    {
        this.myContainer = ac;
    }

    private void despawn()
    {
        sceneManager.Destroy(this.gameObject);
    }

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag != Opponent)
			return;
		Debug.Log ("Ally Hit!");
		myContainer.takeDamage (5);
	/*	currHP=myContainer.getCurrHP();
		currHP -= 5;
		maxHP=myContainer.getMaxHP();
		healthBar1.transform.localScale = new Vector3 ((currHP/maxHP),1,1);*/
	}

	    private void attack()
    {
        //attack the enemy and then try to attack again
        this.isFighting = true;
        int amount = Random.Range(4, 10);
        // Debug.Log("current closest Enemy random HP Value in Ally Script : " + amount);

        // foreach(EnemyContainer ec in enem)
        // Debug.Log(" in attack Ally script--> Enemy object "+this.currEnemy.ToString());

        /// this.currEnemy.takeDamage(amount);
        if(!this.currEnemy.isDead())
        {
            Invoke("attack", 0.5f);
        }
        else
        {
            this.isFighting = false;
        }
    }

    private void updateHealth()
    {
        float percent = this.myContainer.getPercentHealthLeft();
        Vector3 hbScale = this.healthBar.transform.localScale;

        this.healthBar.transform.localScale = new Vector3(hbScale.x, this.hbMaxScale * percent, hbScale.z);


	}

    void Update()
    {
      
        if (this.myContainer.isDead())
        {
			this.currEnemy = null;
            //sceneManager.Kill(this.gameObject, this.anim);
            this.healthBar.GetComponent<MeshRenderer>().enabled = false;

            //remove from scene soon
            Invoke("despawn", 2.5f);
        }
        else
        {
			// consitional check on trigger then update health

            this.updateHealth();
			// 
            
			EnemyContainer closest = sceneManager.findClosestEnemy(this.gameObject);
                   
			//Debug.Log (" In ally controller currrent enemy  "+closest);
			if (closest != null) {
				this.currEnemy = closest;
//                Debug.Log(" current skeleton name"+this.currEnemy.ToString());

				if (Vector3.Distance (closest.getGO ().transform.position, this.transform.position) > 2.2) {
					this.isFighting = false;
					transform.GetComponent<NavMeshAgent> ().Resume ();		// Toggles NavMesh ON/Off
//					Debug.Log (" agent in ally "+agent);
					agent.SetDestination (closest.getGO ().transform.position);
					sceneManager.setMovementAnimationally (this.anim, this.gameObject, closest);
				} else if (!isFighting && !this.currEnemy.isDead ()) {
					transform.GetComponent<NavMeshAgent> ().Stop ();			// Toggles NavMesh ON/Off	
					this.attack ();
				}
			} else 
            {
                //sceneManager.gotoPlayer(this.agent);
			}
        }
    }
}

