using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;



public class CustomPlayer : MonoBehaviour
{
    public List<Ally> Ally;
    public GameObject objToSpawn1;
    public GameObject objToSpawn2;  //Character spawn objects
    public GameObject objToSpawn3;
    public GameObject allyPopUP;
    public bool popUp = false;

    private int spawnCount1 = 0;
    private int spawnCount2 = 0;
    private int spawnCount3 = 0;

    [SerializeField]
    private playerStats health;
    [SerializeField]                //Player Status
    private playerStats mana;
    [SerializeField]
    private playerStats power;

    public Hand leftHand;
    public Hand rightHand;
    Player player;

    void Start()
    {
        sceneManager.player = this.gameObject;
        player = Player.instance;
    }

    private void Awake()
    {
        health.Initialize();
        mana.Initialize();          //Initializing Health, Mana and Power
        power.Initialize();
    }

    void Update()
    {
        foreach (Ally n in Ally)
        {
             
            if (mana.CurrentVal <= 0)
            {
                foreach (Ally a in Ally)
                {
                    a.allyIcon.fillAmount = 0;
                }
                return;
            }
            if (n.currentCooldown < n.coolDown)
            {
                n.currentCooldown += Time.deltaTime;
                n.allyIcon.fillAmount = n.currentCooldown / n.coolDown; //Fill amount
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            mana.CurrentVal -= 10;//For MP
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            power.CurrentVal -= 10;//For Power                       //////***Testing
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            health.CurrentVal -= 10;//For Hp
        }

    }
    //-----------------------------------------------------------------------------------

    void OnTriggerEnter(Collider other)
    {
        if (!(other.gameObject.tag == "enemy"))
            return;

        health.CurrentVal-=(Random.Range(4, 10));               //player takes damage  
        if (health.CurrentVal <= 0)
        {                                                       //Taking Value for TutorialText Display
            tutorialText.playerDead = true;
        }                                               
    }
    //-----------------------------------------------------------------------------------

    public void AllySpawner(string other)
    {
                                                                    // tagname of ally button - to spawn respective ally
        if (other.Equals("spawnally1") && Ally[0].currentCooldown >= Ally[0].coolDown && !(mana.CurrentVal<=0))
        {
            {
                Debug.Log(" inside ally spawner1 ");

                Ally[0].currentCooldown = 0;
                UpdatePlayerStats("spawnally1");
                spawnAlly("spawnally1");
            }
        }

        else if (other.Equals("spawnally2") && Ally[1].currentCooldown >= Ally[1].coolDown && !(mana.CurrentVal <= 0))
        {
            Ally[1].currentCooldown = 0;

            UpdatePlayerStats("spawnally2");
            spawnAlly("spawnally2");
        }

        else if (other.Equals("spawnally3") && Ally[2].currentCooldown >= Ally[2].coolDown && !(mana.CurrentVal <= 0))
        {
            Ally[2].currentCooldown = 0;

            UpdatePlayerStats("spawnally3");
            spawnAlly("spawnally3");	
        }
    }
   //-------------------------------------------------------------------------------------------

    public void UpdatePlayerStats(string s)
    {
        if (s.Equals("spawnally1"))
        {
            mana.CurrentVal -= 10;
        }
        else if (s.Equals("spawnally2"))
        {                                       //Mana Update
            mana.CurrentVal -= 15;
        }
        else if (s.Equals("spawnally3"))
        {
            mana.CurrentVal -= 35;
        }

    }
    //-----------------------------------------------------------------------------------

    public void spawnAlly(string s)
    {
        Debug.Log(" spawning ==>> " + s + " ally");
        AllyContainer ac;

        if (s.Equals("spawnally1") && spawnCount1 < 5)
        { // 

            Debug.Log(" inside respective spanally ");
            GameObject spawnedObj1 = Instantiate(this.objToSpawn1, GameObject.Find("FPSController").transform.position + new Vector3(0.0f,0.0f,2.0f), this.transform.rotation);

            ac = new AllyContainer(spawnedObj1, 100, 100, s);

            spawnCount1++;
            spawnedObj1.SendMessage("setContainer", ac);
            sceneManager.addAlly(ac);
        }

        else if (s.Equals("spawnally2") && spawnCount2 < 5)
        {
            GameObject spawnedObj2 = Instantiate(this.objToSpawn2, GameObject.Find("FPSController").transform.position + new Vector3(0.0f, 0.0f, 2.0f), this.transform.rotation);

            ac = new AllyContainer(spawnedObj2, 100, 100, s);

            spawnCount2++;
            spawnedObj2.SendMessage("setContainer", ac);
            sceneManager.addAlly(ac);
        }

        else if (s.Equals("spawnally3") && spawnCount3 < 5)
        {
            GameObject spawnedObj3 = Instantiate(this.objToSpawn3, GameObject.Find("FPSController").transform.position + new Vector3(0.0f, 0.0f, 2.0f), this.transform.rotation);
            ac = new AllyContainer(spawnedObj3, 100, 100, s);
            spawnCount3++;
            spawnedObj3.SendMessage("setContainer", ac);
            sceneManager.addAlly(ac);
        }
    }
}
//-----------------------------------------------------------------------------------
