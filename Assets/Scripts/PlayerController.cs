using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(0, 3), SerializeField] int currentLane = 3;
    [SerializeField] float swipeCentimeters = 2;
    [SerializeField] float laneChangeManeuverTime = 0.4f;
    [SerializeField] float currentSpeed = 15f;
    [SerializeField] float acceleration = 0.1f;

    public float distance => transform.position.z;
    public bool isAlive { get; private set; }

    private Rigidbody rb;

    private float xSmoothDampVel;

    private Camera mainCamera;
    private Vector2 mouseOrigin;
    private bool swipeFinished;

    private static float pixels2cm => 1f / Screen.dpi * 2.54f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        isAlive = true;
        rb.position = new Vector3(LaneManager.GetX(currentLane), rb.position.y, rb.position.z);

        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        if (!isAlive)
            return;

        float deltaX = ManeueverMovement();
        float deltaZ = ForwardMovement();
        LookRotation(new Vector3(deltaX, 0, deltaZ));
    }

    /// <summary>
    /// Returns delta Z
    /// </summary>
    private float ForwardMovement()
    {
        float deltaZ = currentSpeed * Time.deltaTime;
        rb.position += new Vector3(0, 0, deltaZ);
        currentSpeed += acceleration * Time.deltaTime;
        return deltaZ;
    }

    /// <summary>
    /// Returns delta X
    /// </summary>
    private float ManeueverMovement()
    {
        float currentX = rb.position.x;
        float targetX = LaneManager.GetX(currentLane);
        float newX = Mathf.SmoothDamp(currentX, targetX, ref xSmoothDampVel, laneChangeManeuverTime, 
                                      maxSpeed: float.PositiveInfinity, Time.fixedDeltaTime);
        float deltaX = newX - currentX;
        rb.position = new Vector3(newX, rb.position.y, rb.position.z);
        return deltaX;
    }

    private void LookRotation(Vector3 direction)
    {
        rb.rotation = Quaternion.LookRotation(direction);
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            swipeFinished = false;
        }
        else if(swipeFinished)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            mouseOrigin = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Input.mousePosition;
            float deltaX = mousePosition.x - mouseOrigin.x;
            if(Mathf.Abs(deltaX) * pixels2cm > swipeCentimeters)
            {
                ChangeLane(right: deltaX > 0);
                swipeFinished = true;
            }
        }
    }

    private void ChangeLane(bool right)
    {
        if (right)
        {
            if (currentLane < LaneManager.maxLane) currentLane++;
        }
        else
        {
            if (currentLane > LaneManager.minLane) currentLane--;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isAlive)
            return;

        isAlive = false;
        rb.isKinematic = false;
        rb.velocity = (Random.onUnitSphere + new Vector3(0, 1, 1)) * currentSpeed;
        rb.angularVelocity = Random.onUnitSphere * 180;

        var otherRb = other.GetComponent<Rigidbody>();
        if (otherRb != null)
        {
            other.isTrigger = false;
            otherRb.isKinematic = false;
            otherRb.velocity = (Random.onUnitSphere + new Vector3(0, 1, 1)) * currentSpeed;
            otherRb.angularVelocity = Random.onUnitSphere * 180;
        }
    }
}
