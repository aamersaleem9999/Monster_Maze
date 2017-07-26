using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHUDManager : MonoBehaviour
{
	public GameObject healthBar;
	public GameObject healthBarText;

	void Start ()
	{
		this.healthBarText.GetComponent<TextMesh>().text = "";
	}
	//Player Health Bar
	void Update ()
	{
		this.healthBarText.GetComponent<TextMesh>().text = playerStats.currHP + " / " + playerStats.maxHP;

		if (playerStats.isDead())			//Load GameOverScene if player is dead
		{
			Debug.Log("Player is Dead");
			Application.LoadLevel("GameOverScene");            //SAVED BY AAMER ON 7/16/2017
			Application.Quit();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		//handle player getting attacked
		if(other.tag == "enemy weapon")
		{
			playerStats.currHP -= 0;
		}
	}
}
