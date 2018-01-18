﻿// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class RpgScreenController : MonoBehaviour
{
    #region public serialised vars
    [SerializeField]
    RenderTexture _screenTexture;
    [SerializeField]
    RenderTexture _heatVisionTexture;
    [SerializeField]
    Shader _heatShader;
    [SerializeField]
    Texture2D _heatTexture;
    #endregion


    #region private protected vars
    Camera _arCamera;
    Material _heatMaterial;
    List<UnitBase> _enemies;
    Vector4[] _enemiesScreenspace = new Vector4[16];
    #endregion


    #region pub methods
    #endregion


    #region private protected methods
    void ApplyHeatEffect(Camera camera)
    {
        if (!enabled)
            return;
        
        for (int i = 0; i < _enemies.Count; i++)
        {
            _enemiesScreenspace[i] = _arCamera.WorldToViewportPoint(_enemies[i].transform.position);
            //_enemiesScreenspace[i].z *= UnityRandom.Range(0.5f, 2f);
        }

        _heatMaterial.SetVectorArray("_Enemies", _enemiesScreenspace);
        _heatMaterial.SetInt("_EnemiesCount", _enemies.Count);        

        Graphics.Blit(_screenTexture, _heatVisionTexture, _heatMaterial);
    }
    #endregion


    #region events
    #endregion


    #region mono events
    private void Awake()
    {
        _heatMaterial = new Material(_heatShader);
        _heatMaterial.SetTexture("_HeatColors", _heatTexture);
        FPSCameraController.Instance.OnPreRenderCallback += ApplyHeatEffect;
        _arCamera = FindObjectOfType<CameraToRenderTexture>().GetComponent<Camera>();
        _enemies = UnitsManager.Instance.GetActiveUnits();
    }

    private void Start() {}

    private void OnDestroy()
    {
        FPSCameraController.Instance.OnPreRenderCallback -= ApplyHeatEffect;
    }
    #endregion
}
