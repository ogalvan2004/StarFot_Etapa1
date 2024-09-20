using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Bullet shooting")]
    [Space]
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Transform bulletOrigin;

    [Header("Misile shooting")]
    [Space]
    public float sphereRadius;
    public float maxDistance;
    public float currentDistance;

    public LayerMask targetMask;
    public GameObject missile;
    public Transform missileOrigin;
    public List<GameObject> targetList = new List<GameObject>();
    /*public Transform missileOrigin*/

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
        if(Input.GetMouseButtonDown(1))
        {
            //Fire missile
            if(targetList.Any())
            {
                FireHomingMissiles();

            }         
        }
        //Erregistratu etsaiak misilentzako
        LockInTtargets();
    }

    void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, bulletOrigin.position, Quaternion.identity);
        newBullet.transform.forward = transform.forward.normalized;
        audioSource.clip = audioClip;
        audioSource.pitch = Random.Range(.6f, 1.1f);
        audioSource.Play();
    }

    void LockInTtargets()
    {
        if (Physics.SphereCast(transform.position, sphereRadius, transform.forward, out RaycastHit hit, maxDistance, targetMask))  
        {
            currentDistance = hit.distance;
            GameObject hitObject = hit.transform.gameObject;
            if(!targetList.Contains(hitObject))
            {
                targetList.Add(hitObject);
                if(hitObject.GetComponent<TargetedAnimation>())
                {
                    hit.transform.gameObject.GetComponent<TargetedAnimation>().Targeted(true);
                }
            }       
        }
        else
        {
            currentDistance = maxDistance;
        }
    }

    void FireHomingMissiles()
    {
        for(int i = 0; i < targetList.Count; i++)
        {
            if (targetList[1] != null)
            {
                 GameObject newMissile = Instantiate(missile, missileOrigin.position, Quaternion.identity);
                 newMissile.GetComponent<HomingMisslle>().Target = targetList[i].transform;
            }
           
        }
        targetList.Clear(); 
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * currentDistance);
        Gizmos.DrawWireSphere(transform.position + transform.forward * currentDistance, sphereRadius);
    }
}
