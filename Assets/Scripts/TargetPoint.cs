using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPoint : MonoBehaviour
{
    public Enemy Enemy { get; set; }
    public Vector3 Position => transform.position;

    // Start is called before the first frame update
    void Awake()
    {
        Enemy = transform.root.GetComponent<Enemy>();
        
        Debug.Assert(Enemy != null, "Target point without enemy root!", this);
        Debug.Assert(
            GetComponentInChildren<Collider>() != null,
            "Target must have Collider!", this);
        Debug.Assert(LayerMask.LayerToName(gameObject.layer) == "Enemy", "Target point must be on Enemy layer", this);
    }

    const int enemyLayerMask = 1 << 10;

    static Collider[] buffer = new Collider[100];

    public static int BufferedCount { get; private set; }

    public static bool FillBuffer(Vector3 position, float range)
    {
        Vector3 top = position;
        top.y += 3f;
        BufferedCount = Physics.OverlapSphereNonAlloc(
            position, range, buffer, enemyLayerMask
        );
        return BufferedCount > 0;
    }

    public static TargetPoint GetBuffered(int index)
    {
        var target = buffer[index].GetComponent<TargetPoint>();
        Debug.Assert(target != null, "Targeted non-enemy!", buffer[0]);
        return target;
    }
}
