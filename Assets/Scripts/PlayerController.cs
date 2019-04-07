using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float startSpeed = 15f;
    [SerializeField] float acceleration = 0.1f;

    public float distance => transform.position.z;

    private Rigidbody rb;
    private bool isAlive;
    private float currentSpeed;
    // can be [0; 3]
    private int lane = 3;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = startSpeed;
        isAlive = true;
    }

    private void FixedUpdate()
    {
        if (!isAlive) return;

        rb.position += new Vector3(0, 0, currentSpeed * Time.deltaTime);
        currentSpeed += acceleration * Time.deltaTime;
    }
    
}
