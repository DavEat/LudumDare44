using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectableObj : MonoBehaviour
{
    [HideInInspector] public Transform _transform;

    public Transform head;

    public string sName;
    public bool robot = true;
    [SerializeField] UnityEvent _onClick = null;

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    public void Select()
    {
        _onClick.Invoke();
    }

    public void Diselect()
    {

    }

    public void Removed(float time = .6f)
    {
        Destroy(gameObject, time);
    }
}
