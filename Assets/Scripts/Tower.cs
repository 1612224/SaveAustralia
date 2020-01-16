﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tower : GameTileContent
{
    [SerializeField, Range(0f, 3f)]
    protected float targetingRange = 2f;
    [SerializeField, Range(1, 10)]
    int damagePerSecond = 1;
    [SerializeField]
    Transform turret = default, laserBeam = default;
    
    TargetPoint target;
    const int enemyLayerMask = 1 << 10;
    static Collider[] targetsBuffer = new Collider[1];
    Vector3 laserBeamScale;

    public TowerType towerType;

    private TowerFactory towerFactory;      
    public new TowerFactory OriginFactory
    {
        get => towerFactory;
        set
        {
            Debug.Assert(towerFactory == null, "Redefined origin factory!");
            towerFactory = value;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        laserBeamScale = laserBeam.localScale;
    }

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 position = transform.position;
        position.y += 0.01f;
        Gizmos.DrawWireSphere(position, targetingRange);

        if(target != null)
        {
            Gizmos.DrawLine(position, target.Position);
        }
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
        }
    }

    protected void Shoot()
    {
        Vector3 point = target.Position;
        laserBeam.LookAt(point);

        float d = Vector3.Distance(turret.position, target.Position);
        laserBeamScale.z = d;
        laserBeam.localScale = laserBeamScale;
        laserBeam.position = turret.position + 0.5f * d * laserBeam.forward;

        target.Enemy.ApplyDamage(damagePerSecond);
    }

    protected bool AcquireTarget(out TargetPoint target)
    {
        int hits = Physics.OverlapSphereNonAlloc(
            transform.position, targetingRange, targetsBuffer, enemyLayerMask
        );
        if (hits > 0)
        {
            target = targetsBuffer[0].GetComponent<TargetPoint>();
            Debug.Assert(target != null, "Targeted non-enemy!", targetsBuffer[0]);
            return true;
        }
        target = null;
        return false;
    }

    protected bool TrackTarget(ref TargetPoint target)
    {
        if (target == null)
        {
            return false;
        }
        Vector3 a = transform.position;
		Vector3 b = target.Position;
        if (Vector3.Distance(a, b) > targetingRange) 
        {
            target = null;
            return false;
        }
        return true;
    }

    public void AddDamage(int damage)
    {
        damagePerSecond += damage;
    }
}
