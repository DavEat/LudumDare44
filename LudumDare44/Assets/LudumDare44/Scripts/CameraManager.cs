using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager inst;
    private void Awake() { inst = this; }

    Transform _transform = null;
    Camera _camera = null;

    Quaternion _baseRotation = Quaternion.identity;
    float _baseCameraSize = 0;

    [SerializeField] int _goingToCampsiteView = 0; // -2 followplayer, -1 camera, 0 nothing, 1 campsite

    [SerializeField] float _transitionSpeed = 1;
    [SerializeField] Vector3 _playerOffSet = Vector3.zero;

    [SerializeField] float _csCameraSize = 0;
    [SerializeField] Transform _csCamera = null;

    Action _callback;

    void Start()
    {
        _transform = GetComponent<Transform>();
        _camera = GetComponent<Camera>();

        _baseRotation = _transform.rotation;
        _baseCameraSize = _camera.orthographicSize;
    }

    void Update()
    {
        if (_goingToCampsiteView == 1)
            CampsiteTransition();
        else if (_goingToCampsiteView < 0)
            PlayerTransition();
    }

    void CampsiteTransition()
    {
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _csCameraSize, Time.deltaTime * _transitionSpeed);
        _transform.position = Vector3.Slerp(_transform.position, _csCamera.position, Time.deltaTime * _transitionSpeed);
        _transform.rotation = Quaternion.Slerp(_transform.rotation, _csCamera.rotation, Time.deltaTime * _transitionSpeed);

        if (Mathf.Abs(_camera.orthographicSize - _csCameraSize) < .1f)
        {
            _goingToCampsiteView = 0;
            _callback.Invoke();
        }
    }
    void PlayerTransition()
    {
        if (_goingToCampsiteView == -1)
        {
            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _baseCameraSize, Time.deltaTime * _transitionSpeed);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, _baseRotation, Time.deltaTime * _transitionSpeed);

            if (Mathf.Abs(_camera.orthographicSize - _baseCameraSize) < .1f)
                _goingToCampsiteView = -2;
        }

        _transform.position = Vector3.Slerp(_transform.position, GameManager.inst.player._transform.position + _playerOffSet, Time.deltaTime * _transitionSpeed);
    }

    public void SetTransitionToCamp(Action callback)
    {
        _callback = callback;
        _goingToCampsiteView = 1;
    }

    public void SetTransitionToPlayer()
    {
        _goingToCampsiteView = -1;
    }
}