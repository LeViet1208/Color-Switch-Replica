using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    void OgerEnter2D(Collider2D collision)
    {
        if (Spawner.instance != null)
            Spawner.instance.SpawnNextObstacle();

        Destroy(collision.gameObject);
    }
}
