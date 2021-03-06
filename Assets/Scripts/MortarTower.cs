﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarTower : Tower
{
    [SerializeField, Range(0.5f, 2f)]
    float shotsPerSecond = 1f;

    [SerializeField]
    Transform mortar = default;
    float launchProgress = 0f;
    float launchSpeed;
    [SerializeField, Range(0.5f, 3f)]
    float shellBlastRadius = 1f;

    [SerializeField, Range(1, 100)]
    int shellDamage = 30;
    void Awake()
    {
        OnValidate();
    }

    void Update()
    {
        GameUpdate();
    }

    void OnValidate()
    {
        float x = targetingRange + 0.25001f;
        float y = 1f;
        launchSpeed = Mathf.Sqrt(9.81f * (y + Mathf.Sqrt(x * x + y * y)));
    }

    public override void GameUpdate()
    {
        launchProgress += shotsPerSecond * Time.deltaTime;
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

    public void Launch(TargetPoint target)
    {
        Vector3 launchPoint = mortar.position;
        launchPoint.y = 1f;
        Vector3 targetPoint = target.Position;
        targetPoint.y = 0f;
        Vector2 dir;
        dir.x = targetPoint.x - launchPoint.x;
        dir.y = targetPoint.z - launchPoint.z;
        float x = dir.magnitude;
        float y = -launchPoint.y;
        dir /= x;
        float g = 9.81f;
        float s = launchSpeed;
        float s2 = s * s;

        float r = s2 * s2 - g * (g * x * x + 2f * y * s2);
        float tanTheta = (s2 + Mathf.Sqrt(r)) / (g * x);
        float cosTheta = Mathf.Cos(Mathf.Atan(tanTheta));
        float sinTheta = cosTheta * tanTheta;
        mortar.localRotation =
            Quaternion.LookRotation(new Vector3(dir.x, 0, dir.y));
        
        Game.SpawnShell().Initialize(
            launchPoint, targetPoint,
            new Vector3(s * cosTheta * dir.x, s * sinTheta, s * cosTheta * dir.y),
            shellBlastRadius, shellDamage
        );        
    }

    public override void UpLevel(int level)
    {
        AddDamage(level);
    }

    public override void AddDamage(int level)
    {
        shellDamage += 5 * level;
    }
}
