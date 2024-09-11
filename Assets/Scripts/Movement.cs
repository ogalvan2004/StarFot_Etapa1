using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Transform playerModel;

    [Header("Settings")]
    public bool joystick;

    [Header("Parameters")]
    [SerializeField]
    private float xySpeed;
    [SerializeField]
    private float lookSpeed;

    [SerializeField]
    private float forwardSpeed;

    [SerializeField]
    private Transform aimTarget;

    private void Start()
    {
        playerModel = transform.GetChild(0);
    }
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        LocalMove(h, v, xySpeed);
        RotationLook(h, v, lookSpeed);
        HorizontalLean(playerModel, h, 40, .1f);
    }

    private void LocalMove(float x, float y, float speed)
    {
        transform.localPosition += new Vector3(x, y, 0) * speed * Time.deltaTime;
        ClampPosition();
    }
    void ClampPosition()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }


    void RotationLook(float h, float v, float speed)
    {
        aimTarget.parent.position = Vector3.zero;
        aimTarget.localPosition = new Vector3(h, v, 1);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(aimTarget.position), Mathf.Deg2Rad * speed * Time.deltaTime);
    }

    void HorizontalLean(Transform target, float axis, float leanLimit, float lerpTime)
    {
        Vector3 targetEulerAngles = target.localEulerAngles;
        target.localEulerAngles = new Vector3(targetEulerAngles.x, targetEulerAngles.y,
            Mathf.LerpAngle(targetEulerAngles.z, -axis * leanLimit, lerpTime));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(aimTarget.position, .15f);
        Gizmos.DrawWireSphere(aimTarget.position, .5f);
       
    }
}