using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerGroupController : MonoBehaviour
{


    public static GameObject _CurrClickedTower = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    int TowerLayer = 1 << 9;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if(Physics.Raycast(ray,out hit, float.MaxValue,TowerLayer))
            {
               //Debug.Log(hit.transform.GetInstanceID());
               ClickHandler(hit.transform);
            }
        }
    }

    private void ClickHandler(Transform ClickedObject)
    {
        TowerController script;
        if(_CurrClickedTower == null)
        {
            script = ClickedObject.GetComponent<TowerController>();
            _CurrClickedTower = ClickedObject.gameObject;
            
            
            script.HideDisplayCanvas();
        }
        else
        {
            //script = _CurrClickedTower.gameObject.GetComponentInParent<TowerController>();
            //script.HideDisplayCanvas();
            _CurrClickedTower = null;
        }
        
        
    }


}
