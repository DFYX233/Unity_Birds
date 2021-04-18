using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBlack : MonoBehaviour
{
        private Rigidbody2D rb;
    private bool ClipPlay;
    public bool isClike;
    public bool isFire;
    public GameObject point;
    private Vector3 mousePos;
    private SpriteRenderer sr;
    public Sprite spriteState01;
    public Sprite spriteState02;
    public Sprite spriteState03;

    [Header("技能参数")]
    public float scoup;
    public float effectScoupe;
    private ParticleSystem ps;
    public float brustForce;
    public bool readBoom;
    private void Start()
    {
        ClipPlay = false;
        readBoom = true;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = spriteState01;
        ps = GetComponentInChildren<ParticleSystem>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
            Invoke("ReadExploed", 1f);


    }
    public void ReadExploed()
    {
        if(readBoom)
        {
            sr.sprite = spriteState02;
            Invoke("Exploeding", 0.5f);
            readBoom = false;
        }

    }

    public void Exploeding()
    {
        sr.sprite = spriteState03;
        Invoke("Exploed", 0.5f);
    }
    public void Exploed()
    {
        AudioController.instance.PlayBrust();
        ps.Play();
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, effectScoupe);
        foreach (var item in collider)
        {
            if(item==null)
            continue;
            Pig pig = item.gameObject.GetComponent<Pig>();
            prop p = item.gameObject.GetComponent<prop>();
            if (item.gameObject.CompareTag("prop") && p != null)
            {
                if (Vector2.Distance(this.gameObject.transform.position, item.transform.position) < scoup)
                {
                    p.Brust();
                }
                else if (Vector2.Distance(this.transform.position, item.transform.position) < effectScoupe)
                {
                    Vector3 direct = (item.transform.position - this.transform.position).normalized;
                    Rigidbody2D itemRb = item.GetComponent<Rigidbody2D>();
                    itemRb.AddForce(direct * brustForce, ForceMode2D.Impulse);
                }
            }
            if (item.gameObject.CompareTag("pig") && pig != null)
            {
                if (Vector2.Distance(this.gameObject.transform.position, item.transform.position) < scoup)
                {
                    pig.DestroySelf();
                }
                else if (Vector2.Distance(this.transform.position, item.transform.position) < effectScoupe)
                {
                    Vector3 direct = (item.transform.position - this.transform.position).normalized;
                    Rigidbody2D itemRb = item.GetComponent<Rigidbody2D>();
                    itemRb.AddForce(direct * brustForce, ForceMode2D.Impulse);
                }

            }
        }
        sr.enabled = false;
        Invoke("Destroy", 1f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, scoup);
        Gizmos.DrawWireSphere(gameObject.transform.position, effectScoupe);
    }
    private void Destroy()
    {
        CamerController.instance.ReLoad();
        GameManager.instance.NextBird();
        Destroy(this.gameObject);
    }
}

