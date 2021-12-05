using UnityEngine;


[CreateAssetMenu(fileName = "DifficultySettings", menuName = "Difficulty", order = 0)]
public class Difficulty : ScriptableObject
{
    public string difficultyName;
    public Vector2 playerSpeed;
}
