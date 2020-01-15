using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Tower : GameTileContent
{
    [SerializeField, Range(10.5f, 20.5f)]
    protected float targetingRange = 16.5f;
    [SerializeField, Range(50f, 200f)]
    protected float damagePerSecond = 50f;


    const int enemyLayerMask = 1 << 10;
    static Collider[] targetsBuffer = new Collider[10];
    private TowerFactory towerFactory;

    // Start is called before the first frame update
    void Awake()
    {

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

        //if(target != null)
        //{
        //    Gizmos.DrawLine(position, target.Position);
        //}
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public TowerFactory OriginFactory
    {
        get => towerFactory;
        set
        {
            Debug.Assert(towerFactory == null, "Redefined origin factory!");
            towerFactory = value;
        }
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
        if (Vector3.Distance(a, b) > targetingRange + 0.25f * target.Enemy.Scale) 
        {
            target = null;
            return false;
        }
        return true;
    }
    public void AddDamage(float damage)
    {
        damagePerSecond += damage;
    }
}
