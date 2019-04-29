using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class LeaveKill : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] LeaveKillParent parent = null;
    [SerializeField] bool _kill = false;
    bool _robot {
        get
        {
            return parent.robot;
        }
    }

    [SerializeField] UIMenu _UIMenu = null;

    [SerializeField] TextMeshProUGUI _electProd = null;
    [SerializeField] TextMeshProUGUI _gearStock = null;
    [SerializeField] TextMeshProUGUI _foodProd = null;
    [SerializeField] TextMeshProUGUI _foodStock = null;

    public void OnClick()
    {
        if (_robot)
        {
            _UIMenu.robotCount--;
            _UIMenu.robots.Remove(parent.so.sName);
            if (_kill)
                _UIMenu.v_gear_stock += 15;
        }
        else
        {
            _UIMenu.humanCount--;
            _UIMenu.humans.Remove(parent.so.sName);
            if (_kill)
                _UIMenu.v_food_stock += 15;
        }

        parent.so.Removed();

        _UIMenu.Wait();

        OnPointerExit(null);
        parent.SetPosition(Vector2.one * -10, true, null);
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //leave or kill
        if (_robot)
            SetValue(-_UIMenu.elec_prod_by_robot, _electProd);
        else SetValue((-_UIMenu.v_food_prod) / _UIMenu.humanCount, _foodProd);

        //kill
        if (_kill) //kill
        {
            if (_robot)
                SetValue(15, _gearStock);
            else SetValue(15, _foodStock);
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (_robot)
        {
            _electProd.gameObject.SetActive(false);
            _gearStock.gameObject.SetActive(false);
        }
        else
        {
            _foodProd.gameObject.SetActive(false);
            _foodStock.gameObject.SetActive(false);
        }
    }

    void SetValue(int v, TextMeshProUGUI txt)
    {
        txt.gameObject.SetActive(true);

        string s = "";

        if (v > 0)
        {
            s = "+";
            txt.color = Color.green;
        }
        else txt.color = Color.red;

        s += v;
        txt.text = s;
    }
}
