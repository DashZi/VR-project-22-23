using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//using UnityEngine.XR.Interaction.Toolkit;

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

    public Transform tmpParent;
    //private XRController xr;

    private Matrix4x4 rightOffsetMat; // <-- Hint
    private Matrix4x4 leftOffsetMat; // <-- Hint
    
    // Hint: you can use these matrices to store your offsets for GrabCalculation()
   
    #endregion

    #region MonoBehaviour Callbacks

    //void Start()
    //{
//        xr = (XRController)GameObject.FindObjectOfType(typeof(XRController));
   // }

// void ActivateHaptic()
    //{
    //    xr.SendHapticImpulse(0.7f, 2f);
   // }
    private void Update()
    {
        if (switchMode.action.WasPressedThisFrame())
            reparent = !reparent;
        
        //SnapGrab(); // comment out when implementing your solutions
        
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
               //Matrix4x4 NewMatrixO_1 = NewMatrixO_1.Multiply(rightGrabbedObject, rightOffsetMat);
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
