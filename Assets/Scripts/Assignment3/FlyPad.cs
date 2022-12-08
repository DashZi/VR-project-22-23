using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlyPad : MonoBehaviour
{
    public enum InputMapping
    {
        PositionControl,
        VelocityControl,
        AccelerationControl
    }

    [Header("Transforms")]
    public Transform padTransform;
    public Transform headTransform;

    public InputActionProperty centerPadUnderHeadAction;
    public InputActionProperty accelerationBrakeAction;

    [Header("General Settings")]
    public InputMapping inputMapping;
    [Range(0.0f, 0.9f)]
    [Tooltip("Describes a Deadzone Radius (around the flypad center) within which no input is applied to transform updates. Input is normalized based on this threshold.")]
    public float inputMagnitudeThreshold = 0.2f;

    [Header("Mapping Specific Variables")]
    public float positionControlScaleFactor = 30;
    public float maximumVelocity = 50;
    public float maximumAcceleration = 0.05f;
    public float accelerationBrakeScaleFactor = 0.1f;

    private Vector3 currentVelocity = Vector3.zero;
    private Vector3 padOrigin; // *Hint1 
    private Vector3 offsetToCenter;  // *Hint2 
    public float speed = 0;

    //Vector2 padOriginXZ (Vector3 padOriginxz) {
    //    return new Vector2 (padOriginxz.x, padOriginxz.z);
    //}
    //Vector2 v2 = padOrigin.padOriginxz;

    /*
     * *Hint1: set padOrigin whenever CenterPadUnderHead() is called 
     *    -> Use it for your PositionControl(...) implementation
     * *Hint2: set offsetToCenter whenever CenterPadUnderHead() is called 
     *    -> offsetToCenter should describe the [x,z]-offset between headTransform.position and transform.position.
     *       The offset needs to be applied as a final step in your PositionControl(...) implementation.
     *       Otherwise, your actual jump position will not align with the preview avatar position.
     */

    private void Start()
    {
        CenterPadUnderHead();
    }

    private void Update()
    {
        if (centerPadUnderHeadAction.action.WasPressedThisFrame())
            CenterPadUnderHead();
        else
            EvaluateInput();
    }

    public void CenterPadUnderHead()
    {
        // Task 3.1 TODO
        padTransform.position = new Vector3(headTransform.transform.position.x, 0, headTransform.transform.position.z); //w
        padOrigin = padTransform.transform.position; //w
    }

    private void EvaluateInput()
    {
        Vector2 userPadPosition = CalculateUserPadPosition();

        switch (inputMapping)
        {
            case InputMapping.PositionControl:
                PositionControl(userPadPosition);
                break;
            case InputMapping.VelocityControl:
                VelocityControl(userPadPosition);
                break;
            case InputMapping.AccelerationControl:
                AccelerationControl(userPadPosition);
                break;
        }
    }

    private void PositionControl(Vector2 userPadPosition)
    {
        // Task 3.1 TODO 

        Vector3 v3 = new Vector3(userPadPosition.x, 0, userPadPosition.y); //w
        //userPadPosition = v3; //w
        transform.position = padOrigin + (v3 * positionControlScaleFactor); //w
    }

    private void VelocityControl(Vector2 userPadPosition)
    {
        // Task 3.1 TODO

            Debug.Log("THIS IS OVER THE MAGNITUDE THRESHOLD");
           

            float sConstrain = maximumVelocity * CalculateScaledInputMagnitude(userPadPosition);

            float distanceTraveled = userPadPosition.magnitude * Time.deltaTime;

            Vector3 fVelocity = new Vector3(userPadPosition.normalized.x * distanceTraveled * sConstrain, 0, userPadPosition.normalized.y * distanceTraveled * sConstrain);

            transform.Translate(fVelocity);

            Debug.Log("the percentage of appleid Speed Factor is" + sConstrain);
  
    }

    private void AccelerationControl(Vector2 userPadPosition)
    {
        // Task 3.1 (3.2 optional) TODO

        float aConstrain = maximumAcceleration * CalculateScaledInputMagnitude(userPadPosition);

        Vector2 padDirection = new Vector2();

        padDirection.x = currentVelocity.x;
        padDirection.y = currentVelocity.y;

        currentVelocity.x = currentVelocity.x + (userPadPosition.x * aConstrain);
        currentVelocity.y = currentVelocity.y + (userPadPosition.y * aConstrain);
        
        Debug.Log(currentVelocity.x + currentVelocity.y);


        Vector3 fAceleration = new Vector3(transform.position.x + currentVelocity.x * Time.deltaTime, transform.position.y, transform.position.z + currentVelocity.y * Time.deltaTime);

        transform.position = fAceleration;

        //Debug.Log("the percentage of appleid Acceleration Factor is" + maximumAcceleration);

        // reverse acceleration is a breaking mechanism
    }

    // Returns the users position relative to the pads origin on the xz-Plane ([x,z] are in Range [-1,1])
    // Does not account for inputMagnitudeThreshold (see instead CalculateScaledInputMagnitude())
    private Vector2 CalculateUserPadPosition()
    {
        float userXPos = Mathf.Clamp((headTransform.position.x - padTransform.position.x) / (padTransform.lossyScale.x / 2), -1f, 1f);
        float userZPos = Mathf.Clamp((headTransform.position.z - padTransform.position.z) / (padTransform.lossyScale.x / 2), -1f, 1f);

        return new Vector2(userXPos, userZPos);
    }

    // Returns the users input magnitude scaled between [0, 1] depending on inputMagnitudeThreshold
    private float CalculateScaledInputMagnitude(Vector2 userPadPosition)
    {
        var clampedMagnitude = Mathf.Clamp(userPadPosition.magnitude, 0f, 1f);
        return clampedMagnitude < inputMagnitudeThreshold ? 0 : (clampedMagnitude - inputMagnitudeThreshold) / (1 - inputMagnitudeThreshold);
    }

}
