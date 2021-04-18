using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    [Header("分数")]
    public float score;
    [Header("状态监测")]
    public bool isAilve;
    [Header("公有属性")]
    public Sprite inHint;
    public float hintSpeed;
    public float deadSpeed;
    public Sprite point;

    public Sprite eye;

    //私有属性
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private ParticleSystem ps;
    private CircleCollider2D cd;
    private bool isShowed = false;
    private void Start()
    {
        cd = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        ps = GetComponentInChildren<ParticleSystem>();

        StartCoroutine(CloseEye());
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("player")||collision.gameObject.CompareTag("prop"))
        {
            float x =  collision.gameObject.GetComponent<Rigidbody2D>().velocity.x;
            float y =  collision.gameObject.GetComponent<Rigidbody2D>().velocity.y;
            float speed = Mathf.Sqrt(x*x+y*y);

            if(speed>deadSpeed)
            {
                //直接死亡
                cd.enabled = false;
                sr.enabled = false;
                Destroy(rb);
                ShowSocer();
                isShowed = true;
                ps.Play();
                Invoke("Die", 1f);
            }
            else if(speed>hintSpeed)
            {
                //受伤
                sr.sprite = inHint;
            }

        }
    }
    public void Die()
    {
        sr.enabled = false;
        Destroy(rb);
        Invoke("DestroySelf", 0.5f);
    }
    public void DestroySelf()
    {
        if(!isShowed)
             ShowSocer();
        Destroy(this.gameObject);
    }
    private void ShowSocer()
    {
        //显示数字
        GameObject score = new GameObject();
        score.AddComponent<SpriteRenderer>();
        score.GetComponent<SpriteRenderer>().sprite = point;
        score.GetComponent<SpriteRenderer>().sortingLayerName = "font";
        score.transform.position = new Vector3(this.transform.position.x,this.transform.position.y+1,
        this.transform.position.z);
        Destroy(score,1f);
    }
    IEnumerator CloseEye()
    {
        int i = 0;
        while(true)
        {
            i++;
            if(i%2!=0)
                yield return new WaitForSeconds(5f);
            Sprite temp = sr.sprite;
            sr.sprite = eye;
            eye = temp;
            yield return new WaitForSeconds(0.5f);
        }

    }
}

