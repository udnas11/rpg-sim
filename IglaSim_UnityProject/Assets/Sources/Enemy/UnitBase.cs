// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class UnitBase : MonoBehaviour
{

    public event Action<UnitBase> OnDeathAction;

	#region public serialised vars
    [SerializeField]
    int _maxHealth = 1;
    #endregion


    #region private protected vars
    int _health;
    bool _dying;
    #endregion


    #region pub methods
    public virtual void DealDamage()
    {
        if (--_health <= 0)
            Die();
    }
    #endregion


    #region private protected methods
    protected virtual void Die()
    {
        if (_dying)
            return;

        _dying = true;
        Destroy(this.gameObject, 0.5f);
        if (OnDeathAction != null)
            OnDeathAction(this);
    }
    #endregion


    #region events
    #endregion


    #region mono events
    protected virtual void Awake()
    {
        _health = _maxHealth;
        UnitsManager.Instance.RegisterUnit(this);
    }

    private void OnDestroy()
    {
        var unitManager = UnitsManager.Instance;
        if (unitManager)
            unitManager.UnregisterUnit(this);
    }
    #endregion
}