using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    GameObject lastObjectHit;
    CircleCollider2D circleCollider;
    public ParticleSystem Fire;
    public ParticleSystem Explode;
    public ParticleSystem Hit;
    public Vector2 Velocity = new Vector2(4, 4);

    public GameObject batVis;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        batVis.transform.LookAt(transform.position + new Vector3(Velocity.x, Velocity.y, 0));
        batVis.transform.Rotate(0, 90, 0);
        
        transform.Translate(Velocity * Time.deltaTime);

        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, circleCollider.radius, Velocity, (Velocity * Time.deltaTime).magnitude);
        foreach(RaycastHit2D hit in hits)
        {
            if (hit.transform.gameObject.layer != LayerMask.NameToLayer("Ball") 
                && hit.collider != circleCollider 
                )
            {
                Instantiate(Hit, hit.point, Quaternion.identity);

                lastObjectHit = hit.transform.gameObject;

                Velocity = Vector2.Reflect(Velocity, hit.normal);

                if (hit.transform.GetComponent<Paddle>())
                {
                    Velocity.y = Mathf.Abs(Velocity.y);
                }

                if (hit.transform.GetComponent<Block>())
                {
                    hit.transform.GetComponent<Block>().OnHit();
                }
            }
        }

        if (transform.position.y < -Camera.main.orthographicSize)
        {
            Debug.Log("Ball Dead");
            Instantiate(Fire, transform.position, Quaternion.identity);
            Instantiate(Explode, transform.position, Quaternion.identity);
            FindObjectOfType<GameController>().BallLost(this);
           
        }
    }
}
