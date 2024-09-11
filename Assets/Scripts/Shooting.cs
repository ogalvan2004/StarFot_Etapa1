using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Bullet shooting")]
    [Space]
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Transform bulletOrigin;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip audioClip;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, bulletOrigin.position, Quaternion.identity);
        newBullet.transform.forward = transform.forward.normalized;
        audioSource.clip = audioClip;
        audioSource.pitch = Random.Range(.6f, 1.1f);
        audioSource.Play();
    }

}
