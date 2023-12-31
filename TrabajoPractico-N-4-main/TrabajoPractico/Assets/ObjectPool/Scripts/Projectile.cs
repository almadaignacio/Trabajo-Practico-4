﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public TurretAI.TurretType type = TurretAI.TurretType.Single;
    public Transform target;
    [SerializeField] private bool lockOn;
    [SerializeField] private float speed = 1;
    [SerializeField] private float turnSpeed = 1;
    [SerializeField] private bool catapult;

    [SerializeField] private float knockBack = 0.1f;
    [SerializeField] private float boomTimer = 1;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private int poolAmmount;


    private void Start()
    {
        if (catapult)
        {
            lockOn = true;
        }
    }

    private void Update()
    {
        if (target == null)
        {
            Explosion();
        }

        if (type == TurretAI.TurretType.Single)
        {
            Vector3 dir = target.position - transform.position;
            transform.rotation = Quaternion.LookRotation(dir);
        }

        if (transform.position.y < -0.2F)
        {
            Explosion();
        }

        switch(type)
        {
            case TurretAI.TurretType.Catapult:
                if (lockOn)
                {
                    Vector3 Vo = CalculateCatapult(target.transform.position, transform.position, 1);
                    transform.GetComponent<Rigidbody>().velocity = Vo;
                    lockOn = false;
                }
                break;
            case TurretAI.TurretType.Dual:
                Vector3 dir = target.position - transform.position;
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, dir, Time.deltaTime * turnSpeed, 0.0f);
                Debug.DrawRay(transform.position, newDirection, Color.red);

                transform.Translate(Vector3.forward * Time.deltaTime * speed);
                transform.rotation = Quaternion.LookRotation(newDirection);
                break;
            case TurretAI.TurretType.Single:
                float singleSpeed = speed * Time.deltaTime;
                transform.Translate(transform.forward * singleSpeed * 2, Space.World);
                break;
            default:
                break;
        }
    }

    Vector3 CalculateCatapult(Vector3 target, Vector3 origen, float time)
    {
        Vector3 distance = target - origen;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0;

        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Vector3 dir = other.transform.position - transform.position;
            Vector3 knockBackPos = other.transform.position + (dir.normalized * knockBack);
            knockBackPos.y = 0.2f;
            other.transform.position = knockBackPos;
            Explosion();
        }
    }

    public void Explosion()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        this.gameObject.SetActive(false);
        lockOn = true;
    }
}



