using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriftController : MonoBehaviour {

    private int currentDriftScore;
    private int totalRunDriftScore;
    private bool isDriftingThisFrame;
    private bool wasDriftingLastFrame;
    private bool CarInPlay;
    private PlayerController PC;

	// Use this for initialization
	void Start () {
        isDriftingThisFrame = false;
        wasDriftingLastFrame = false;
        PC = GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {

        //Debug.Log(GetComponent<Rigidbody>().velocity.magnitude);
        CarInPlay = PC.getInPlay();
        if (Input.GetKey(KeyCode.G)) {
            totalRunDriftScore = 0;
            Debug.Log("Score reset. Succesfully initialized to " + totalRunDriftScore);
        }


        wasDriftingLastFrame = isDriftingThisFrame;
        isDriftingThisFrame = driftCheck();

        if (CarInPlay) { 
            if (!wasDriftingLastFrame && isDriftingThisFrame ) {
                StartDrift();
            }
            if (wasDriftingLastFrame && isDriftingThisFrame) {
                addToDrift();
            }
            if(wasDriftingLastFrame && !isDriftingThisFrame) {
                StopDrift();
                Debug.Log("New Total Score: " + totalRunDriftScore);
            }

            if (driftCheck()) {
                Debug.Log("Current Drift Score: " + currentDriftScore);
            }
        }

	}


    bool driftCheck() {
        // Check the dot product of the velocity and forward position vectors. 
        // This will tell you what the direction of movement currently is.
        // If the car is in a certain range of sideways motion, it counts as drifting.
        float dotProd = Vector3.Dot(transform.forward.normalized, GetComponent<Rigidbody>().velocity.normalized);
        //Debug.Log("Dot Product: " + dotProd);
        if (0.8f > dotProd && dotProd > 0.0f && GetComponent<Rigidbody>().velocity.magnitude > 5f) {
            Debug.Log("DRIFTING!");
            return true;
        }
        //if (0.8 > dotProd) {
        //    Debug.Log(dotProd);
        //}
        return false;
    }

    void StartDrift() {
        currentDriftScore = 1;
    }

    void addToDrift() {
        currentDriftScore++;
    }

    void StopDrift() {
        totalRunDriftScore += currentDriftScore;
        currentDriftScore = 0;
    }
}
