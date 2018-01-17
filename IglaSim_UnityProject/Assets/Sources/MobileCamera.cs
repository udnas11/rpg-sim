// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class MobileCamera : MonoBehaviour
{
    static public event Action OnCameraPlaying;

    #region public serialised vars
    [SerializeField]
    RenderTexture _renderTexture;
    #endregion


    #region private protected vars
    WebCamDevice _camDevice;
    WebCamTexture _camTexture;
    #endregion


    #region pub methods
    #endregion


    #region private protected methods
    IEnumerator InitFoV()
    {
        Vector2 fovRads = Vuforia.CameraDevice.Instance.GetCameraFieldOfViewRads();
        while (fovRads == Vector2.zero)
            yield return null;
        Debug.Log("fov: " + fovRads);
        Camera.main.fieldOfView = Mathf.Rad2Deg * fovRads.y;
        Debug.Log("fov v d: " + Camera.main.fieldOfView);
    }
    #endregion


    #region events
    #endregion


    #region mono events
    private void Start()
    {
        /*
        var devices = WebCamTexture.devices;
        if (devices.Length == 0)
        {
            Debug.Log("no webcamdevices");
            return;
        }

        foreach (var device in devices)
        {
            if (device.isFrontFacing == false)
            {
                _camDevice = device;
                break;
            }
        }

        _camTexture = new WebCamTexture(_camDevice.name);
        Debug.Log("webcamtexture available: " + _camTexture.name);
        if (_camTexture != null)
        {
            _camTexture.Play();
            if (OnCameraPlaying != null)
                OnCameraPlaying();
        }
        */
        StartCoroutine(InitFoV());
    }

    private void Update()
    {
        if (_camTexture != null)
            Graphics.Blit(_camTexture, _renderTexture);
    }
    #endregion
}
