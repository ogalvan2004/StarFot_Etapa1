using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;

public class HomingMisslle : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    GameObject explosion;


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
      if (other.gameObject.CompareTag("Player"))
        {
            //Kendu bizitza playerrari
            Destroy(gameObject);
        }
      else if (other.gameObject.CompareTag("Enemy"))
        {
            Instantiate(explosion, other.gameObject.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
      else
        {
            Destroy(gameObject);
        }

        
    }

}
