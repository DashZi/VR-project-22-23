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
    public float maxDistance = 100f;
    public Color idleColor;
    public Color highlightColor;
    public LayerMask layersToInclude;
    public InputActionProperty rayActivation;

    private bool isHitting;

    private Vector3 receivedStartPos;
    private Vector3 receivedEndPos;
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
            Debug.Log(lineRenderer.enabled);
            if (rayActivation.action.WasPressedThisFrame()) //toggle visibility
            {
                lineRenderer.enabled = true;
                
            }
            RaycastHit hit;
            isHitting = Physics.Raycast(hand.position, hand.forward, out hit, maxDistance, layersToInclude);

            if (isHitting)
            {
                isHitting = true;
                lineRenderer.startColor = highlightColor;
                lineRenderer.endColor = highlightColor;
                lineRenderer.SetPositions(new Vector3[] { hand.position, hit.point });
            }
            else
            {
                lineRenderer.startColor = idleColor;
                lineRenderer.endColor = idleColor;
                lineRenderer.SetPositions(new Vector3[] { hand.position, hand.position + hand.forward * idleLength });
            }
        }

        
    }

    #endregion

    #region IPunObservable
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (photonView.IsMine && stream.IsWriting)
        {
            stream.SendNext(lineRenderer.GetPosition(0));
            stream.SendNext(lineRenderer.GetPosition(1));
            stream.SendNext((Vector4)lineRenderer.startColor);
            stream.SendNext(lineRenderer.enabled);
        }
        else if (stream.IsReading)
        {
            receivedStartPos = (Vector3)stream.ReceiveNext();
            receivedEndPos= (Vector3)stream.ReceiveNext();
            receivedColor = (Vector4)stream.ReceiveNext();
            receivedEnabled = (bool)stream.ReceiveNext();

            lineRenderer.SetPositions(new Vector3[] { receivedStartPos, receivedEndPos });
            lineRenderer.startColor = receivedColor;
            lineRenderer.endColor = receivedColor;
            lineRenderer.enabled = true;
        }
    }

    #endregion

}
