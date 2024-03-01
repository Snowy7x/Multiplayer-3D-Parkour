using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class WinObject : MonoBehaviour
{
    public GameObject winParticles;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && other.GetComponentInParent<PhotonView>().IsMine)
        {
            GameManager.instance.Finished();
            Instantiate(winParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
