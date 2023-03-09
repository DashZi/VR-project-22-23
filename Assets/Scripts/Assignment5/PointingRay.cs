using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.Demo.SlotRacer.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(LineRenderer))]
public class PointingRay : MonoBehaviourPun, IPunObservable
{
    #region Member Variables

    [SerializeField] private LineRenderer lineRenderer;

    [Header("Ray parameter")] 
    public float rayWidth;
    public float idleLength = 10f;
    public float maxDistance = 50f;
    public Color idleColor;
    public Color highlightColor;
    public LayerMask layersToInclude;
    public InputActionProperty rayActivation;

    private bool isHitting;
    private Vector3 receivedStartPos;
    private Vector3 recievedFinPos;
    private Color receivedColor;
    private bool receivedEnabled;

    public Transform hand;
    #endregion

    #region MonoBehaviour Callbacks
    private void Start()
    {
        InitializeRay();
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            UpdateRay();
        }
    }
    #endregion

    #region Custom Methods
    private void InitializeRay()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;

        lineRenderer.startWidth = rayWidth;
        lineRenderer.endWidth = rayWidth;
        
        lineRenderer.positionCount = 2;

        lineRenderer.startColor = idleColor;
        lineRenderer.endColor = idleColor;
    }

    private void UpdateRay()
    {
        if (photonView.IsMine)
        {
            //Debug.Log(lineRenderer.enabled);

            if (rayActivation.action.WasPressedThisFrame()) //toggle visibility
            {
                lineRenderer.enabled = !lineRenderer.enabled;
            }
            
      
                if(lineRenderer.enabled)
                {
                    Debug.Log("Line renderer has been Enabled");
                    RaycastHit hit;

                layersToInclude = LayerMask.GetMask("Interactable");

                isHitting = Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, layersToInclude);
                

                    if (isHitting)
                    {
                        Debug.Log(hit.point);
                        
                        lineRenderer.SetPosition(0, transform.position);
                        lineRenderer.SetPosition(1, hit.point);
                        //lineRenderer.SetPosition(1, this.transform.position);

                        lineRenderer.startColor = highlightColor;
                        lineRenderer.endColor = highlightColor;


                        //lineRenderer.SetPositions(new Vector3[] { transform.position, hit.point });
                        Debug.Log("isHitting = TRUE, Ray is hitting soemthing");
                    }
                    else
                    {
                        lineRenderer.SetPosition(0, this.transform.position);
                        lineRenderer.SetPosition(1, this.transform.position + this.transform.forward * idleLength);


                        lineRenderer.startColor = idleColor;
                        lineRenderer.endColor = idleColor;
                        //lineRenderer.SetPositions(new Vector3[] { transform.position, transform.position + transform.forward * idleLength });
                        Debug.Log("isHitting = FALSE, Ray is NOT hitting anything");
                    }
                
                //else { 
                //Debug.Log("Line renderer has been Disabled");
                //}
            }
        }    
    }
    #endregion

    #region IPunObservable
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) //for other users to be visible
    {
        isHitting = Physics.Raycast(transform.position, transform.forward, layersToInclude);
        if (photonView.IsMine && stream.IsWriting)
        {
            stream.SendNext(isHitting);
        }
        else if (stream.IsReading)
        {
            isHitting = (bool)stream.ReceiveNext();
        }
    }
    #endregion
}
