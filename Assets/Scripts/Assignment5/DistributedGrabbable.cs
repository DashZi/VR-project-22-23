using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class DistributedGrabbable : MonoBehaviourPun
{
    #region Member Variables

    private bool isGrabbed = false; // initial state of the object 
                                    
    private Photon.Realtime.Player owner = null; //
    private PhotonView photonView;
    //private bool owner;

    #endregion

    #region Custom Methods

    void Start()
    {
        photonView = GetComponent<PhotonView>();

        // Set the ownership callback
        photonView.OwnershipTransfer = OwnershipOption.Takeover;
        //photonView.AddCallbackTarget((IPhotonViewCallback)this);
    }


    public bool RequestGrab()
    {
        //if (this.photonView.Owner == null && PhotonNetwork.LocalPlayer.IsMasterClient)
        //{
        //    this.photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
        //    isGrabbed = true;
        //}
        //return isGrabbed;
        //return false;

        if (!isGrabbed)
        {
            // Request ownership of the object
            photonView.RequestOwnership();
        }
        else
        {
            // Release ownership of the object
            Release();
        }

        isGrabbed = !isGrabbed;
        return isGrabbed;
    }

    public void Release()
    {
        //PhotonView photon = GetComponent<PhotonView>();
        //this.photonView.TransferOwnership(PhotonNetwork.MasterClient);
        photonView.TransferOwnership(PhotonNetwork.MasterClient);
        //isGrabbed = false;
        //return isGrabbed;

    }
    #endregion
    // Ownership callback methods
    public void OnOwnershipRequest(PhotonView targetView, Photon.Realtime.Player requestingPlayer)
    {
        // Check if the object is already owned
        if (owner != null)
        {
            // Deny ownership request
            targetView.TransferOwnership(requestingPlayer);
        }
        else
        {
            // Accept ownership request
            targetView.TransferOwnership(requestingPlayer);
            owner = requestingPlayer;
        }
    }

    public void OnOwnershipTransfered(PhotonView targetView, Photon.Realtime.Player previousOwner)
    {
        owner = photonView.Owner;
    }

    

    #region RPCS

    [PunRPC]
    public void GrabRPC(bool b)
    {
       // isGrabbed = true;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // send the state of the object to other players
            stream.SendNext(isGrabbed);
        }
        else if (stream.IsReading)
        {
            isGrabbed = (bool)stream.ReceiveNext();
        }
    }

    #endregion
}
