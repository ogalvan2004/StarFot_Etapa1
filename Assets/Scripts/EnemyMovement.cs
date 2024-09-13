using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

   [SerializeField]
    private CinemachineDollyCart dollyCart;
    [SerializeField]
    private float enemySpeed;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            SetSpeed(enemySpeed);
        }
    }
    void SetSpeed(float speed)
    {
        dollyCart.m_Speed = speed;
    }

}
