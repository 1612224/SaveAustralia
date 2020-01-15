using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerCanvasController : MonoBehaviour
{

    public string _TowerCanvasTag = "RockTowerCanvas";
    private GameObject _CurrTowerChosen;

    public GameObject _TowerCanvas;
    public TextMeshProUGUI _GoldText;

    
    // Start is called before the first frame update
    void Start()
    {
        GetTowerCanvas();
    }

    public void initial(string canvasTag)
    {
        _TowerCanvasTag = canvasTag;
        GetTowerCanvas();
    }

    private void GetTowerCanvas()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            Transform child = transform.GetChild(i);
            if (child.gameObject.tag == _TowerCanvasTag)
            {
                _TowerCanvas = child.gameObject;
                break;
            }
        }
    }

    int UILayerMask = 1 << 11;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
       
            if (Physics.Raycast(ray, out hit,float.MaxValue,UILayerMask))
            {
                Debug.Log(hit.transform.GetInstanceID());
                ClickHandler(hit.transform);
            }
        }
    }
    
    private void ClickHandler(Transform hitObject)
    {
        Debug.Log(hitObject.GetInstanceID());
        Debug.Log("AAa");
        Debug.Log(_TowerCanvas);
        for(int i=0;i< _TowerCanvas.transform.childCount; ++i)
        {
            Debug.Log(_TowerCanvas.transform.GetChild(i).gameObject.GetInstanceID());
            if(hitObject.gameObject.GetInstanceID() == _TowerCanvas.transform.GetChild(i).gameObject.GetInstanceID() && _CurrTowerChosen != null)
            {
               
                _CurrTowerChosen.GetComponent<TowerController>().TowerUpLevelClick();
                _CurrTowerChosen.GetComponent<TowerController>().HideDisplayCanvas();
            }
        }

    }

    public void SetCurrTower(GameObject tower)
    {
        _CurrTowerChosen = tower;
    }

    public void SetActiveAllRockTowerCanvas(bool active)
    {
        for(int i=0; i< _TowerCanvas.transform.childCount; ++i)
        {
            SetActiveRockTowerCanvas(i, active);
        }
    }

    public void SetActiveRockTowerCanvas(int index, bool value)
    {
        _TowerCanvas.transform.GetChild(index).gameObject.SetActive(value);
    }

  


    public void DrawCanvasToPosition(GameObject canvas,Vector3 position, bool active)
    {
        canvas.SetActive(active);
        Vector3 outPosition;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(this.GetComponent<RectTransform>(),
            Camera.main.WorldToScreenPoint(position),
            Camera.main, out outPosition);
        canvas.transform.position = outPosition;
    }
}
