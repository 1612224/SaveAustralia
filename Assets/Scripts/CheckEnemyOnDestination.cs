using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemyOnDestination : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TAG" + other.tag);
        if (other.CompareTag("Enemy"))
        {
            other.GetComponentInParent<Enemy>().OnReachDestination();
        }
    }
}
