using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    
    public string _TowerLevelsTag = "RockTower";
    public List<int> _TowerGoldCost = new List<int>();
    private List<Transform> _ChildTowerObject = new List<Transform>();
    public int _CurrLevelIndex = 0;
    private bool _IsTowerCanvasShowing = false;

    private TowerCanvasController _TowerCanvasController;

    void Start()
    {
        GetChildObject();
    }
    public void initial(Canvas canvas,string towerTag, string canvasTag,List<int> golds)
    {

        _TowerLevelsTag = towerTag;
        GetChildObject();

        _TowerGoldCost.Clear();
        _TowerGoldCost = golds;

        _TowerCanvasController = canvas.GetComponent<TowerCanvasController>();
        _TowerCanvasController.initial(canvasTag);
    }
  
    private void GetChildObject()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            Transform child = transform.GetChild(i);
            if (child.gameObject.tag == _TowerLevelsTag)
            {
                _ChildTowerObject.Add(child);
            }
        }
    }

    public int UpLevel(int gold)
    {
        if(_IsTowerCanvasShowing && _CurrLevelIndex + 1 < _ChildTowerObject.Count && gold >= _TowerGoldCost[_CurrLevelIndex+1])
        {
            ++_CurrLevelIndex;
            for(int i = 0; i < _ChildTowerObject.Count; ++i)
            {
                _ChildTowerObject[i].gameObject.SetActive(false);
            }
            _ChildTowerObject[_CurrLevelIndex].gameObject.SetActive(true);
            transform.root.GetComponent<Tower>().AddDamage(_CurrLevelIndex);
            return _TowerGoldCost[_CurrLevelIndex];
        }
        return 0;
    }

    

    public void HideDisplayCanvas()
    {
        // Show/Hide canvas options
        if (_CurrLevelIndex + 1 < _ChildTowerObject.Count)
        {
            _IsTowerCanvasShowing = !_IsTowerCanvasShowing;
            if (_IsTowerCanvasShowing)
            {
                _TowerCanvasController.SetCurrTower(gameObject);
            }
            else
            {
                _TowerCanvasController.SetCurrTower(null);
            }
            HideAndDisplayTowerCanvasNextLevel();
        }
    }

    public void TowerUpLevelClick()
    {
        UpLevel(100);
    }

    public int GetNextLevelGold()
    {
        return _CurrLevelIndex + 1 < _ChildTowerObject.Count ? _TowerGoldCost[_CurrLevelIndex + 1]:-1;
    }

   
    public bool IsChildObjectClicked(Transform tower)
    {
        for (int i = 0; i < _ChildTowerObject.Count; ++i)
        {
            if (tower.gameObject.GetInstanceID() == _ChildTowerObject[i].gameObject.GetInstanceID())
            {
                return true;
            }
        }
        return false;
    }

    public void HideAndDisplayTowerCanvasNextLevel()
    {
        _TowerCanvasController.SetActiveAllRockTowerCanvas(false);
        _TowerCanvasController._GoldText.gameObject.SetActive(false);

        if(_IsTowerCanvasShowing && _CurrLevelIndex +1 < _ChildTowerObject.Count)
        {
            // Draw image
            _TowerCanvasController.DrawCanvasToPosition(_TowerCanvasController._TowerCanvas.transform.GetChild(_CurrLevelIndex).gameObject,
                new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1, gameObject.transform.position.z),
                _IsTowerCanvasShowing);

            //Draw gold text
            _TowerCanvasController.DrawCanvasToPosition(_TowerCanvasController._GoldText.gameObject,
                   new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2, gameObject.transform.position.z),
                   _IsTowerCanvasShowing);
            int gold = GetNextLevelGold();
            _TowerCanvasController._GoldText.SetText(gold == -1 ? "" : gold.ToString());
        }
    }

    
}
