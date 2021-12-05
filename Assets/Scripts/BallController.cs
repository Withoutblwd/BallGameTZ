using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameManager GameManager
    {
        get => _gameManager;
        set => _gameManager = value;
    }
    public Vector2 Velocity
    {
        get => velocity;
        set => velocity = value;
    }

    [SerializeField] private float timeToSpeed = 15;
    [SerializeField] private Vector2 velocity;
    [SerializeField] private Vector2 acceleration;
    
    private Rigidbody2D _rb;
    private float _timerToSpeed;
    private GameManager _gameManager;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _timerToSpeed = timeToSpeed;
        _rb.velocity = new Vector2(0,velocity.y);;
    }

    private void Update()
    {
        _timerToSpeed -= Time.deltaTime;
        if (_timerToSpeed <= 0)
        {
            _timerToSpeed = timeToSpeed;
            velocity += acceleration;
        }
    }

    private void OnEnable()
    {
        _timerToSpeed = timeToSpeed;
        _rb.velocity = new Vector2(0,velocity.y);
    }
    
    void OnTriggerEnter2D (Collider2D other)
    {
        Debug.Log(other);
        if (other.name == "Wall"|| other.name == "Obstacle" )
        {
           _gameManager.LoseGame();
        }
        
    }

    public void OnButtonPressed()
    {
        _rb.velocity = new Vector2(0,-velocity.y);
    }

    public void OnButtonReleased()
    {
        _rb.velocity = new Vector2(0,velocity.y);
    }
}
