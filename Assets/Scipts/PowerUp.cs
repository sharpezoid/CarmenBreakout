using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float MoveSpeed = 5;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * MoveSpeed);       
    }

    public virtual void OnCollect()
    {
        Debug.Log("Base Called");
    }
}
