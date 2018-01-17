// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EasyRotatorUpdate : MonoBehaviour
{
    #region public serialised vars
    [SerializeField]
    Vector3 _axis;
    [SerializeField]
    Space _space;
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
    private void Update()
    {
        this.transform.Rotate(_axis * Time.deltaTime, _space);
    }
    #endregion
}
