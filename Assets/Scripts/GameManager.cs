using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public UnityEvent onLose;
    public BallController BallController => _ballController;
    public Difficulty Difficulty => _difficulty;
    
    [SerializeField] private Difficulty _difficulty;
    [SerializeField] private EventTrigger flyButtonEventTrigger;
    [SerializeField] private LoseScreenManager loseScreen;
    [SerializeField] private GameObject environmentGameObject;
    [SerializeField] private Transform playerSpawnPos;
    [SerializeField] private GameObject playerPrefab;
    
    private BallController _ballController;
    private float _startTime;
    private int _tryCount = 0;
    
    public void SetDifficulty(Difficulty difficulty)
    {
        _difficulty = difficulty;
    }

    public void StartGame()
    {
        LoadPlayer();
        environmentGameObject.SetActive(true);

        _startTime = Time.time;
        _tryCount++;
    }

    public void RestartGame()
    {
        _ballController.Velocity = _difficulty.playerSpeed;
        _ballController.transform.position = playerSpawnPos.position;
        _ballController.gameObject.SetActive(true);
        environmentGameObject.SetActive(true);
        
        _startTime = Time.time;
        _tryCount++;
    }

    public void LoseGame()
    {
        loseScreen.SetTryTime((int)(Time.time - _startTime));
        loseScreen.SetTryCount(_tryCount);
        loseScreen.gameObject.SetActive(true);
        _ballController.gameObject.SetActive(false);
        environmentGameObject.SetActive(false);
        onLose.Invoke();
    }

    void LoadPlayer()
    {
        GameObject goplayer = Instantiate(playerPrefab, playerSpawnPos.position, Quaternion.identity);
        _ballController = goplayer.GetComponent<BallController>();
        _ballController.Velocity = _difficulty.playerSpeed;
        _ballController.GameManager = this;
        
        EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry();
        pointerDownEntry.eventID = EventTriggerType.PointerDown;
        pointerDownEntry.callback.AddListener((data) => { _ballController.OnButtonPressed(); });
        
        EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry();
        pointerUpEntry.eventID = EventTriggerType.PointerUp;
        pointerUpEntry.callback.AddListener((data) => { _ballController.OnButtonReleased(); });
        
        flyButtonEventTrigger.triggers.Add(pointerDownEntry);
        flyButtonEventTrigger.triggers.Add(pointerUpEntry);
    }
}
