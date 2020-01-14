using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGroupController : MonoBehaviour
{

    private bool _a_tower_is_clicked = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if(Physics.Raycast(ray,out hit))
            {
               ClickHandller(hit.transform);
            }
        }
    }

    private void ClickHandller(Transform ClickedObject)
    {

        TowerController script = ClickedObject.gameObject.GetComponentInParent<TowerController>();
        

        if(script != null)
        {
            if (!_a_tower_is_clicked)
            {
                Debug.Log("clicked");
                _a_tower_is_clicked = true;
                script.Click(null, 0);
            }
            else if(script.IsChildUIButtonClicked(ClickedObject))
            {
                Debug.Log("child clicked");
                _a_tower_is_clicked = false;
                script.Click(ClickedObject, 100);
            }
            else
            {
                _a_tower_is_clicked = false;
                script.Click(null, 0);
            }

        }
        
    }


}
