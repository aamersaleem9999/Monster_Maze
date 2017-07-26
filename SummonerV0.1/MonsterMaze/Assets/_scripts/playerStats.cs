using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStats : MonoBehaviour
{
	public static int currHP = 10;
	public static int maxHP = 10;
	public int startHP = 10;		
	public int startMaxHP = 10;
	public static GameObject player;

	//Initializing player HP
	void Start ()
	{
		playerStats.currHP = 9999;
		playerStats.maxHP = 9999;
		player = GameObject.FindGameObjectWithTag("player").gameObject;
	}

	public int getCurrHP()
	{ 
		return currHP;		//Returns current HP of the player
	}

	public static void takeDamage(int amount)
	{
		currHP = currHP-amount;  //Damange amount is taken from enemy.cs
	}

	public static bool isDead()
	{
		return currHP <= 0;  //Returns True if player is dead/ HP=0
	}

	public static void despawn()
	{
		//Debug.Log ("In player stats despawn method");
	}
		
}
