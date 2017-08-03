using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthbar : MonoBehaviour
{ 
	// Use this for initialization
	void Start () {
        Vector3 hbScale = this.transform.localScale;
		this.transform.localScale = new Vector3(hbScale.x, hbScale.y * 0.5f, hbScale.z);
    }
	
	// Update is called once per frame
	void Update ()
    {   

	}
}
