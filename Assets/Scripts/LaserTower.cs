using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : Tower
{
    [SerializeField]
    Transform turret = default, laserBeam = default;
    Vector3 laserBeamScale;
    [SerializeField, Range(1, 10)]
    protected int damagePerSecond = 1;

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
        if (transform.root.GetComponent<TowerController>() &&
            transform.root.GetComponent<TowerController>()._CurrLevelIndex > 0 &&
            (TrackTarget(ref target) || AcquireTarget(out target)))
        {
            Shoot();
        }
        else
        {
            laserBeam.localScale = Vector3.zero;
        }
    }

    protected void Shoot()
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

    public override void UpLevel(int level)
    {
        AddDamage(level);
    }
    public void AddDamage(int damage)
    {
        damagePerSecond += damage;
    }
}
