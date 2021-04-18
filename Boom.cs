using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public float scoup;
    public float starSpeed;
    public float effectScoup;
    public float brustForce;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private ParticleSystem ps;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("pig")||collision.gameObject.CompareTag("land")||collision.gameObject.CompareTag("prop"))
        {
            AudioController.instance.PlayBrust();
            sr.enabled = false;
            GetComponent<BoxCollider2D>().isTrigger = true;
            ps.Play();
            Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, effectScoup);
            foreach (var item in collider)
            {
                Pig pig = item.gameObject.GetComponent<Pig>();
                prop p = item.gameObject.GetComponent<prop>();
                if (item.gameObject.CompareTag("prop") && p != null)
                {
                    if (Vector2.Distance(this.gameObject.transform.position, item.transform.position) < scoup)
                    {
                        p.Brust();
                    }
                    else if (Vector2.Distance(this.transform.position, item.transform.position) < effectScoup)
                    {
                        Vector3 direct = (item.transform.position - this.transform.position).normalized;
                        Rigidbody2D itemRb = item.GetComponent<Rigidbody2D>();
                        itemRb.AddForce(direct * brustForce, ForceMode2D.Impulse);
                    }

                }
                if(item.gameObject.CompareTag("pig")&&pig!=null)
                {
                    if (Vector2.Distance(this.gameObject.transform.position, item.transform.position) < scoup)
                    {
                        pig.DestroySelf();
                    }
                    else if (Vector2.Distance(this.transform.position, item.transform.position) < effectScoup)
                    {
                        Vector3 direct = (item.transform.position - this.transform.position).normalized;
                        Rigidbody2D itemRb = item.GetComponent<Rigidbody2D>();
                        itemRb.AddForce(direct * brustForce, ForceMode2D.Impulse);
                    }

                }
            }
        }
        Destroy(this.gameObject,2f);
    }
    private void Update()
    {
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, scoup);
        Gizmos.DrawWireSphere(gameObject.transform.position, effectScoup);
    }
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, starSpeed);
        ps = GetComponentInChildren<ParticleSystem>();
    }
}
