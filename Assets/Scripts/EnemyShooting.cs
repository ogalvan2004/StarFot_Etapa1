using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    LayerMask whatIsPlayer;

    [SerializeField]
    float attackRange;
    [SerializeField]
    float attackRate;
    [SerializeField]
    GameObject enemyBullet;
    [SerializeField]
    Transform bulletOrigin;

    bool playerInAttackRange;
    float attackCountdown = 0f;



    private void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        attackCountdown -= Time.deltaTime;
        if (playerInAttackRange) AttackPlayer();
    }

    private void AttackPlayer()
    {
        FaceTarget();
        Shoot();
    }

    void Shoot()
    {
        if(attackCountdown <= 0f)
        {
            attackCountdown =  1f/ attackRate;

            GameObject newBullet = Instantiate(enemyBullet, bulletOrigin.position, Quaternion.identity);
            newBullet.transform.forward = transform.forward.normalized;
            /*audioSource.clip = audioClip;
            audioSource.pitch = Random.Range(.6f, 1.1f);
            audioSource.Play();*/

        }

    }



    private void FaceTarget()
    {
       Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }


}
