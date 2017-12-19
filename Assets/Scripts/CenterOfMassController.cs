using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterOfMassController : MonoBehaviour {

    Transform parentTransform;
    Rigidbody parentRB;
	// Use this for initialization
	void Start () {
        parentTransform = transform.parent;
        parentRB = parentTransform.GetComponent<Rigidbody>();
        parentRB.centerOfMass = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(transform.localPosition);
	}
}
