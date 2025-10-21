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
    private void CameraMove()
    {
        // Vector3 pos = transform.position;
        float posX = Mathf.Clamp(player.transform.position.x, -13, 18);
        float posY = Mathf.Clamp(player.transform.position.y, -7, 26);
        transform.position = new Vector3(posX, posY , - 1f);
        // transform.position = new Vector3(player.transform.position.x, player.transform.position.y , - 1f);
    }
    public void Process()
    {
        CameraMove();
    }

}
