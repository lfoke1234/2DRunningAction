using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform[] levelPart;
    [SerializeField] private Vector3 nextPartPosition;

    [SerializeField] private float distanceToSpawn;
    [SerializeField] private float distanceToDelete;
     private Transform player;

    void Start()
    {
        player = PlayerManager.instance.player.transform;
    }

    void Update()
    {
        GeneratePlatform();
        DeletePlatform();
    }

    private void GeneratePlatform()
    {
        while(Vector2.Distance(player.transform.position, nextPartPosition) < distanceToSpawn)
        {
            Transform part = levelPart[Random.Range(0, levelPart.Length)];

            Vector2 newPosition = new Vector2(nextPartPosition.x - part.Find("Start").position.x, 0);
            Transform newPart = Instantiate(part, nextPartPosition - part.Find("Start").position, transform.rotation, transform);

            nextPartPosition = newPart.Find("End").position;
        }
    }

    private void DeletePlatform()
    {
        if (transform.childCount > 0)
        {
            Transform partToDelete = transform.GetChild(0);

            if (Vector2.Distance(player.transform.position, partToDelete.transform.position) >  distanceToDelete)
            {
                Destroy(partToDelete.gameObject);
            }
        }
    }
}
