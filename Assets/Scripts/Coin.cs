using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] float angularSpeed = 180;

    private void Update()
    {
        transform.Rotate(Vector3.up, angularSpeed * Time.deltaTime);
    }
}
