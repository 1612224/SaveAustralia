using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlaerController : MonoBehaviour
{

    public Camera cam;
    public NavMeshAgent player;
 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //Debug.Log("Mouse Clicked");
            //Debug.Log(ray.origin);

            if(Physics.Raycast(ray,out hit))
            {
                //Debug.Log("Physics raycast");
                //Debug.Log(hit.point);
                //player.SetDestination(hit.point);
            }
        }
        
    }
}
