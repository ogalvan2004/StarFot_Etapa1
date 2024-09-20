using Cinemachine;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Movement : MonoBehaviour
{
    private Transform playerModel;

    [SerializeField]
    private CinemachineDollyCart dollyCart;

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
    [SerializeField]
    TrailRenderer trailParticles;
    [SerializeField]
    TrailRenderer trailParticles2;

    [SerializeField]
    private Transform cameraParent;

    private void Start()
    {
        playerModel = transform.GetChild(0);
        SetSpeed(forwardSpeed);
    }
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        LocalMove(h, v, xySpeed);
        RotationLook(h, v, lookSpeed);
        HorizontalLean(playerModel, h, 40, .1f);
        if (Input.GetButtonDown("BarrelRollLeft") || Input.GetButtonDown("BarrelRollRight"))
        {
            int dir = Input.GetButtonDown("BarrelRollLeft") ? -1 : 1;
            QuickSpin(dir);
        }
        if (Input.GetButtonDown("Boost"))
        {
            Boost(true);
        }
        if (Input.GetButtonUp("Boost"))
        {
            Boost(false);
        }
    }
    #region MovementLogic
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
        gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation,
            Quaternion.LookRotation(aimTarget.position, dollyCart.transform.up), Mathf.Deg2Rad * speed * Time.deltaTime);
    }

    void HorizontalLean(Transform target, float axis, float leanLimit, float lerpTime)
    {
        Vector3 targetEulerAngles = target.localEulerAngles;
        target.localEulerAngles = new Vector3(targetEulerAngles.x, targetEulerAngles.y,
            Mathf.LerpAngle(targetEulerAngles.z, -axis * leanLimit, lerpTime));
    }

    void SetSpeed(float speed)
    {
        dollyCart.m_Speed = speed;
    }
    #endregion

    #region Actions
    void QuickSpin(int dir)
    {
        if (!DOTween.IsTweening(playerModel))
        {
            playerModel.DOLocalRotate(new Vector3(playerModel.localEulerAngles.x, playerModel.localEulerAngles.y, 360 * -dir), .4f,
                RotateMode.LocalAxisAdd).SetEase(Ease.OutSine);
            //Collider desaktibatu
            //Partikula batzuk aktibatu 

        }
    }

    void Boost(bool state)
    {
        if(state)
        {
            cameraParent.GetComponentInChildren<CinemachineImpulseSource>().GenerateImpulse();
            //trailParticles.Play();
        }
        else
        {
            //trailParticles.Stop();
        }
        trailParticles.emitting = state;
        trailParticles2.emitting = state;

        float zoom = state ? -10 : 0;

        float originChrom = state ? 0 : 1;
        float endChrom = state ? 1 : 0;

        float originFieldOfView = state ? 40 : 40;
        float endFieldOfView = state ? 40 : 40;

        float speed = state ? forwardSpeed * 2 : forwardSpeed;

        DOVirtual.Float(dollyCart.m_Speed, speed, .15f, SetSpeed);
        DOVirtual.Float(originChrom, endChrom, .5f, Chromatic);
        DOVirtual.Float(originFieldOfView, endFieldOfView, .5f, FieldOfView);

        SetCameraZoom(zoom, .4f);
    }
    #endregion

    #region PostProcessing
    void SetCameraZoom(float zoom, float duration)
    {
        cameraParent.DOLocalMove(new Vector3(0, 0, zoom), duration);
    }
    void FieldOfView(float fov)
    {
        cameraParent.GetComponentInChildren<CinemachineVirtualCamera>().m_Lens.FieldOfView = fov;
    }
    void Chromatic(float x)
    {
        Camera.main.GetComponent<Volume>().profile.TryGet(out ChromaticAberration c);
        c.intensity.value = x;
    }
    #endregion
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(aimTarget.position, .15f);
        Gizmos.DrawWireSphere(aimTarget.position, .5f);
    }
}