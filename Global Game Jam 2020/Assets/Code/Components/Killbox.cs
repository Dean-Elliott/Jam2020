using UnityEngine;

public class Killbox : MonoBehaviour
{
    [SerializeField]
    private Vector3 size = new Vector3(1f, 1f, 1f);

    private Bounds bounds;

    private void Awake()
    {
        bounds = new Bounds(transform.position, size);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    }

    private void Update()
    {
        Player[] players = FindObjectsOfType<Player>();
        foreach (Player player in players)
        {
            if (bounds.Contains(player.transform.position))
            {
                //teleport player to an available position
                Teleport(player);
            }
        }
    }

    private void Teleport(Player player)
    {
        Vector3 bounds = new Vector3(30f, 0f, 10f);
        Vector3 spawnPoint = new Vector3(0f, 10f, 0f);
        int tries = 0;
        int maxTries = 1000;
        while (tries < maxTries)
        {
            Vector3 position = new Vector3(Random.Range(-bounds.x, bounds.x), 10f, Random.Range(-bounds.z, bounds.z));
            Ray ray = new Ray(position, Vector3.down);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                player.transform.position = hit.point + Vector3.up * 2f;
                player.Movement.Rigidbody.velocity = Vector3.zero;
            }

            tries++;
        }
    }
}