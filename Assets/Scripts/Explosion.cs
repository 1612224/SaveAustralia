using UnityEngine;

public class Explosion : WarEntity
{

    [SerializeField, Range(0f, 5f)]
    float duration = 0.5f;
    [SerializeField]
    AnimationCurve opacityCurve = default;

    [SerializeField]
    AnimationCurve scaleCurve = default;
    float age;
    static int colorPropertyID = Shader.PropertyToID("_Color");

    static MaterialPropertyBlock propertyBlock;
	
	float scale;


    void Awake()
    {
        //meshRenderer = GetComponent<MeshRenderer>();
        //Debug.Assert(meshRenderer != null, "Explosion without renderer!");
    }

    public void Initialize(Vector3 position, float blastRadius, int damage)
    {
        TargetPoint.FillBuffer(position, blastRadius);
        for (int i = 0; i < TargetPoint.BufferedCount; i++)
        {
            TargetPoint.GetBuffered(i).Enemy.ApplyDamage(damage);
        }
        transform.localPosition = position;
        scale = blastRadius * 0.5f;
    }

    public override bool GameUpdate()
    {
        age += Time.deltaTime;
        if (age >= duration)
        {
            OriginFactory.Reclaim(this);
            return false;
        }
        if (propertyBlock == null)
        {
            propertyBlock = new MaterialPropertyBlock();
        }
        float t = age / duration;
        transform.localScale = Vector3.one * (scale * scaleCurve.Evaluate(t));
        return true;
    }
}