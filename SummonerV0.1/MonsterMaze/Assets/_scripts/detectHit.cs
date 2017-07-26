using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class detectHit : MonoBehaviour {

	//public Slider healthBar;
	public string Opponent;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag != Opponent)
			return;
		
		Debug.Log ("Hit!");
		
	}
}
