using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterOfMassController : MonoBehaviour {

    Transform parentTransform;
    Rigidbody parentRB;


    // All this gotta do is set the Center of Mass to wherever the empty CoM GObject has been manually placed
    // I've currently placed it sort of near the front and bottom of the car
    // It's a guess tbh
	void Start () {
        parentTransform = transform.parent;
        parentRB = parentTransform.GetComponent<Rigidbody>();
        parentRB.centerOfMass = transform.localPosition;
	}
	

}
