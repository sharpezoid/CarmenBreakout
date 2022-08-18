using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float MoveSpeed;

    public ObjectPooling BulletPool;
    float fireRate = 0.1f;
    float fireDuration;

    Rigidbody2D rb;

    SpriteRenderer sr;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleInput();

        fireDuration -= Time.fixedDeltaTime;
    }

    void HandleInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            MoveLeft();

        if (Input.GetKey(KeyCode.RightArrow))
            MoveRight();

        if (Input.GetKey(KeyCode.Space) && fireDuration <= 0.0f)
            Shoot();
    }


    public void MoveLeft()
    {
        Move(Vector2.left);
    }

    public void MoveRight()
    {
        Move(Vector2.right);
    }

    public void Move(Vector2 dir)
    {
        Vector2 move = dir * MoveSpeed * Time.fixedDeltaTime;
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, sr.size, 0, Vector2.right, move.magnitude, LayerMask.GetMask("Block", "Wall"));

        if (hit.transform != null)
        {
            transform.position = hit.centroid;
        }
        else
        {
            transform.Translate(move);
        }
    }

    void Shoot()
    {
        GameObject bulletFromPool = BulletPool.GetItem();
        if (bulletFromPool)
        {
            bulletFromPool.transform.position = transform.position;
            bulletFromPool.transform.rotation = Quaternion.identity;
        }

        fireDuration = fireRate;
    }
}
