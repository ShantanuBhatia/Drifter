using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriftController : MonoBehaviour {
    // Score racked up during a particular drift.
    private int currentDriftScore;

    // Total score racked up over a game, start to finish
    private int totalRunDriftScore;

    // Self explanatory
    private bool isDriftingThisFrame;
    private bool wasDriftingLastFrame;

    // Car in play vs Car has hit a wall and is now out
    private bool CarInPlay;

    // Reference to the Player Controller to get CarInPlay
    private PlayerController PC;


	void Start () {
        isDriftingThisFrame = false;
        wasDriftingLastFrame = false;
        PC = GetComponent<PlayerController>();
	}
	

	void Update () {


        CarInPlay = PC.getInPlay();

        // DEBUG CODE - this helps zero out and avoid weird errors where it thinks Car is drifting even if it isn't
        // TODO: Fix the issues that cause Drift to evaluate true even when false
        if (Input.GetKey(KeyCode.G)) {
            totalRunDriftScore = 0;
            Debug.Log("Score reset. Succesfully initialized to " + totalRunDriftScore);
        }

        // The sta of a drift is a frame where you're drifting now and weren't drifting one frame ago
        // The mid of a drift is a frame where you're drifting now and were drifting one frame ago
        // The end of a drift is a frame where you're not drifting now and were drifting one frame ago

        // These two lines store the values for this frame and the last, and then the nested ifs check which drift-state you're in
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

        // Check the dot product of the velocity and forward position vectors 
        // This will tell you what the direction of movement currently is
        // If the car is in a certain range of sideways motion, it counts as drifting

        float dotProd = Vector3.Dot(transform.forward.normalized, GetComponent<Rigidbody>().velocity.normalized);

        // 0.8f is going slightly sideways, 0.0f is going totally perpendicular
        // Magnitude > 5f because slow drifts shouldn't get you points
        if (0.8f > dotProd && dotProd > 0.0f && GetComponent<Rigidbody>().velocity.magnitude > 5f) {
            Debug.Log("DRIFTING!");
            return true;
        }

        // Default return
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
