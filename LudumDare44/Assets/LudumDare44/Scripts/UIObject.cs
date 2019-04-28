using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    UIMenu _UIMenu;

    [SerializeField] GameObject _obj = null, _objFinal = null;
    bool built = false;
    [SerializeField] Target[] _targets = null;

    [SerializeField] bool _isLeave = false;

    public void OnClick()
    {
        if (_UIMenu == null)
            _UIMenu = GetComponentInParent<UIMenu>();

        bool success = _UIMenu.AssignNewData(_targets);

        if (success)
        {
            if (_objFinal != null)
                StartCoroutine(Build());

            if (_isLeave)
            {
                if (--GameManager.inst.leaveLeft > 0)
                {
                    GameManager.inst.textLeave.text = string.Format("Leave ({0})", GameManager.inst.leaveLeft);
                    _UIMenu.NewDataValidationFade();
                }
                else Menu.inst.End();
            }
            else _UIMenu.NewDataValidationFade();
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (built) return;

        if (_obj != null && !built) _obj.SetActive(true);

        for (int i = 0; i < _targets.Length; i++)
        {
            int v = _targets[i].value;
            if (_targets[i].resources == Resources.food_prod)
                v *= UIMenu.humanCount;
            else if (_targets[i].resources == Resources.food_stock && _isLeave)
                v *= UIMenu.v_food_prod;

            SetValue(v, _targets[i].txt);
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (built) return;

        if (_obj != null && !built) _obj.SetActive(false);

        for (int i = 0; i < _targets.Length; i++)
        {
            Hide(_targets[i].txt);
        }
    }

    void Hide(TextMeshProUGUI txt)
    {
        txt.gameObject.SetActive(false);
    }

    void SetValue(int v, TextMeshProUGUI txt)
    {
        if (v == 0)
        {
            Hide(txt);
            return;
        }
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

    [System.Serializable]
    public struct Target
    {
        public Resources resources;
        public int value;
        public TextMeshProUGUI txt;

        public Target(Resources resources, int value, TextMeshProUGUI txt)
        {
            this.resources = resources;
            this.value = value;
            this.txt = txt;
        }
    }

    IEnumerator Build()
    {
        yield return new WaitForSeconds(0.6f);
        _objFinal.SetActive(true);
        built = true;
        GetComponent<Selectable>().interactable = false;
    }
}
