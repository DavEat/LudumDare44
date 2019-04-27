using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    Animator _anim;
    [SerializeField] PlayerInteraction _playerInteraction = null;

    public enum AnimType { idle, move, attack, dash }

    private void Start()
    {
        _anim = GetComponent<Animator>();
        if (_playerInteraction == null)
            _playerInteraction = GetComponentInParent<PlayerInteraction>();
    }

    public void AttackEnd()
    {
        _playerInteraction.AnimEnd(AnimType.attack);
        _anim.SetTrigger("AttackEnd");
    }
}
