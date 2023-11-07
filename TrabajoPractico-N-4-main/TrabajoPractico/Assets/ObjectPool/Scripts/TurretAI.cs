using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TurretAI : MonoBehaviour {

    public enum TurretType
    {
        Single = 1,
        Dual = 2,
        Catapult = 3,
    }
    
    private GameObject currentTarget;
    [SerializeField] private Transform turreyHead;

    [SerializeField] private float attackDist = 10.0f;
    [SerializeField] private float attackDamage;
    [SerializeField] private float shootCoolDown;
    private float timer;
    [SerializeField] private float loockSpeed;

    private Vector3 randomRot;
    private Animator animator;

    [Header("[Turret Type]")]
    [SerializeField] private TurretType turretType = TurretType.Single;

    [SerializeField] private Transform muzzleMain;
    [SerializeField] private Transform muzzleSub;
    [SerializeField] private GameObject muzzleEff;
    [SerializeField] private GameObject bullet;
    private bool shootLeft = true;

    private Transform lockOnPos;

    void Start () {
        InvokeRepeating("ChackForTarget", 0, 0.5f);

        if (transform.GetChild(0).GetComponent<Animator>())
        {
            animator = transform.GetChild(0).GetComponent<Animator>();
        }

        randomRot = new Vector3(0, Random.Range(0, 359), 0);
    }
	
	void Update () {
        if (currentTarget != null)
        {
            FollowTarget();

            float currentTargetDist = Vector3.Distance(transform.position, currentTarget.transform.position);

            if (currentTargetDist > attackDist)
            {
                currentTarget = null;
            }
        }
        else
        {
            IdleRotate();
        }

        timer += Time.deltaTime;
        if (timer >= shootCoolDown)
        {
            if (currentTarget != null)
            {
                timer = 0;
                
                if (animator != null)
                {
                    animator.SetTrigger("Fire");
                    Shoot(turretType);
                }
                else
                {
                    Shoot(turretType);
                }
            }
        }
	}

    private void ChackForTarget()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, attackDist);
        float distAway = Mathf.Infinity;

        for (int i = 0; i < colls.Length; i++)
        {
            if (colls[i].tag == "Player")
            {
                float dist = Vector3.Distance(transform.position, colls[i].transform.position);
                if (dist < distAway)
                {
                    currentTarget = colls[i].gameObject;
                    distAway = dist;
                }
            }
        }
    }

    private void FollowTarget() 
    {
        Vector3 targetDir = currentTarget.transform.position - turreyHead.position;
        targetDir.y = 0;
        if (turretType == TurretType.Single)
        {
            turreyHead.forward = targetDir;
        }
        else
        {
            turreyHead.transform.rotation = Quaternion.RotateTowards(turreyHead.rotation, Quaternion.LookRotation(targetDir), loockSpeed * Time.deltaTime);
        }
    }

    Vector3 CalculateVelocity(Vector3 target, Vector3 origen, float time)
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

    public void IdleRotate()
    {
        bool refreshRandom = false;

        if (turreyHead.rotation != Quaternion.Euler(randomRot))
        {
            turreyHead.rotation = Quaternion.RotateTowards(turreyHead.transform.rotation, Quaternion.Euler(randomRot), loockSpeed * Time.deltaTime * 0.2f);
        }
        else
        {
            refreshRandom = true;

            if (refreshRandom)
            {
                int randomAngle = Random.Range(0, 359);
                randomRot = new Vector3(0, randomAngle, 0);
                refreshRandom = false;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDist);
    }

    public void Shoot(TurretType turretType)
    {
        switch(turretType)
        {
            case TurretType.Catapult:
                ShootBullet(muzzleMain);
                break;
            case TurretType.Dual:
                if(shootLeft)
                {
                    ShootBullet(muzzleMain);
                }
                else
                {
                    ShootBullet(muzzleSub);
                }
                shootLeft = !shootLeft;
                break;
            case TurretType.Single:
                ShootBullet(muzzleMain);
                break;
            default:
                break;
        }
    }

    public void ShootBullet(Transform spawnBullet)
    {
        Instantiate(muzzleEff, spawnBullet.transform.position, spawnBullet.transform.rotation);

        GameObject prefab = ObjectPool.Instance.GetPooledObject();
        if (prefab != null)
        {
            Projectile projectile = prefab.GetComponent<Projectile>();
            projectile.target = currentTarget.transform;
            prefab.SetActive(true);
            prefab.transform.position = spawnBullet.position;
            prefab.transform.rotation = spawnBullet.rotation;
        }
    }
}
