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
            Debug.Log("Hit a wall! Press R to restart");
            Time.timeScale = 0;
            // Show Game Over
            // Show "Press R to Restart" prompt
            // Create a GameController script that'll store gamestate and perform resets if needed
            // Then you can work on making the random 
        }
    }
}
