using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPoint : MonoBehaviour
{
	public GameObject objToSpawn;
	private int count = 0;

	void Start ()
	{
		InvokeRepeating("spawnEnemy", 3, 5);
	}

	void spawnEnemy()
	{
		count++;
		GameObject go = Instantiate(this.objToSpawn, this.transform.position, Quaternion.identity);

		EnemyContainer ec = new EnemyContainer(go, 100, 100);//Creating new enemy
		go.SendMessage("setContainer", ec);
		sceneManager.addEnemy(ec);

		if(count == 1)//Limits number of spawns overwriting above start statement for **TESTING
		{
			this.CancelInvoke();
		}
	}
}
