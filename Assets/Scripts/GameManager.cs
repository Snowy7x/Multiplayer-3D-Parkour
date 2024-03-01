using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Snowy.MultiplayerBasics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class LB_Pos
{
    public string name;
    public int pos;
    public string timeTaken;
    public int actorNumber;
}

[Serializable]
public class Leaderbaord
{
    public List<LB_Pos> lbPos = new List<LB_Pos>();
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    PhotonView _pv;
    [Header("Game Settings")]
    public float time;
    //public float timeLimit;
    public Leaderbaord lb;
    public Player player;
    
    public Transform leaderboardContainer;
    public GameObject leaderboardItemPrefab;
    private Dictionary<int, LeaderboardItem> _leaderboardItems = new Dictionary<int, LeaderboardItem>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        
        _pv = GetComponent<PhotonView>();
        lb = new Leaderbaord();
        time = 0;
    }
    
    public void SetLocalPlayer(Player player)
    {
        this.player = player;
    }

    private void Update()
    {
        time += Time.deltaTime;
    }

    public void Finished()
    {
        string timeTaken = time.ToString("F2");
        time = 0;
        _pv.RPC(nameof(RPC_Finished), RpcTarget.All, timeTaken, PhotonNetwork.LocalPlayer.ActorNumber, PhotonNetwork.LocalPlayer.NickName);
    }
    
    [PunRPC]
    public void RPC_Finished(string timeTaken, int actorNumber, string playerName)
    {
        // check if already in leaderboard
        if (lb.lbPos.Find(x => x.actorNumber == actorNumber) == null)
        {
            // add to leaderboard
            lb.lbPos.Add(new LB_Pos()
            {
                name = playerName,
                pos = lb.lbPos.Count + 1,
                timeTaken = timeTaken,
                actorNumber = actorNumber
            });
            GameObject leaderboardItem = Instantiate(leaderboardItemPrefab, leaderboardContainer);
            leaderboardItem.GetComponent<LeaderboardItem>().Init(lb.lbPos.Count, playerName, timeTaken);
            _leaderboardItems.Add(actorNumber, leaderboardItem.GetComponent<LeaderboardItem>());
        }
        else
        {
            // update leaderboard
            LB_Pos lbPos = lb.lbPos.Find(x => x.actorNumber == actorNumber);
            lbPos.timeTaken = timeTaken;
            _leaderboardItems[actorNumber].Init(lbPos.pos, lbPos.name, timeTaken);
        }
    }

    public void Click()
    {
        var pos = 3;
        var an = PhotonNetwork.LocalPlayer.ActorNumber;
        //...
        _pv.RPC(nameof(RPC_Click), RpcTarget.All, pos, an);
    }

    [PunRPC]
    public void RPC_Click(int pos, int an)
    {
        if (an != PhotonNetwork.LocalPlayer.ActorNumber)
        {
            // The other player
            // pos...
        }
        else
        {
            // ...
            // ...
        }
    }

}
