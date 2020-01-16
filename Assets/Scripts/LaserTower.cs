using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : Tower
{
    [SerializeField, Range(1, 10)]
    int damagePerSecond = 1;
    [SerializeField]
    Transform turret = default, laserBeam = default;
    [SerializeField]
    Transform water = default;

    TargetPoint target;

    Vector3 laserBeamScale;

    float cumulatedTime;

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
        if ((TrackTarget(ref target) || AcquireTarget(out target)))
        {
            Shoot();
        }
        else
        {
            laserBeam.localScale = Vector3.zero;
            water.localScale = Vector3.zero;
        }
    }

    protected override void Shoot()
    {
        cumulatedTime += Time.deltaTime;
        Vector3 point = target.Position;
        laserBeam.LookAt(point);
        water.LookAt(point);
        water.localScale = Vector3.one;

        float d = Vector3.Distance(turret.position, target.Position);
        laserBeamScale.z = d;
        laserBeam.localScale = laserBeamScale;
        laserBeam.position = turret.position + 0.5f * d * laserBeam.forward;

        while (cumulatedTime >= 1f)
        {
            target.Enemy.ApplyDamage(damagePerSecond);
            cumulatedTime -= 1f;
        }
    }

    public override void AddDamage(int damage)
    {
        damagePerSecond += damage;
    }

    public override void UpLevel(int level)
    {
        AddDamage(level);
    }
}