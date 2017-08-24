using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ally : MonoBehaviour
{

    private AllyContainer currAlly;
    private NavMeshAgent agent;
    private Animator anim;
    private GameObject player;
    private EnemyContainer currEnemy = null;
    private bool isFighting = false;
    public string Opponent;
    [SerializeField]
    private Image HealthB;
    private float Maxhealth;

    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("player").gameObject;
        Maxhealth = currAlly.getMaxHP();
    }

    public void setContainer(AllyContainer ac)
    {
        this.currAlly = ac;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != Opponent)
            return;
       //  Debug.Log("Ally Hit!");                        //Takes Damage
        // Debug.Log(" Curr hp of Ally " + currAlly.getCurrHP());
        currAlly.takeDamage(Random.Range(4, 10));
        HealthB.fillAmount = (currAlly.getCurrHP()) / Maxhealth;   //HealthBar FillAmount
    }

    void Update()
    {
        if (this.currAlly.isDead())
        {
            this.currEnemy = null;
            sceneManager.Death(this.gameObject, this.anim);
            Destroy(this.gameObject,2.5f);                       //Death animation and despawn
        }
        else
        {
            EnemyContainer closest = sceneManager.findClosestEnemy(this.gameObject);

            if (closest != null)
            {
                this.currEnemy = closest;

                if (Vector3.Distance(closest.getGO().transform.position, this.transform.position) > 3.2)
                {
                    //transform.GetComponent<NavMeshAgent>().Resume();        // Toggles NavMesh ON/Off
                    this.agent.isStopped = false;
                    agent.SetDestination(closest.getGO().transform.position + new Vector3(0.0f,0.0f,2.0f));
                    sceneManager.setMovementAnimationally(this.anim, this.gameObject, closest);
                }
                else if (!this.currEnemy.isDead())
                {
                    sceneManager.setMovementAnimationally(this.anim, this.gameObject, closest);
                    this.agent.isStopped = true;
                    //transform.GetComponent<NavMeshAgent>().Stop();          // Toggles NavMesh ON/Off	
                }
            }
            else
            {
                this.agent.isStopped = false;
               // transform.GetComponent<NavMeshAgent>().Resume();
                sceneManager.gotoPlayer(anim, this.agent, this.gameObject);
            }
        }
    }
}

