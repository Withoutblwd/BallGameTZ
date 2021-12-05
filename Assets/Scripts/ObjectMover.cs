using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ObjectMover : MonoBehaviour
    {
        public GameManager GameManager
        {
            get => gameManager;
            set => gameManager = value;
        }
        
        [SerializeField] private GameManager gameManager;
        [SerializeField] protected float _boundaryPosX;
        [SerializeField] private float depth = 1f;
        
        private BallController _ballController;
        private Rigidbody2D _rb;
        private float _initialPosX;

        public void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void Start()
        {
            _initialPosX = transform.position.x;
        }

        public void OnEnable()
        {
            _ballController = gameManager.BallController;
        }

        

        public void FixedUpdate()
        {
            if (!(_ballController is null))
            {
                _rb.velocity = new Vector2(-_ballController.Velocity.x/depth,0);
                if(_rb.position.x < _boundaryPosX) OnBoundaryReached();
            }
        }

        protected virtual void OnBoundaryReached()
        {
            _rb.position = new Vector2(_initialPosX,_rb.position.y);
        }
    }
}