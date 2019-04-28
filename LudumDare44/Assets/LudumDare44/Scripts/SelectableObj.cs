using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectableObj : MonoBehaviour
{
    [HideInInspector] public Transform _transform;

    [SerializeField] UnityEvent _onClick;

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
}
