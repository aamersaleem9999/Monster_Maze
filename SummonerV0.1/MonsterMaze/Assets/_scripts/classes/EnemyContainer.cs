using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyContainer
{
    private int currHP;
    private int maxHP;
    private int currMP;
    private int maxMP;
    private GameObject go;
    private NavMeshAgent agent;

    public EnemyContainer(GameObject go, int hp, int mp)
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

    public void takeDamage(int hp)
    {
        this.currHP -= hp;
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
