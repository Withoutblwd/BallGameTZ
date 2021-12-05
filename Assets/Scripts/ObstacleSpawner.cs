using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private int spawnEvery = 15;
        [SerializeField] private Vector2 spawnPosMin;
        [SerializeField] private Vector2 spawnPosMax;
        [SerializeField] private float boundaryPosX;
        [SerializeField] private Obstacle obstaclePrefab;
        
        private ObjectPool<Obstacle> _obstaclesPool;
        private BallController _ballController;
        private float _playerWayX;
        private void Start()
        {
            gameManager.onLose.AddListener(OnLose);
            _ballController = gameManager.BallController;
            _obstaclesPool = new ObjectPool<Obstacle>(() =>
            {
                Obstacle obstacle = Instantiate(obstaclePrefab,parent: transform);
                obstacle.name = "Obstacle";
                obstacle.transform.parent = transform;
                obstacle.GameManager = gameManager;
                obstacle.Init(ObstacleUpdate,boundaryPosX);
                return obstacle;
            }, obstacle =>
            {
                obstacle.transform.position = Vector2.Lerp(spawnPosMin,spawnPosMax,Random.value);
                obstacle.gameObject.SetActive(true);
            } , obstacle =>
            {
                obstacle.gameObject.SetActive(false);
            });
        }

        private void FixedUpdate()
        {
            _playerWayX += _ballController.Velocity.x * Time.fixedDeltaTime;
            if (_playerWayX > spawnEvery)
            {
                _obstaclesPool.Get();
                _playerWayX = 0;
            }
        }

        private void ObstacleUpdate(Obstacle obstacle)
        {
            _obstaclesPool.Release(obstacle);
        }

        private void OnLose()
        {
            _obstaclesPool.ReleaseAll();
        }
    }
}