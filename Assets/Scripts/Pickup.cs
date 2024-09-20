using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BizitzaPickup : MonoBehaviour
{
    public float speed;
    private bool isActivated;

    public float healAmount;
    private void Update()
    {
        if (!isActivated)
        {
            transform.eulerAngles += new Vector3(0, speed, 0) * Time.deltaTime;
        }
        transform.eulerAngles += new Vector3(0, speed, 0) * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //JOKU LOGIKA GEHITU
            //PUNTUAK, BIZITZA, BOOST, AMAMENTU AKTIBAZIO TENPORALA, EZKUTUA
            GameManager.Instance.GetComponent<HealthManager>().GainHealth(healAmount);
            gameObject.GetComponent<Collider>().enabled = false;
            isActivated = true;
            transform.parent = other.transform.parent;

            isActivated = true;
            transform.parent = other.transform.parent;

            Sequence s = DOTween.Sequence();

            s.Append(transform.DORotate(new Vector3(0, 0, -900), 3f, RotateMode.LocalAxisAdd));
            s.Append(transform.DOScale(0, .5f).SetDelay(1f));
            s.AppendCallback(() => Destroy(gameObject));

        }

    }

}
