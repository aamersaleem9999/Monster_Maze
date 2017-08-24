using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dungeonGenerator : MonoBehaviour {

    CustomPlayer cp;
    //public GameObject player;
    // Use this for initialization
    public GameObject trigger;
    public GameObject wave1;
    public GameObject wave2;
    public GameObject wave3;
    public GameObject wave4;
    public GameObject wave5;
    public GameObject otherPiramid;

    void Start () {
        cp = this.GetComponentInParent<CustomPlayer>();

    }

    void OnTriggerEnter(Collider other)
    {
       // Debug.Log(" dungeon generated");
        if (other.gameObject.tag == "esp_t_1")
        {
            trigger.GetComponent<AudioSource>().enabled = true;
            wave1.SetActive(true);
            wave2.SetActive(true);
        }
       else if (other.gameObject.tag == "esp_t_2")
        {

            wave3.SetActive(true);
            wave4.SetActive(true);
            wave5.SetActive(true);
        }
        else if (other.gameObject.tag == "teleport")
        {
            Debug.Log(" inside teleportation" +
                " ");
            // yield return new WaitForSeconds(4);
            this.transform.position = otherPiramid.transform.position;
           // yield return new WaitForSeconds(2);
        }

    }
    // Update is called once per frame
    void Update () {
		
	}
}
