using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Transform player;
    public void Initialize(Transform player)
    {
        this.player = player;
        CameraInstantiate();
    }
    private void CameraInstantiate()
    {
        this.transform.position = player.position;
        transform.Translate(Vector3.back);
    }
    public void Process()
    {
        // Vector3 pos = transform.position;
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y , - 1f);
    }

}
