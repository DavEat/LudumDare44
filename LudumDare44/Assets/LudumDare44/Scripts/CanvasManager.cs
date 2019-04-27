using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager inst;

    private void Awake() { inst = this; }

    [SerializeField] RectTransform _campsiteInfo = null;
    [SerializeField] RectTransform _campsiteSpendEnergy = null;
    [SerializeField] RectTransform _campsiteSpendables = null;

    public void EnableCsInfo(bool e)
    {
        if (_campsiteInfo.gameObject.activeSelf != e)
            _campsiteInfo.gameObject.SetActive(e);
    }

    public void EnableCsSpendEnergy(bool e)
    {
        if (_campsiteSpendEnergy.gameObject.activeSelf != e)
            _campsiteSpendEnergy.gameObject.SetActive(e);
    }

    public void SelectSpendables()
    {
        if (_campsiteSpendables.childCount > 1)
        {
            _campsiteSpendables.GetChild(1).GetComponent<Selectable>().Select();
            _campsiteSpendables.GetChild(0).GetComponent<Selectable>().Select();
        }
    }
}
