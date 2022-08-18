using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : PooledObject
{
    public float MoveSpeed;

    private void Update()
    {
        Vector3 vel = Vector3.up * MoveSpeed * Time.deltaTime;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, vel, vel.magnitude, LayerMask.GetMask("Block", "Wall"));

        if (hit.transform != null)
        {
            Block block = hit.transform.GetComponent<Block>();
            if (block != null)
            {
                block.OnHit();
            }



            //Destroy(gameObject);
        }

        transform.Translate(vel);
    }
}
