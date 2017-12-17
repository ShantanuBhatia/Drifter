using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AxleInfo {
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}

public class CarController : MonoBehaviour {
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public GameObject car_root;

    public void Start() {
        foreach(AxleInfo axleInfo in axleInfos) {
            InitLocalPositionToVisuals(axleInfo.leftWheel);
            InitLocalPositionToVisuals(axleInfo.rightWheel);
        }
        // Awful, awful hack to get the car_root object as a reference I can use to fix steering angle
        //car_root = transform.parent.gameObject;
        //car_root = car_root.transform.parent.gameObject;
    }

    // Initialize wheels to correct orientation
     public void InitLocalPositionToVisuals(WheelCollider collider) {
        if (collider.transform.childCount == 0) {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = Quaternion.Euler(90, 0, 90);
    }


    // finds the corresponding visual wheel
    // correctly applies the transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider) {
        if (collider.transform.childCount == 0) {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        //visualWheel.transform.position = position;
        //visualWheel.transform.rotation = Quaternion.Euler(90.0f + transform.rotation.x, 0.0f, 90.0f + transform.rotation.z);

        //visualWheel.transform.rotation = Quaternion.Euler(90.0f + transform.rotation.x, car_root.transform.rotation.y + collider.steerAngle, 90.0f + car_root.transform.rotation.z + collider.steerAngle);

        //visualWheel.transform.localEulerAngles = new Vector3(0.0f, car_root.transform.rotation.y + collider.steerAngle, 90.0f + car_root.transform.rotation.z + collider.steerAngle);
        visualWheel.transform.localEulerAngles = new Vector3(0.0f, car_root.transform.rotation.y + collider.steerAngle, 90.0f);

    }

    public void FixedUpdate() {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        foreach (AxleInfo axleInfo in axleInfos) {
            if (axleInfo.steering) {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
                ApplyLocalPositionToVisuals(axleInfo.leftWheel);
                ApplyLocalPositionToVisuals(axleInfo.rightWheel);
            }
            if (axleInfo.motor) {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
        }
    }
}