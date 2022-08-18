using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    float MoveSpeed = 2;

    private void Update()
    {
        transform.Translate(Vector3.up * MoveSpeed * Time.deltaTime);
    }
}
