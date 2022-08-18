using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpGrow : PowerUp
{
    public override void OnCollect()
    {
        Debug.Log("Grow Collected");


        base.OnCollect();
    }
}
