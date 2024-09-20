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
    private void OnTriggerEnter(Collider other)
    {
        bool check = false;

       if (other.gameObject.CompareTag("Player"))
        {
            //Kendu bizitza playerrari
            GameManager.Instance.GetComponent<HealthManager>().LoseHealth(10);
            Destroy(gameObject);
            check = true;
        }
        Destroy(gameObject);

       if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

}
