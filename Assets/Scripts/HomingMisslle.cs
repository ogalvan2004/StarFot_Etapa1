using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class HomingMisslle : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;


    public float missileSpeed;

    private Transform target;

    public Transform Target { get => target; set => target = value; }

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        rb.velocity = transform.forward * missileSpeed;
    }

    private void LateUpdate()
    {
        gameObject.transform.LookAt(Target);
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

}
