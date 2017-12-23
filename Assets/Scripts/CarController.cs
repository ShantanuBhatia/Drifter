using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AxleInfo {
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
    public float antiRoll;
}

public class CarController : MonoBehaviour {
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxBrakeTorque;
    public float maxSteeringAngle;
    public GameObject car_root;

    public void Start() {
        foreach (AxleInfo axleInfo in axleInfos) {
            InitLocalPositionToVisuals(axleInfo.leftWheel);
            InitLocalPositionToVisuals(axleInfo.rightWheel);
        }
    }

    public void AntiRollBar() {
        foreach(AxleInfo axleInfo in axleInfos) {
            WheelHit hit;
            float leftTravel = 1.0f;
            float rightTravel = 1.0f;

            bool leftWheelGrounded = axleInfo.leftWheel.GetGroundHit(out hit);
            if (leftWheelGrounded) {
                leftTravel = (-axleInfo.leftWheel.transform.InverseTransformPoint(hit.point).y - axleInfo.leftWheel.radius) / axleInfo.leftWheel.suspensionDistance;
            }

            bool rightWheelGrounded = axleInfo.rightWheel.GetGroundHit(out hit);
            if (rightWheelGrounded) {
                rightTravel = (-axleInfo.rightWheel.transform.InverseTransformPoint(hit.point).y - axleInfo.rightWheel.radius) / axleInfo.rightWheel.suspensionDistance;
            }

            float antiRollForce = (leftTravel - rightTravel) * axleInfo.antiRoll;

            if (leftWheelGrounded) {
                GetComponent<Rigidbody>().AddForceAtPosition(axleInfo.leftWheel.transform.up * -antiRollForce, axleInfo.leftWheel.transform.position);
            }

            if (rightWheelGrounded) {
                GetComponent<Rigidbody>().AddForceAtPosition(axleInfo.rightWheel.transform.up * -antiRollForce, axleInfo.rightWheel.transform.position);
            }

        }
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

        visualWheel.transform.localEulerAngles = new Vector3(0.0f, car_root.transform.rotation.y + collider.steerAngle, 90.0f);

    }

    public void FixedUpdate() {
        //Debug.Log(transform.GetComponent<Rigidbody>().centerOfMass);
        //Debug.Log(Input.GetAxis("Vertical"));
        float motor = Input.GetAxis("Vertical") > 0 ? maxMotorTorque * Input.GetAxis("Vertical") : 0.0f;
        float brake = Input.GetAxis("Vertical") < 0 ? maxBrakeTorque * Input.GetAxis("Vertical") : 0.0f;
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        foreach (AxleInfo axleInfo in axleInfos) {
            if (axleInfo.steering) {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
                ApplyLocalPositionToVisuals(axleInfo.leftWheel);
                ApplyLocalPositionToVisuals(axleInfo.rightWheel);
            }
            if (axleInfo.motor) {
                axleInfo.leftWheel.brakeTorque = brake;
                axleInfo.rightWheel.brakeTorque = brake;
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
        }
        AntiRollBar();
    }
}