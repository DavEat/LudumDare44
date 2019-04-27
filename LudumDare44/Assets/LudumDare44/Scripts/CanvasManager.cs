using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager inst;

    private void Awake() { inst = this; }

    [SerializeField] RectTransform _campsiteInfo = null;
    [SerializeField] RectTransform _campsiteSpendEnergy = null;

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
}
