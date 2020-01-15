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
            GetComponent<SphereCollider>() != null,
            "Target without SphereCollider!", this);

        Debug.Assert(gameObject.layer == 10, "Target point on wrong layer", this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
