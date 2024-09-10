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
    private float forwardSpeed;

    private void Start()
    {
        playerModel = transform.GetChild(0);
    }
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        LocalMove(h, v, xySpeed);
        HorizontalLean(playerModel, h, 40, .1f);
    }

    private void LocalMove(float x, float y, float speed)
    {
        transform.position += new Vector3(x, y, 0) * speed * Time.deltaTime;
        ClampPosition();
    }
    void ClampPosition()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    void HorizontalLean(Transform target, float axis, float leanLimit, float lerpTime)
    {
        Vector3 targetEulerAngles = target.localEulerAngles;
        target.localEulerAngles = new Vector3(targetEulerAngles.x, targetEulerAngles.y,
            Mathf.LerpAngle(targetEulerAngles.z, -axis * leanLimit, lerpTime));
    }
}