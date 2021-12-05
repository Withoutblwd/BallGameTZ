using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class LoseScreenManager : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private TextMeshProUGUI difficultyText;
        [SerializeField] private TextMeshProUGUI timeText;
        [SerializeField] private TextMeshProUGUI tryCountText;
        [SerializeField] private GameObject difficultyGroup;
        
        void Start()
        {
            difficultyText.text = gameManager.Difficulty.difficultyName;
        }

        public void RestartGame()
        {
            gameObject.SetActive(false);
            gameManager.RestartGame();
        }
        
        public void SetDifficulty(Difficulty difficulty)
        {
            difficultyText.text = difficulty.difficultyName;
            gameManager.SetDifficulty(difficulty);
        }

        public void SetTryTime(int t)
        {
            timeText.text = t + " секунд";
        }

        public void SetTryCount(int tryCount)
        {
            tryCountText.text = tryCount.ToString();
        }

        public void ChangeDifficultyVisibility()
        {
            difficultyGroup.SetActive(!difficultyGroup.activeSelf);
        } 
    }
}