using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Game Design/GameData")]
public class GameData : ScriptableObject
{
    [field: SerializeField] public PlayerData PlayerData { get; private set; }    
    [field: SerializeField] public OpossumData OpossumData { get; private set; }    
}
