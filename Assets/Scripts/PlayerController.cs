using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private bool inPlay;
	// Use this for initialization
	void Start () {
        inPlay = true;
	}
	
	// Update is called once per frame
	void Update () {


	}



    void OnCollisionEnter(Collision col) {

        // So far in the prototype, the only objects the thing can collide with and should die from are bollards
        // And by the messy construction of the prototype course, all bollards are children of a Course GObject
        // Hit a child of Course, you lose.
        // TODO: update this to work with the generated levels once infinite level generation is a thing
        if(col.collider.GetComponent<Transform>().parent.name == "Course") {
            inPlay = false;
            Debug.Log("Hit a wall! Press R to restart");
            Time.timeScale = 0;
        }
    }

    // Getter function so that DriftController can see how the game's doing
    public bool getInPlay() {
        return inPlay;
    }
}
