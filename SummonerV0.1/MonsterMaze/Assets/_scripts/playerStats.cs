using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class playerStats
{
    public static GameObject player;
    [SerializeField]
    private playerHealthBar bar;
    [SerializeField]
    private float maxVal;
    [SerializeField]
    public float currentVal;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player").gameObject;
    }

    public void Initialize()
    {
        //Initializing HP Values
        this.MaxVal = maxVal;
        this.CurrentVal = currentVal;

    }

    public float CurrentVal
    {
        get
        {
            return currentVal;
        }
        set
        {
            this.currentVal = Mathf.Clamp(value, 0, MaxVal);//Limits the HP value to 0
            bar.Value = currentVal;
        }
    }

    public float MaxVal
    {
        get
        {
            return maxVal;
        }
        set
        {
            this.maxVal = value;
            bar.MaxValue = maxVal;
        }
    }
}