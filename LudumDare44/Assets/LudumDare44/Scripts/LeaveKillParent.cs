using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveKillParent : MonoBehaviour
{
    [SerializeField] RectTransform _rect = null;
    public bool robot;
    [HideInInspector] public SelectableObj so  =null;
    public UIMenu _UIMenu;

    public void SetPosition(Vector2 position, bool robot, SelectableObj so)
    {
        _rect.anchoredPosition = position;
        this.robot = robot;
        this.so = so;
    }
}
