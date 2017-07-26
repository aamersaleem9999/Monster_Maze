using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ally : MonoBehaviour {

    private AllyContainer myContainer;
    private NavMeshAgent agent;
    private NavMeshObstacle obs;
    private Animator anim;
    public GameObject healthBar;
	public string Opponent;
    private float hbMaxScale = 0f;

    private EnemyContainer currEnemy = null;
    private bool isFighting = false;


    // Use this for initialization
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        obs = this.GetComponent<NavMeshObstacle>();
        anim = this.GetComponent<Animator>();
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

	}
    
	/*void onTriggerEnter(Collider c){
	
		Debug.Log (" In ally cs :- ");
		if(c.gameObject.tag==this.opponent ){
			myContainer.takeDamage (5);
		}
	}*/

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

    // Update is called once per frame
    void Update()
    {
        //am I free to hunt?
        //should I die now?
        if (this.myContainer.isDead())
        {
            this.currEnemy = null;

            //kill myself
          //  this.agent.enabled = false;
            sceneManager.Kill(this.gameObject, this.anim);
            this.healthBar.GetComponent<MeshRenderer>().enabled = false;

            //remove from scene soon
            Invoke("despawn", 1f);
        }
        else
        {
            this.updateHealth();

            EnemyContainer closest = sceneManager.findClosestEnemy(this.gameObject);
                   
			//Debug.Log (" In ally controller currrent enemy  "+closest);
            if (closest != null)
			{
                this.currEnemy = closest;

//                Debug.Log(" current skeleton name"+this.currEnemy.ToString());

                if (Vector3.Distance(closest.getGO().transform.position, this.transform.position) > 4.2)
                {
                    this.isFighting = false;

//					Debug.Log (" agent in ally "+agent);
                    agent.SetDestination(closest.getGO().transform.position);
                }
                else if (!isFighting && !this.currEnemy.isDead())
                {
                    this.attack();
                }
                //sceneManager.setMovementAnimation(this.anim, this.gameObject, closest);
            }
            else
            {
                sceneManager.gotoPlayer(this.agent);
            }
        }
    }
}

