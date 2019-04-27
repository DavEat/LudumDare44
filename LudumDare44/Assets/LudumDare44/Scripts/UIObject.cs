using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIObject : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _name, _cost, _impact, _sImpact;
    [SerializeField] Image _impact_img;
    public string sName, sImpact;
    public int cost, impact;

    [SerializeField] GameObject _obj = null;

    private void Start()
    {
        Init(sName, cost, impact, sImpact);
    }

    public void Init(string name, int cost, int impact, string sImpact)
    {
        if (_name == null) _name = GetComponentInChildren<TextMeshProUGUI>();
        if (_cost == null) _cost = GetComponentInChildren<TextMeshProUGUI>();
        if (_impact == null) _impact = GetComponentInChildren<TextMeshProUGUI>();
        if (_sImpact == null) _sImpact = GetComponentInChildren<TextMeshProUGUI>();

        _name.text = sName = name;
        this.cost = cost;
        _cost.text = "" + cost;
        this.impact = impact;
        _impact.text = "" + impact;
        _sImpact.text = this.sImpact = sImpact;
    }

    public void Click()
    {
        if (GameManager.inst.player.energy >= cost)
        {
            GameManager.inst.player.energy -= cost;
            _obj.SetActive(true);
        }
    }
}
