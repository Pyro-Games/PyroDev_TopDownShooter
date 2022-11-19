using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody _rb;
    public Rigidbody RB 
    { 
        get => _rb;
        private set
        {
            _rb = value;
        }
    }

    private Coroutine _shootCoroutine;


    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    internal void InitBullet()
    {
        _shootCoroutine = StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        yield return new WaitUntil(() => _rb != null);
        _rb.AddForce(transform.forward * 1000);

        yield return new WaitForSeconds(2.0f);
        BulletPooling.instance.DeactivateBullet(this);
    }
}
