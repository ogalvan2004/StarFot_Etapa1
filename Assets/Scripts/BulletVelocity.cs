using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletVelocity : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float bulletSpeed;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * bulletSpeed;
    }
}
