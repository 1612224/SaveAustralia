using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockTower : Tower
{
    [SerializeField, Range(0, 200)]
    int shotsPerSecond = 1;

    [SerializeField]
    Transform mortar = default;

    TargetPoint target;

    [SerializeField, Range(0.1f, 10f)]
    float shotsSpeed = 0.3f;
    float shotsProgress;

    [SerializeField, Range(0.5f, 3f)]
    float shellBlastRadius = 2f;

    [SerializeField, Range(1, 100000)]
    int shellDamage = 1;

    // Start is called before the first frame update

    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GameUpdate();
    }

    float launchProgress;

    public override void GameUpdate()
    {
        launchProgress += shotsPerSecond;
        while (launchProgress >= 1f)
        {
            if (AcquireTarget(out TargetPoint target))
            {
                Launch(target);
                launchProgress -= 1f;
            }
            else
            {
                launchProgress = 0.999f;
            }
        }

        
    }
    public override void AddDamage(int damage)
    {
        shellDamage += damage;
        shellBlastRadius += 0.2f;
    }
 

    public void Launch(TargetPoint target)
    {
        Vector3 launchPoint = mortar.position;
        Vector3 targetPoint = target.Position;

        Vector2 dir;
        dir.x = targetPoint.x - launchPoint.x;
        dir.y = targetPoint.z - launchPoint.z;
        float x = dir.magnitude;
        dir /= x;

        shotsProgress += shotsSpeed * Time.deltaTime;
        if (shotsProgress >= 0.5f)
        {
            shotsProgress -= 0.5f;
            Game.SpawnShell().Initialize(
            launchPoint, targetPoint,
            new Vector3(x * dir.x, x, x * dir.y),
            shellBlastRadius, shellDamage);
        }       
        
    }

    public override void UpLevel(int level)
    {
    }
}
