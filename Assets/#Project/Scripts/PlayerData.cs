using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game Design/PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField] public float playerSpeed = 2.5f;
    [SerializeField] public Vector3 playerStartPos;
    
}
