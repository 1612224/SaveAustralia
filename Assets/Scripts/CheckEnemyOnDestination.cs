using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemyOnDestination : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponentInParent<Enemy>().OnReachDestination();
        }
    }
}
