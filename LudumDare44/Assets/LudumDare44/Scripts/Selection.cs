using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    //bool canSelect = true;

    float _distance = 50;
    [SerializeField] LayerMask _selectionLayer = 0;

    SelectableObj _lastSelected = null;

    [SerializeField] LeaveKillParent _UI = null;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, _distance, _selectionLayer))
            {
                SelectableObj so = hit.collider.GetComponent<SelectableObj>();
                if (so != null)
                {
                    if (_lastSelected != null)
                        _lastSelected.Diselect();
                    _lastSelected = so;
                    _lastSelected.Select();

                    _UI.SetPosition(Camera.main.WorldToScreenPoint(_lastSelected._transform.position), so.robot, so);

                    //float _minDst = 270f, _maxDst = 480f;
                    //float scaleMul = 1f - (((transform.position - hit.point).sqrMagnitude - _minDst) / (_maxDst - _minDst)) * .2f;
                    //_UI.localScale = Vector2.one * scaleMul;

                    Debug.Log(string.Format("{0} has been selected", _lastSelected.sName));
                }
            }
            //else _UI.anchoredPosition = Vector2.one * -10;
        }
    }
}
