using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllySelector: MonoBehaviour {

	CustomPlayer cp;
	public GameObject player;

	void Start () {
		
		player = GameObject.FindGameObjectWithTag("player").gameObject;
		cp=GetComponentInParent<CustomPlayer>();
	}

   void  OnTriggerEnter(Collider other)
	{
		
		if (other.gameObject.tag == "spawnally1" || other.gameObject.tag == "spawnally2" || other.gameObject.tag == "spawnally3") 
		{
	            
			Debug.Log (" trigger generated into button with name"+ other.gameObject.tag);
			//cp.moving = false;
			cp.AllySpawner (other.gameObject.tag);
		}
	}
}
