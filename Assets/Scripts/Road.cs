using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private int roadPartsQuantity;
    [SerializeField] private GameObject roadPartPrefab;
    [SerializeField] private float noVanishDistance;

    private float roadPartLength;
    private LinkedList<GameObject> roadParts = new LinkedList<GameObject>();

    private PlayerController player;

    private void Awake()
    {
        roadPartLength = roadPartPrefab.GetComponent<MeshFilter>().sharedMesh.bounds.size.z * roadPartPrefab.transform.localScale.z;
        FirstBuild();
        player = FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        RearrangeParts();
    }

    private void FirstBuild()
    {
        for (int i = 0; i < roadPartsQuantity; i++)
        {
            GameObject newPart = Instantiate(roadPartPrefab, transform);
            MovePartAtEnd(newPart);
            roadParts.AddLast(newPart);
        }
    }

    private void RearrangeParts()
    {
        GameObject firstPart = roadParts.First.Value;
        while (firstPart.transform.position.z + noVanishDistance < player.distance)
        {
            MovePartAtEnd(firstPart);
            roadParts.AddLast(firstPart);
            roadParts.RemoveFirst();
            firstPart = roadParts.First.Value;
        }
    }

    private void MovePartAtEnd(GameObject roadPart)
    {
        Vector3 pos = (roadParts.Count == 0) ? Vector3.zero : roadParts.Last.Value.transform.position + new Vector3(0, 0, roadPartLength);
        roadPart.transform.position = pos;
    }
}
