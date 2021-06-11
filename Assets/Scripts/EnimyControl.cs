using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnimyControl : MonoBehaviour
{
    private int _currHp;
    private int _stepHp; // X (test)
    private int _maxHp;

    private float _radiusAttack; // K (test)
    private float _timeReload; // M (test)

    private float timer;
    private bool active;

    //private TextMeshPro hpText;


    void Start()
    {
        timer = 0.0f;
        active = true;
        //hpText = gameObject.GetComponent<TextMeshPro>();
    }

    void Update()
    {
        if (_currHp <= 0 && active == true)
        {
            active = false;
            gameObject.SetActive(false);
        }
    }

    public void MinusHpEnimy(Vector3 posBullet)
    {
        if (FindDirBullet(posBullet) <= _radiusAttack)
        {
            _currHp -= _stepHp;

            //string strHp = FindHp();
            //hpText.text = strHp;
        }
    }

    private string FindHp()
    {
        return "" +_currHp + "/" + _maxHp;
    }

    private float FindDirBullet(Vector3 posBullet)
    {
        var heading = transform.position - posBullet;
        return heading.magnitude;
    }


    public void InitEnimy(int maxHp, int stepHp, float radiusAttack, float timeReload, Vector3 startPos)
    {
        _maxHp = maxHp;
        _stepHp = stepHp;
        _radiusAttack = radiusAttack;
        _timeReload = timeReload;
        _currHp = _maxHp;

        SetPositionEnimy(startPos);
    }

    private void SetPositionEnimy(Vector3 startPos)
    {
        transform.position = startPos;
    }

    public Vector3 GetPositionEnimy()
    {
        return transform.position;
    }

    public void SetActive(bool newValue)
    {
        _currHp = _maxHp;
        active = newValue;
    }

    public bool GetActive()
    {
        return active;
    }

    public void ActiveEnimy()
    {
        gameObject.SetActive(true);
    }

    public void AddTimer(float addValue)
    {
        timer += addValue;
    }

    public float GetCurrTimer()
    {
        return timer;
    }

    public void ZerosTimer()
    {
        timer = 0.0f;
    }
}
