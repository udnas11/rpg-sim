// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class CameraToRenderTexture : MonoBehaviour
{
    #region public serialised vars
    [SerializeField]
    RenderTexture _outputTexture;
    #endregion


    #region private protected vars
    #endregion


    #region pub methods
    #endregion


    #region private protected methods
    #endregion


    #region events
    #endregion


    #region mono events
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (enabled == false)
            return;

        Graphics.Blit(src, _outputTexture);
        Graphics.Blit(src, dest);
    }
    #endregion
}
