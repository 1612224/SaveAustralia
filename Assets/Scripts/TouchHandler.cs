using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TouchHandler : MonoBehaviour
{
    [SerializeField]
    GameBoard board;

    [Header("Touch Events")]
    public TileTouchEvent towerTouchEvent;
    public TileTouchEvent wallTouchEvent;
    public UnityEvent touchOnNothingEvent;

    [SerializeField]
    PanelsController panelsController;

    Ray TouchRay => Camera.main.ScreenPointToRay(Input.mousePosition);

    void Awake()
    {
        if (towerTouchEvent == null)
        {
            towerTouchEvent = new TileTouchEvent();
        }
        if (wallTouchEvent == null)
        {
            wallTouchEvent = new TileTouchEvent();
        }
        if (touchOnNothingEvent == null)
        {
            touchOnNothingEvent = new UnityEvent();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !panelsController.AnyPanelActive)
        {
            GameTile tile = board.GetTile(TouchRay);
            if (tile == null)
            {
                touchOnNothingEvent.Invoke();
            } 
            else if (tile.Content.Type == GameTileContentType.Tower) 
            {
                towerTouchEvent.Invoke(tile);
                //TEST
                TowerController towerCtrl = tile.Content.GetComponent<TowerController>();
                towerCtrl.UpLevel(999);
            } 
            else if (tile.Content.Type == GameTileContentType.Wall) 
            {
                wallTouchEvent.Invoke(tile);
            } else
            {
                touchOnNothingEvent.Invoke();
            }
        }
    }
}
