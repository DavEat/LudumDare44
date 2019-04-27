using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camp : MonoBehaviour
{
    [HideInInspector] public Transform _transform;

    bool _spendingEnergy;

    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        if ((GameManager.inst.player._transform.position - _transform.position).sqrMagnitude > 25)
        {
            CanvasManager.inst.EnableCsInfo(false);
            return; // player out of range
        }


        

        if (!_spendingEnergy)
        {
            CanvasManager.inst.EnableCsInfo(true);

            if (Input.GetButtonDown("Submit"))
            {
                _spendingEnergy = true;
                CanvasManager.inst.EnableCsInfo(false);
                CameraManager.inst.SetTransitionToCamp(ShowSpendEnergy);

                GameManager.inst.player.canMove = false;
            }
        }
        else
        {
            if (Input.GetButtonDown("Cancel"))
            {
                _spendingEnergy = false;
                CanvasManager.inst.EnableCsInfo(true);
                CanvasManager.inst.EnableCsSpendEnergy(false);

                GameManager.inst.player.canMove = true;

                CameraManager.inst.SetTransitionToPlayer();
            }
        }
    }

    private void ShowSpendEnergy()
    {
        CanvasManager.inst.EnableCsSpendEnergy(true);
    }
}
