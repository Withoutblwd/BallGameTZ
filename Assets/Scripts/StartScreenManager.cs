using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class StartScreenManager : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private TextMeshProUGUI difficultyText;
        
        void Start()
        {
            difficultyText.text = gameManager.Difficulty.difficultyName;
        }
        
        public void SetDifficulty(Difficulty difficulty)
        {
            difficultyText.text = difficulty.difficultyName;
            gameManager.SetDifficulty(difficulty);
        }

        public void StartGame()
        {
            gameObject.SetActive(false);
            gameManager.StartGame();
        }
    }
}