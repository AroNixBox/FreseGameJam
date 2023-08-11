using UnityEngine;

public class Camerafollow : MonoBehaviour
{
    public Transform player; // Setze dieses Feld im Unity-Editor auf deinen Spieler.
    public Vector3 offset; // Distanz von der Kamera zum Spieler.

    private void Update()
    {
        Vector3 newPos = player.position + offset;
        transform.position = newPos;
    }
}