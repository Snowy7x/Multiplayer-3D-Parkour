using Photon.Pun;
using UnityEngine;

public class NetworkClass : MonoBehaviour
{
    [Header("Online")]
    public bool isOffline;
    public bool isMine;

    public virtual void SetUp(bool isMine, bool isOffline)
    {
        TH9
        this.isMine = isMine;
        this.isOffline = isOffline;
    }
}


public class Player : MonoBehaviour
{
    public bool isOffline;
    public PhotonView photonView;
    public NetworkClass[] networkClasses;
    public GameObject camObject;
    public Movement movement;
    public void Awake()
    {
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            camObject.SetActive(true);
            if (GameManager.instance)
            {
                GameManager.instance.SetLocalPlayer(this);
            }
        }
        else
        {
            camObject.SetActive(false);
        }

        foreach (var cClass in networkClasses)
        {
            cClass.SetUp(photonView.IsMine, isOffline);
        }

    }

    public void Die()
    {
        movement.Die();
    }
}
