using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditStyleScroll : MonoBehaviour
{
    [SerializeField]
    AnimationCurve speedCurve = default;

    [SerializeField, Range(0f, 100f)]
    float speed = 3f;

    [SerializeField]
    float topOffsetPercent = 0f;

    float topOffset;
    float begin;
    // Start is called before the first frame update
    void Start()
    {
        begin = transform.position.y;
        topOffset = Screen.height * (1-topOffsetPercent);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= topOffset)
        {
            float t = (transform.position.y - begin) / (topOffset - begin);
            float speed = this.speed * speedCurve.Evaluate(t);
            Vector3 pos = transform.position;
            pos.y += speed;
            transform.position = pos;
        }
    }
}
