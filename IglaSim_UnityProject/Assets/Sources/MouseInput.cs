// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MouseInput : MonoBehaviour
{
    #region public serialised vars
    [SerializeField]
    float _sensitivity;
    #endregion


    #region private protected vars
    #endregion


    #region pub methods
    #endregion


    #region private protected methods
    Vector2 _lastMousePos;
    Vector3 _rotation;

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(1))
            _lastMousePos = Input.mousePosition;
        else if (Input.GetMouseButton(1))
        {
            Vector2 delta = _lastMousePos - (Vector2)Input.mousePosition;
            _rotation.x += delta.y * _sensitivity;
            _rotation.x = Mathf.Clamp(_rotation.x, -80f, 80f);
            _rotation.y += -delta.x * _sensitivity;
            this.transform.rotation = Quaternion.Euler(_rotation);
            _lastMousePos = Input.mousePosition;
        }
    }
    #endregion


    #region events
    #endregion


    #region mono events
    private void Awake()
    {
#if !UNITY_EDITOR
        this.enabled = false;
#endif
    }

    private void Update()
    {
        HandleInput();
    }
#endregion
}
