using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : Tower
{
    [SerializeField]
    Transform turret = default, laserBeam = default;
    Vector3 laserBeamScale;

    // Start is called before the first frame update
    void Awake()
    {
        laserBeamScale = laserBeam.localScale;
    }

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    public override TowerType TowerType => TowerType.Laser;

    // Update is called once per frame
    void Update()
    {
        GameUpdate();
    }

    public override void GameUpdate()
    {
        if (TrackTarget(ref target) || AcquireTarget(out target))
        {
            Shoot();
        }
        else
        {
            laserBeam.localScale = Vector3.zero;
        }
    }

    protected override void Shoot()
    {
        Vector3 point = target.Position;
        laserBeam.LookAt(point);
        //laserBeam.localRotation = turret.localRotation;

        float d = Vector3.Distance(turret.position, target.Position);
        laserBeamScale.z = d;
        laserBeam.localScale = laserBeamScale;
        laserBeam.position = turret.position + 0.5f * d* laserBeam.forward;

        target.Enemy.ApplyDamage(damagePerSecond);
    }
}
