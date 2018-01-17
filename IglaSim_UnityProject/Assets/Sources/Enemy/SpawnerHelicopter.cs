// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class SpawnerHelicopter : SpawnerBase
{
    #region public serialised vars
    [SerializeField]
    UnitHelicopter _heliPrefab;
    [SerializeField]
    float _spawnRadius;
    [SerializeField]
    Vector2 _spawnAltitudeRange;
    #endregion


    #region private protected vars
    #endregion


    #region pub methods
    #endregion


    #region private protected methods
    public override UnitBase Spawn()
    {
        UnitHelicopter newInst = Instantiate(_heliPrefab) as UnitHelicopter;
        if (newInst != null)
        {
            float direction = UnityRandom.Range(0f, Mathf.PI * 2);
            float altitude = UnityRandom.Range(_spawnAltitudeRange.x, _spawnAltitudeRange.y);
            Vector3 position = new Vector3(Mathf.Sin(direction) * _spawnRadius, altitude, Mathf.Cos(direction) * _spawnRadius);
            newInst.transform.SetParent(this.transform);
            newInst.transform.position = position;
        }
        return newInst;
    }
    #endregion


    #region events
    #endregion


    #region mono events
    private void OnDrawGizmosSelected()
    {
        for (float i = 0f; i < Mathf.PI * 2; i += 0.1f)
        {
            Gizmos.DrawLine(new Vector3(Mathf.Sin(i) * _spawnRadius, _spawnAltitudeRange.x, Mathf.Cos(i) * _spawnRadius),
                new Vector3(Mathf.Sin(i + 0.3f) * _spawnRadius, _spawnAltitudeRange.y, Mathf.Cos(i + 0.3f) * _spawnRadius));
        }
    }
    #endregion
}
