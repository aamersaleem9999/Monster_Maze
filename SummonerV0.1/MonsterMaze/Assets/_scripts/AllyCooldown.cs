using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllyCooldown : MonoBehaviour {

	public List<Ally> Ally;

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("click");
            if (Ally[0].currentCooldown >= Ally[0].coolDown)
            {

                Ally[0].currentCooldown = 0;

            }
        }
        ///Repeat this code for different characters

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            if (Ally[1].currentCooldown >= Ally[1].coolDown)
            {

                Ally[1].currentCooldown = 0;

            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (Ally[2].currentCooldown >= Ally[2].coolDown)
            {

                Ally[2].currentCooldown = 0;

            }
        }
    }
	void Update()
	{
		foreach (Ally n in Ally) {
		
			if (n.currentCooldown < n.coolDown) {
				n.currentCooldown += Time.deltaTime;
				n.allyIcon.fillAmount = n.currentCooldown / n.coolDown; //Fill amount
			}
		}
	}
}

[System.Serializable]
public class Ally
{
	public float coolDown;
	public Image allyIcon;
	[HideInInspector]
	public float currentCooldown;
}
	