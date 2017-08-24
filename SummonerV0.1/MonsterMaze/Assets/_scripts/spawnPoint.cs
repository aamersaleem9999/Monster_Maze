using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPoint : MonoBehaviour
{
    public GameObject objToSpawn;
    private int count = 0;
    public int enemyNumber;

	// Use this for initialization
	void Start ()
    {
        InvokeRepeating("spawnEnemy", 3, 5);
	}
	
    public void spawnEnemy()
    {
        count++;
        GameObject go = Instantiate(this.objToSpawn, this.transform.position, Quaternion.identity);
        EnemyContainer ec = new EnemyContainer(go, 100, 100);
        go.SendMessage("setContainer", ec);
        sceneManager.addEnemy(ec);
        if(count == enemyNumber)
        {
            this.CancelInvoke(); 
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
