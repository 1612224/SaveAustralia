using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public List<string> _levels_UI = new List<string>(){ "TowerUIlv1", "TowerUIlv2", "TowerUIlv3" };
    private List<string> _levels = new List<string>() { "Towerlv0","Towerlv1", "Towerlv2", "Towerlv3" };
    private int[] _gold_cost = {0, 5, 10, 15 };
    private List<Transform> childTowerOBject = new List<Transform>();
    private List<Transform> childTowerUIButton = new List<Transform>();
    private int _curr_level = 0;
    private int MAX_LEVEL = 3;
    private bool _is_showing_UI_button = false;

    void Start()
    {
        
        for(int i =0;i < transform.childCount; ++i)
        {

            Transform child = transform.GetChild(i);
            for (int j =0; j < _levels_UI.Count; ++j)
            {
                if(child.gameObject.tag == _levels_UI[j])
                {
                    childTowerUIButton.Add(child);
                    break;
                }
                
                
            }
            for (int j = 0; j < _levels.Count; ++j)
            {
                if (child.gameObject.tag == _levels[j])
                {
                    childTowerOBject.Add(child);
                    break;
                }
                
            }
            
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    public bool isCapableUpLevel(int gold)
    {
        if (_is_showing_UI_button && _curr_level < MAX_LEVEL && gold <= _gold_cost[_curr_level+1])
        {
            return true;
        }
        return false;
    }
    public int UpLevel(int gold)
    {
        if(_is_showing_UI_button && _curr_level < MAX_LEVEL && gold >= _gold_cost[_curr_level+1])
        {
            ++_curr_level;
            for(int i = 0; i < childTowerOBject.Count; ++i)
            {
                childTowerOBject[i].gameObject.SetActive(false);
            }
            Debug.Log(_curr_level);
            childTowerOBject[_curr_level].gameObject.SetActive(true);
            return _gold_cost[_curr_level];
        }
        return 0;

    }

    

    public void Click(Transform ClickedObject, int gold)
    {
        
        if (ClickedObject == null && _curr_level-1 < MAX_LEVEL)
        {
            _is_showing_UI_button = !_is_showing_UI_button;
            DisplayChildUIButtonLevel();
        }
       
        else if (ClickedObject != null && _is_showing_UI_button && IsChildUIButtonClicked(ClickedObject) )
        {
            
            UpLevel(gold);
            _is_showing_UI_button = false;
            DisplayChildUIButtonLevel();


        }
    }

    public bool IsChildUIButtonClicked(Transform button)
    {
        Debug.Log(button.gameObject.GetInstanceID());
        for(int i = 0; i < childTowerUIButton.Count; ++i)
        {
            Debug.Log(childTowerUIButton[i].gameObject.GetInstanceID());
            if (button.gameObject.GetInstanceID() == childTowerUIButton[i].gameObject.GetInstanceID())
            {
                return true;
            }
        }
        return false;
    }

    public void DisplayChildUIButtonLevel()
    {
        for (int i = 0; i < childTowerUIButton.Count; ++i)
        {
            childTowerUIButton[i].gameObject.SetActive(false);
        }
        if(_curr_level < MAX_LEVEL)
        {
            childTowerUIButton[_curr_level].gameObject.SetActive(_is_showing_UI_button);
        }
                     
        
      
    }

    public float Damage()
    {
        return 100 + _curr_level * 100;
    }

}
