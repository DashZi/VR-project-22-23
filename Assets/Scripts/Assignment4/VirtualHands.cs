using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VirtualHands : MonoBehaviour
{
    #region Member Variables

    [Header("Input Actions")] 
    public InputActionProperty rightHandGrab;
    public InputActionProperty leftHandGrab;
    public InputActionProperty switchMode;
    
    [Header("Collider")]
    public HandCollider rightHandCollider;
    public HandCollider leftHandCollider;

    [Header("Parameters")] 
    public bool reparent;

    private GameObject rightGrabbedObject;
    private GameObject leftGrabbedObject;

    //public float grabDistance = 0.5;
    private bool isHoldingObj = false;

    private Matrix4x4 rightOffsetMat; // <-- Hint
    private Matrix4x4 leftOffsetMat; // <-- Hint
    
    // Hint: you can use these matrices to store your offsets for GrabCalculation()

    #endregion

    #region MonoBehaviour Callbacks

    private void Update()
    {
        if (switchMode.action.WasPressedThisFrame())
            reparent = !reparent;
        
        SnapGrab(); // comment out when implementing your solutions
        
        if (reparent)
        {
            ReparentingGrab();
        }
        else
        {
            GrabCalculation();
        }
    }

    #endregion

    #region Custom Methods

    public void SnapGrab()
    {
        if (rightHandGrab.action.IsPressed())
        {
            if (rightGrabbedObject == null && rightHandCollider.isColliding &&
                rightHandCollider.collidingObject != leftGrabbedObject)
            {
                rightGrabbedObject = rightHandCollider.collidingObject;
            }
            else if (rightGrabbedObject != null)
            {
                rightGrabbedObject.transform.position = rightHandCollider.transform.position;
                rightGrabbedObject.transform.rotation = rightHandCollider.transform.rotation;
            }
        }
        else if (rightHandGrab.action.WasReleasedThisFrame())
        {
            rightGrabbedObject = null;
        }
    }

    public void ReparentingGrab()
    {
        // TODO: Excercise 4.2
    //Right    
        if (rightHandGrab.action.IsPressed())
        {
            if (rightGrabbedObject == null && rightHandCollider.isColliding && rightHandCollider.collidingObject != leftGrabbedObject)//* check if coliding is true
            {
               //rightHandCollider.transform.SetParent(rightGrabbedObject.transform);// change or transform its parent to RH
               rightGrabbedObject = rightHandCollider.collidingObject;
               rightGrabbedObject.transform.SetParent(rightHandCollider.gameObject.transform, true);
            }
        }
        else if (rightHandGrab.action.WasReleasedThisFrame())// or when i leave the obj the new parent is null
        {
            rightGrabbedObject.transform.SetParent(null);
            rightGrabbedObject = null;//parent becomes null and stays there
        }
    //Left    
        if (leftHandGrab.action.IsPressed())
        {
            if (leftGrabbedObject == null && leftHandCollider.isColliding && leftHandCollider.collidingObject != rightGrabbedObject)//* check if coliding is true
            {
               //rightHandCollider.transform.SetParent(rightGrabbedObject.transform);// change or transform its parent to RH
               leftGrabbedObject = leftHandCollider.collidingObject;
               leftGrabbedObject.transform.SetParent(leftHandCollider.gameObject.transform, true);
            }
        }
        else if (leftHandGrab.action.WasReleasedThisFrame())// or when i leave the obj the new parent is null
        {
            leftGrabbedObject.transform.SetParent(null);
            leftGrabbedObject = null;//parent becomes null and stays there
        }
    }

    public void GrabCalculation()
    {
        // TODO: Excercise 4.2
    //Right    
        if (rightHandGrab.action.IsPressed())
        {
            if (rightGrabbedObject == null && rightHandCollider.isColliding && rightHandCollider.collidingObject != leftGrabbedObject)//* check if coliding is true
            {
               //rightHandCollider.transform.SetParent(rightGrabbedObject.transform);// change or transform its parent to RH
               rightGrabbedObject = rightHandCollider.collidingObject;
            }
            else if (rightGrabbedObject != null)
            {
                rightGrabbedObject.transform.position = rightHandCollider.transform.position;
                rightGrabbedObject.transform.rotation = rightHandCollider.transform.rotation;
            }
        }
        else if (rightHandGrab.action.WasReleasedThisFrame())
        {
            rightGrabbedObject = null;
        }   
    }

    
    /// <summary>
    /// Returns TRS-Matrix for t
    /// if world is true, the matrix is given in world space, if world is false it's given in local space
    /// </summary>
    /// <param name="t"></param>
    /// <param name="world"></param>
    /// <returns></returns>
    public Matrix4x4 GetTransformationMatrix(Transform t, bool world = true)
    {
        if (world)
        {
            return Matrix4x4.TRS(t.position, t.rotation, t.lossyScale);
        }
        else
        {
            return Matrix4x4.TRS(t.localPosition, t.localRotation, t.localScale);
        }
    }
    
    #endregion
}
