using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightSwitch : MonoBehaviourPunCallbacks
{
    #region Member Variables

    public List<Light> lightSources;
    public InputActionProperty lightToggleAction;

    private bool lightOn = true;
    
    #endregion

    #region MonoBehaviour Callbacks

    private void Start()
    {
        foreach (var light in lightSources)
        {
            light.intensity = lightOn ? 1f : 0f;
        }
    }

    private void Update()
    {
        if(lightToggleAction.action.WasPressedThisFrame())
            ToggleLight();
    }

    #endregion

    #region Custom Methods

    private void ToggleLight()
    {
        
    }

    #endregion

    #region PUN Callbacks

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        
    }

    #endregion

    #region RPCs

    [PunRPC]
    public void ToggleLightRPC()
    {
        // use to toggle lights
    }

    [PunRPC]
    public void SendStateRPC(bool lightOn)
    {
        // use to inform late joined users
    }

    #endregion
}