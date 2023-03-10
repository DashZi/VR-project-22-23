using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Ingredient : MonoBehaviour
{
    Vector3 originalPos;
    //Vector3 originalRot;
    public GameObject ihgredient;
    
    // [SerializeField] XRBaseInteractable grabbedobj;
    // private Pose _origin;
    // private Rigidbody rb;

    // private void OnEnabled()
    // {
    //     grabbedobj.selectExited.AddListener(ObjectReleased);
    // }

    // private void OnDisabled()
    // {
    //     grabbedobj.selectExited.RemoveListener(ObjectReleased);
    // }

    // private void Awake()
    // {
    //     _origin.position = this.transform.position;
    //     _origin.rotation = this.transform.rotation;
    //     rb = GetComponent<Rigidbody>();
    // }

    // private void ObjectReleased(SelectExitEventArgs arg0)
    // {
    //     rb.Sleep();

    //     GetComponent<Collider>().enabled = false;

    //     this.transform.position = _origin.position;
    //     this.transform.rotation = _origin.rotation;

    //     rb.WakeUp();

    //     GetComponent<Collider>().enabled = true;
    // }

    void Start()
    {
        originalPos = new Vector3(ihgredient.transform.position.x, ihgredient.transform.position.y, ihgredient.transform.position.z);
        //originalRot = new Vector3(ihgredient.transform.rotation.x, ihgredient.transform.rotation.y, ihgredient.transform.rotation.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("the obj is the ingredient");
        if(other.gameObject.tag == "MagicPot")
        {
            new WaitForSeconds(420);
            ihgredient.transform.position = originalPos;
            //ihgredient.transform.rotation = originalRot;
        }
    }
}
