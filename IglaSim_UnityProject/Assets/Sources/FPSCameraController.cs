// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class FPSCameraController : MonoSingleton<FPSCameraController>
{
    #region public serialised vars
    public event Action<Camera> OnPreRenderCallback;
    #endregion


    #region private protected vars
    Camera _camera;
    #endregion


    #region pub methods
    #endregion


    #region private protected methods
    #endregion


    #region events
    #endregion


    #region mono events
    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void OnPreRender()
    {
        if (OnPreRenderCallback != null)
            OnPreRenderCallback(_camera);
    }
    #endregion
}
