  using UnityEngine;

[CreateAssetMenu(fileName = "OpossumData", menuName = "Game Design/OpossumData")]
public class OpossumData : ScriptableObject
{
    [SerializeField] public float location_y = -0.5f;
    [SerializeField] public float delaySpawn = 0.5f;
    [SerializeField] public float delayStartled = 0.5f;
    [SerializeField] public float runAwaySpeed = 2.5f;

}
