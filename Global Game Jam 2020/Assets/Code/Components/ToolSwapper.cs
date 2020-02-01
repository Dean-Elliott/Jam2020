using System.Collections.Generic;
using UnityEngine;

public class ToolSwapper : MonoBehaviour
{
    [SerializeField]
    private GameObject[] players = { };

    [SerializeField]
    private Vector3 size = new Vector3(2f, 2f, 2f);

    [SerializeField]
    private Vector3 offset = new Vector3(0f, 1f, 0f);

    private List<int> controllerIndeces = new List<int>();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position + offset, size);
    }

    private void Update()
    {
        Bounds bounds = new Bounds(transform.position + offset, size);
        Player[] players = FindObjectsOfType<Player>();
        foreach (Player player in players)
        {
            if (bounds.Contains(player.transform.position))
            {
                if (!controllerIndeces.Contains(player.ControllerIndex))
                {
                    controllerIndeces.Add(player.ControllerIndex);
                    SwapPlayer(player);
                }
            }
            else
            {
                if (controllerIndeces.Contains(player.ControllerIndex))
                {
                    controllerIndeces.Remove(player.ControllerIndex);
                }
            }
        }
    }

    private void SwapPlayer(Player player)
    {
        Player[] allPlayers = FindObjectsOfType<Player>();

        //remove existing player types
        List<Player> validPrefabs = new List<Player>();
        foreach (GameObject prefab in players)
        {
            Player p = prefab.GetComponent<Player>();
            if (p)
            {
                //if this player exists, dont add to list
                bool alreadyExists = false;
                foreach (Player existingPlayer in allPlayers)
                {
                    if (existingPlayer.GetType() == p.GetType())
                    {
                        alreadyExists = true;
                        continue;
                    }
                }

                if (alreadyExists)
                {
                    continue;
                }
            }
            
            validPrefabs.Add(p);
        }

        Destroy(player.gameObject);

        Player newPlayer = Instantiate(validPrefabs[Random.Range(0, validPrefabs.Count)]);
        newPlayer.transform.position = player.transform.position;
        newPlayer.transform.eulerAngles = player.transform.eulerAngles;
        newPlayer.ControllerIndex = player.ControllerIndex;
    }
}