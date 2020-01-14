using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    GameBoard board = default;

    Ray TouchRay => Camera.main.ScreenPointToRay(Input.mousePosition);
 
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleTouch();
        }
    }
    void HandleTouch()
    {
        GameTile tile = board.GetTile(TouchRay);
        if (tile != null)
        {
            GetComponent<NavMeshAgent>().SetDestination(tile.GetComponent<Transform>().position);
        }
    }
}
