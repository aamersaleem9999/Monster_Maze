using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class player : MonoBehaviour {
	public GameObject objToSpawn;
	public Hand leftHand;
	public SteamVR_Camera camera;
	private Rigidbody rb;
	private int spawnCount = 0;

	void Start ()
	{
		this.rb = this.GetComponent<Rigidbody>();
		sceneManager.player = this.gameObject;
		InvokeRepeating("spawnAlly", 3, 3);
	}

	private void HandleTriggerClicked()
	{
		transform.position += this.leftHand.transform.forward * Time.deltaTime * 1.0f;
	}

	void spawnAlly()
	{
		spawnCount++;
		GameObject spawnedObj = Instantiate(this.objToSpawn, this.transform.localPosition, this.transform.rotation);

		AllyContainer ac = new AllyContainer(spawnedObj, 100, 100);
		spawnedObj.SendMessage("setContainer", ac);
		sceneManager.addAlly(ac);//Creating new ally

		if(spawnCount == 1)
		{
			this.CancelInvoke();//Limits number of spawns overwriting above start statement for **TESTING
		}
	}
}
