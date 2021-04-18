using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prop : MonoBehaviour
{
    public ParticleSystem ps;
    public SpriteRenderer sr;
    public static prop instance;
    public Collider2D collider;
    [Header("交互参数")]
    public Sprite inHite;
    public float hiteSpeed;
    public float deadSpeed;

    private void Start()
    {
        if(instance!=null)
        {
            instance = null;
        }
        instance = this;
        ps = GetComponentInChildren<ParticleSystem>();
        sr = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
    }
    public void Brust()
    {
        if (ps != null)
        {
            sr.enabled = false;
            ps.Play();
            collider.enabled = false;
            Invoke("DestroySelf", 2f);
        }
        else
        {
            DestroySelf();
        }
    }
    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
    void OnCollisionEnter2D(Collision2D other) 
    {
        float otherSpeedX = other.gameObject.GetComponent<Rigidbody2D>().velocity.x;
        float otherSpeedY = other.gameObject.GetComponent<Rigidbody2D>().velocity.y;
        float speed = Mathf.Sqrt(otherSpeedX*otherSpeedX+otherSpeedY*otherSpeedY);
        if(speed>deadSpeed)
        {
            Invoke("Brust",0.1f);
        }
        else if(speed>hiteSpeed)
        {
            sr.sprite = inHite;
        }
    }


}
