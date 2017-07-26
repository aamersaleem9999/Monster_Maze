using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AllyContainer
{
    private int currHP;
    private int maxHP;
    private int currMP;
    private int maxMP;
    private GameObject go;
    private NavMeshAgent agent;

    public AllyContainer(GameObject go, int hp, int mp)
    {
        this.go = go;
        this.agent = this.go.GetComponent<NavMeshAgent>();
        this.currHP = hp;
        this.maxHP = hp;
        this.currMP = mp;
        this.maxMP = mp;
    }

    public bool isDead()
    {
        return this.currHP <= 0;
    }

																//Taking damange from enemy
    public  void takeDamage(int hp)
    {
        //Debug.Log(" taking damage in ally controller with current HP as ==>: " +this.currHP );

		currHP -= hp;

		Debug.Log (" Curr hp of Ally "+currHP);
    }

    public float getPercentHealthLeft()
    {
        return (float)this.currHP / (float)this.maxHP;
    }

    public GameObject getGO()
    {
        return this.go;
    }

    public int getCurrHP()
    {
        return this.currHP;
    }

    public int getMaxHP()
    {
        return this.maxHP;
    }

    public int getCurrMP()
    {
        return this.currMP;
    }

    public int getMaxMP()
    {
        return this.maxMP;
    }
    
}
