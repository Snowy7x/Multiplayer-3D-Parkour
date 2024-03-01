using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Snowy.MultiplayerBasics
{
    public class SpawnManager : MonoBehaviour
    {
        public static SpawnManager Instance;

        public bool isChildren = true;
        public List<Transform> spawnPoints;
        List<Transform> _usedSpawnPoints;

        void Awake()
        {
            Instance = this;

            if (isChildren)
            {
                spawnPoints = new List<Transform>();
                foreach (Transform child in transform)
                {
                    spawnPoints.Add(child);
                }
            }
        }

        public Transform GetRandomSpawnPoint()
        {
            if (_usedSpawnPoints == null)
            {
                _usedSpawnPoints = new List<Transform>();
            }

            if (_usedSpawnPoints.Count == spawnPoints.Count)
            {
                _usedSpawnPoints.Clear();
            }

            Transform spawnPoint = null;
            do
            {
                int randomIndex = Random.Range(0, spawnPoints.Count);
                spawnPoint = spawnPoints[randomIndex];
            } while (_usedSpawnPoints.Contains(spawnPoint));

            _usedSpawnPoints.Add(spawnPoint);

            return spawnPoint;
        }
    }
}

