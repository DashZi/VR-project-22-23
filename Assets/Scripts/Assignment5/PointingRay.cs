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
        
    }

    #endregion

    #region IPunObservable

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (photonView.IsMine && stream.IsWriting)
        {
            
        }
        else if (stream.IsReading)
        {
            
        }
    }

    #endregion

}
