using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldPopulatingController : MonoBehaviour
{
    private PlayerController player;
    private float oldDistance;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        oldDistance = player.distance;
    }
    
    void Update()
    {
        float distance = player.distance;
        // Do stuff
    }
}
