using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GoGo : MonoBehaviour
{
    #region Member Variables

    [Header("Transforms")] 
    public Transform head;
    public Transform rightController;
    public Transform rightHandMesh;
    public Transform leftController;
    public Transform leftHandMesh;

    [Header("Input Actions")]
    public InputActionProperty rightHandGrab;
    public InputActionProperty leftHandGrab;
    
    [Header("Collider")]
    public HandCollider rightHandCollider;
    public HandCollider leftHandCollider;

    [Header("Parameters")] 
    public float maxDistance = 9f;
    public float activationOffset = 0.35f;
    public float maxReachDistance = 1f;

    private GameObject rightGrabbedObject;
    private GameObject leftGrabbedObject;

    float coefficient = 9;
    public Transform tmpParent;
    #endregion

    #region MonoBeaviour Callbacks

    private void Update()
    {

        //----------------------------------------------------------



        //----------------------------------------------------------

        UpdateHands();
        UpdateGrab();
    }

    #endregion

    #region Custom Methods
    public void UpdateHands()
    {

        // TODO Excercise 4.3 
        // hand savesent calculation

        Vector3 rightDifference = rightController.position - (head.position - new Vector3(0.0f, 0.2f));
        float rightHandMotorSpaceDistance = rightDifference.magnitude;
        Vector3 rightHandDirection = rightDifference.normalized;

        if (rightHandMotorSpaceDistance < activationOffset)
        {
            rightHandMesh.position = rightController.position;
        }
        else
        {
            float virtualDistanceFromRightController = coefficient * (float)Math.Pow((rightHandMotorSpaceDistance - activationOffset), 2);
            virtualDistanceFromRightController *= 100.0f; //conversion to centieetres
            rightHandMesh.position = rightController.position + rightHandDirection * virtualDistanceFromRightController;
        }

        Vector3 leftDifference = leftController.position - (head.position - new Vector3(0.0f, 0.2f));
        float leftHandMotorSpaceDistance = leftDifference.magnitude;
        Vector3 leftHandDirection = leftDifference.normalized;

        if (leftHandMotorSpaceDistance < activationOffset)
        {
            leftHandMesh.position = leftController.position;
        }
        else
        {
            float virtualDistanceFromLeftController = coefficient * (float)Math.Pow((leftHandMotorSpaceDistance - activationOffset), 2);
            virtualDistanceFromLeftController *= 100.0f; //conversion to centimetres
            leftHandMesh.position = leftController.position + leftHandDirection * virtualDistanceFromLeftController;
        }
    }

    //public void ReparentingGrab()
    public void UpdateGrab()
    {
        // TODO: Excercise 4.2
        //Right    
        if (rightHandGrab.action.IsPressed())
        {
            if (rightGrabbedObject == null && rightHandCollider.isColliding && rightHandCollider.collidingObject != leftGrabbedObject)//* check if coliding is true
            {
               //rightHandCollider.transform.SetParent(rightGrabbedObject.transform);// change or transform its parent to LH
               tmpParent = rightHandCollider.collidingObject.transform.parent.transform;
               rightGrabbedObject = rightHandCollider.collidingObject;
               //leftGrabbedObject.transform.SetParent(leftHandCollider.gameObject.transform, true);
            //    Matrix4x4 NewMatrixO_1 = GetTransformationMatrix(rightHandCollider.gameObject.transform, true);
              // ActivateHaptic();
            //    rightGrabbedObject.transform.position = NewMatrixO_1.GetColumn(3);

            }
            else if (rightGrabbedObject != null)
            {
               rightGrabbedObject.transform.SetParent(rightHandCollider.transform.parent.transform.parent.transform);
            }
        }
        else if (rightHandGrab.action.WasReleasedThisFrame())// or when i leave the obj the new parent is null
        {
            rightGrabbedObject.transform.SetParent(tmpParent);
            rightGrabbedObject = null;//parent becomes null and stays there
            tmpParent = null;
        }

    //Left    
        if (leftHandGrab.action.IsPressed())
        {
            if (leftGrabbedObject == null && leftHandCollider.isColliding && leftHandCollider.collidingObject != rightGrabbedObject)//* check if coliding is true
            {
               //rightHandCollider.transform.SetParent(rightGrabbedObject.transform);// change or transform its parent to LH
               tmpParent = leftHandCollider.collidingObject.transform.parent.transform;
               leftGrabbedObject = leftHandCollider.collidingObject;
               //leftGrabbedObject.transform.SetParent(leftHandCollider.gameObject.transform, true);
               //Matrix4x4 NewMatrixO_1 = GetTransformationMatrix(leftHandCollider.gameObject.transform, true);
               //ActivateHaptic();
               //leftGrabbedObject.transform.position = NewMatrixO_1.GetColumn(3);

            }
            else if (leftGrabbedObject != null)
            {
               leftGrabbedObject.transform.SetParent(leftHandCollider.transform.parent.transform.parent.transform);
            }
        }
        else if (leftHandGrab.action.WasReleasedThisFrame())// or when i leave the obj the new parent is null
        {
            leftGrabbedObject.transform.SetParent(tmpParent);
            leftGrabbedObject = null;//parent becomes null and stays there
            tmpParent = null;
        }
    }

    /*public void UpdateGrab()
    {
        // TODO Excercise 4.3
        // grab calculation
    }*/

    #endregion
}
