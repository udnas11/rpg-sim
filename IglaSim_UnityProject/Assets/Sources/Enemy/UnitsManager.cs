// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class UnitsManager : MonoSingleton<UnitsManager>
{
    #region public serialised vars
    #endregion


    #region private protected vars
    SpawnerHelicopter _spawnerHelicopter;
    List<UnitBase> _units = new List<UnitBase>();
    #endregion


    #region pub methods
    public List<UnitBase> GetActiveUnits()
    {
        return _units;
    }

    public void RegisterUnit(UnitBase unit)
    {
        Assert.IsNotNull(unit, "Registering null unit");
        Assert.IsFalse(_units.Contains(unit), "Unit already registered");
        _units.Add(unit);
    }

    public void UnregisterUnit(UnitBase unit)
    {
        Assert.IsNotNull(unit, "Un-registering null unit");
        Assert.IsTrue(_units.Contains(unit), "Unit not in registered pool");
        _units.Remove(unit);
    }
    #endregion


    #region private protected methods
    [ContextMenu("Spawn Heli")]
    void SpawnHelicopter()
    {
        var newHeli = _spawnerHelicopter.Spawn();
    }
    #endregion


    #region events
    #endregion


    #region mono events
    private void Awake()
    {
        _spawnerHelicopter = GetComponent<SpawnerHelicopter>();
    }
    #endregion
}
