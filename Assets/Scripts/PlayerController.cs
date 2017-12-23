using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		

	}

    void OnCollisionEnter(Collision col) {
        //Debug.Log(col.collider.GetComponent<Transform>().parent.name);
        if(col.collider.GetComponent<Transform>().parent.name == "Course") {
            Debug.Log("Hit a wall!");
        }
    }
}
