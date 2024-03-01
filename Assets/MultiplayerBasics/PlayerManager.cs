using System.IO;
using System.Linq;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Snowy.MultiplayerBasics
{
    public class PlayerManager : MonoBehaviour
    {
        PhotonView _pv;

        GameObject _controller;

        private void Awake()
        {
            _pv = GetComponent<PhotonView>();
        }

        private void Start()
        {
            if (_pv.IsMine)
                CreateController();
        }

        void CreateController()
        {
            Transform spawnPoint = SpawnManager.Instance.GetRandomSpawnPoint() ?? transform;
            _controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"),
                spawnPoint.position,
                spawnPoint.rotation, 0, new object[] { _pv.ViewID });
        }

        public void Die()
        {
            PhotonNetwork.Destroy(_controller);
        }


        public static PlayerManager Find(Player player)
        {
            return FindObjectsOfType<PlayerManager>().SingleOrDefault(x => Equals(x._pv.Owner, player));
        }
    }
}


