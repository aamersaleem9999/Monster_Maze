using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHUDManager : MonoBehaviour
{
    public GameObject healthBar;
    public GameObject healthBarText;

    private float maxWidth;
    
	// Use this for initialization
	void Start ()
    {
        //Vector3 objectSize = Vector3.Scale(this.healthBar.transform.localScale, this.healthBar.GetComponent<Mesh>().bounds.size);
        this.maxWidth = this.healthBar.transform.lossyScale.y;
        this.healthBarText.GetComponent<TextMesh>().text = "";
	}
	

 
    private void OnTriggerEnter(Collider other)
    {
        //handle player getting attacked
        if(other.tag == "enemy weapon")
        {
          
        }
    }
}
