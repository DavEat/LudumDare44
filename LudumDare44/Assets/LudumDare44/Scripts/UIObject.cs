using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Target[] _targets;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        for (int i = 0; i < _targets.Length; i++)
        {
            SetValue(_targets[i].value, _targets[i].txt);
        }
    }

    private void OnMouseEnter()
    {
        for (int i = 0; i < _targets.Length; i++)
        {
            SetValue(_targets[i].value, _targets[i].txt);
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        for (int i = 0; i < _targets.Length; i++)
        {
            Hide(_targets[i].txt);
        }
    }

    private void OnMouseExit()
    {
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
    struct Target
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
}
