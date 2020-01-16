using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : Tower
{
    [SerializeField, Range(0, 200)]
    int damagePerSecond = 5;

    [SerializeField]
    Transform turret = default, laserBeam = default;

    TargetPoint target;

    Vector3 laserBeamScale;

    // Start is called before the first frame update
    void Awake()
    {
        laserBeamScale = laserBeam.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        GameUpdate();
    }

    public override void GameUpdate()
    {
        if (transform.root.GetComponent<TowerController>() &&
            transform.root.GetComponent<TowerController>()._CurrLevelIndex > 0 &&
            TrackTarget(ref target) || AcquireTarget(out target))
        {
            Shoot();
        }
        else
        {
            laserBeam.localScale = Vector3.zero;
        }
    }
    public override void AddDamage(int damage)
    {
        damagePerSecond += damage;
    }
    public override void Shoot()
    {
        Vector3 point = target.Position;
        laserBeam.LookAt(point);
        //laserBeam.localRotation = turret.localRotation;

        float d = Vector3.Distance(turret.position, target.Position);
        laserBeamScale.z = d;
        laserBeam.localScale = laserBeamScale;
        laserBeam.position = turret.position + 0.5f * d * laserBeam.forward;

        target.Enemy.ApplyDamage(damagePerSecond);
    }
}
