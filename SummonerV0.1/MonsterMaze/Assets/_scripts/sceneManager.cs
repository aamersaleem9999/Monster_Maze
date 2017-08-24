using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class sceneManager : MonoBehaviour
{
    public static List<EnemyContainer> enemies = new List<EnemyContainer>();
    public static List<AllyContainer> allies = new List<AllyContainer>();
    public static int walkHash = Animator.StringToHash("isWalking");
    public static int attackHash = Animator.StringToHash("isAttacking");
    public static int idleHash = Animator.StringToHash("isIdle");
    public static int deathHash = Animator.StringToHash("isDead");
    public static GameObject player = null;

    ///////////////////////////////////////////////////////////////           ANIMATION SECTION         ///////////////////////////////////////////////////////
    public static void Death(GameObject go, Animator anim)
    {
        anim.SetTrigger(deathHash);
    }

    //Removes the character from the map if dead
    public static void deSpawn(GameObject go)
    {
        Destroy(go);
    }

    //Character movement animation
    public static void setMovementAnimation(Animator anim, GameObject src, AllyContainer destinationTarget)

    {
        if (Vector3.Distance(src.transform.position, destinationTarget.getGO().transform.position) > 4.2)
        {
            anim.SetTrigger(walkHash);//Walk animation to attack ally character

        }
        else
        {
            anim.SetTrigger(attackHash);//Attack animation to attack ally character
        }
    }

    public static void setMovementAnimationally(Animator anim, GameObject src, EnemyContainer destinationTarget)

    {
        if (Vector3.Distance(src.transform.position, destinationTarget.getGO().transform.position) > 4.2)
        {
            anim.SetTrigger(walkHash);//Walk animation to attack ally character

        }
        else
        {
            anim.SetTrigger(attackHash);//Attack animation to attack ally character
        }
    }

    public static void setMovementToPlayer(Animator anim, GameObject src, GameObject playerObject)

    {
        if (Vector3.Distance(src.transform.position, playerObject.transform.position) > 3.2)
        {
            anim.SetTrigger(walkHash);//Walk animation to attack player
        }
        else
        {
            anim.SetTrigger(attackHash);//Attack animation to attack player
        }
    }
    ///////////////////////////////////////////////////////////           ANIMATION SECTION **ENDS      ////////////////////////////////////////////////////////// 




    public static void addAlly(AllyContainer ally)
    {
        allies.Add(ally);//Spawns ally
    }
    ////////////////////////              SPAWNING CHARACTERS         ////////////////////////////////////


    public static void addEnemy(EnemyContainer enemy)
    {
        enemies.Add(enemy);//Spawns Enemy
    }



    ///////////////////////////////////////////////////////////           DIRECTING CHARACTERS      ////////////////////////////////////////////////////////////// 


    /// Finds the closest ally.
    public static AllyContainer findClosestAlly(GameObject enemy)
    {
        AllyContainer resultAlly = null;

        foreach (AllyContainer allyObj in allies)
        {
            if (allyObj != null && !allyObj.isDead())                   //Check for live ally 
            {
                resultAlly = allyObj;
                break;
            }
        }
        return resultAlly;
    }
    public static AllyContainer findClosestAllyTesting(GameObject go)
    {

        AllyContainer resultAlly = null;
       
        float closestDistance = -1f;
        int winnerPos = -1;
        int currPos = -1;
        foreach (AllyContainer obj in allies)
        {
            
            if (obj == null || obj.isDead()) continue;
            currPos++;
            if (resultAlly == null)
            {
                winnerPos = currPos;
                resultAlly = obj;
                closestDistance = Vector3.Distance(go.transform.position, obj.getGO().transform.position);
            }
            else
            {
                float currDistance = Vector3.Distance(go.transform.position, obj.getGO().transform.position);
                if (currDistance < closestDistance)
                {
                    winnerPos = currPos;
                    resultAlly = obj;
                    closestDistance = currDistance;
                }
            }
        }
       
        return resultAlly;
    }

    public static void gokillPlayer(Animator anim, NavMeshAgent agent, GameObject enemyObject, AllyContainer closest)
    {
        player = GameObject.FindGameObjectWithTag("player").gameObject;

        if (Vector3.Distance(agent.transform.position, player.transform.position) > 3.2)
        {
            anim.SetTrigger(walkHash);
            agent.SetDestination(player.transform.position + new Vector3(0.0f, 0.0f, 3.0f));
        }
        else
            anim.SetTrigger(attackHash);
        if(Vector3.Distance(agent.transform.position, player.transform.position) > 10)
        {
            agent.SetDestination(closest.getGO().transform.position);
            sceneManager.setMovementAnimation(anim, enemyObject, closest);
        }
    }

    //Ally Comes back to player
    public static void gotoPlayer(Animator anim, NavMeshAgent agent, GameObject allyObject)
    {
        player = GameObject.FindGameObjectWithTag("player").gameObject;

        if (Vector3.Distance(agent.transform.position, player.transform.position) > 5.2f)
        {
            anim.SetTrigger(walkHash);
            agent.SetDestination(player.transform.position + new Vector3(0.0f, 0.0f, Random.Range(3f, 5f)));
        }
        else
        {
            agent.isStopped = true;
            anim.SetTrigger(idleHash);
        }
    }

    //Enemy searches for nearest target; Either ally or player himself
    public static EnemyContainer findClosestEnemy(GameObject go)
    {
        EnemyContainer closest = null;
        float closestDistance = -1f;
        int winnerPos = -1;
        int currPos = -1;
        foreach (EnemyContainer obj in enemies)
        {
            //Lets the enemy ignore dead allies of the player
            if (obj == null || obj.isDead()) continue;
            currPos++;
            if (closest == null)
            {
                winnerPos = currPos;
                closest = obj;
                closestDistance = Vector3.Distance(go.transform.position, obj.getGO().transform.position);
            }
            else
            {
                float currDistance = Vector3.Distance(go.transform.position, obj.getGO().transform.position);
                if (currDistance < closestDistance)
                {
                    winnerPos = currPos;
                    closest = obj;
                    closestDistance = currDistance;
                }
            }
        }
        return closest;
    }

    ///////////////////////////////////////////////////////////           DIRECTING CHARACTERS **ENDS     //////////////////////////////////////////////////////////////

/*
    public static void sceneChange()
    //Load GameOverScene if player is dead
    {
        yield return new WaitForSecondsRealtime(2);
        Debug.Log("Player is Dead");
        Application.LoadLevel("GameOverScene");             
        Application.Quit();

    }
    */
}