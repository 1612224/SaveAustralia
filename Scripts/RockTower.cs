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
            if (transform.root.GetComponent<TowerController>() &&
                transform.root.GetComponent<TowerController>()._CurrLevelIndex > 0 &&
                AcquireTarget(out TargetPoint target))
            {
                Launch(target);
                launchProgress -= 1f;
            }
            else
            {
                launchProgress = 0.999f;
            }
        }
        //if (transform.root.GetComponent<TowerController>() &&
        //    transform.root.GetComponent<TowerController>()._CurrLevelIndex > 0 &&
        //    AcquireTarget(out target))
        //{
        //    mortar.localScale = Vector3.one;
        //    Shoot();
        //}
        //else
        //{
        //    mortar.localScale = Vector3.zero;
        //}
        
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

        Debug.DrawLine(launchPoint, targetPoint, Color.yellow);
        Debug.DrawLine(
            new Vector3(launchPoint.x, 0.01f, launchPoint.z),
            new Vector3(
                launchPoint.x + dir.x * x, 0.01f, launchPoint.z + dir.y * x
            ),
            Color.white
        );

        float g = 9.81f;
        float s = 0.6f* x / 0.707f;


        Vector3 prev = launchPoint, next;
     
        float cosTheta = 0.707f;
        float sinTheta = 0.707f;
        for (int i = 1; i <= 10; i++)
        {
            float t = 2*i / 10f;
            float dx = s * cosTheta * t;
            float dy = s * sinTheta * t - 0.5f * g * t * t;
            dy = launchPoint.y + dy >= targetPoint.y ? dy :0;
            next = launchPoint + new Vector3(dir.x * dx, dy, dir.y * dx);
            Debug.DrawLine(prev, next, Color.blue);
            prev = next;
        }

        shotsProgress += shotsSpeed * Time.deltaTime;
        if (shotsProgress >= 0.5f)
        {
            shotsProgress -= 0.5f;
            Game.SpawnShell().Initialize(
            launchPoint, targetPoint,
            new Vector3(s * cosTheta * dir.x, s * sinTheta, s * cosTheta * dir.y),
            shellBlastRadius, shellDamage);
        }

        
        
    }
}
