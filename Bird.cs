using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    //种类选择
    public enum Kinds
    {
        Red,
        Black,
        White,
        Yellow,
        other
    }
    public Kinds what;
    [Header("公有参数")]
    [Range(1, 200)]
    public float Flyforce;
    public float maxDistence;
    public GameObject SlingShoot_left;
    public GameObject SlingShoot_right;
    public GameObject point;
    public Sprite sprite;

    [Header("状态监视")]
    public bool needDraw;
    public bool onSlingShoot;
    public bool isClike;
    public bool isFire;
    public bool OnGround;
    private bool ClipPlay;
    [Header("白鸟技能")]
    public float SForce;
    public GameObject boom;
    private Vector3 dropPlace;
    public bool boomIsRead;
    [Header("黑鸟技能")]
    public Sprite spriteState01;
    public Sprite spriteState02;
    public Sprite spriteState03;
    public float scoup;
    public float effectScoupe;
    private ParticleSystem ps;
    public float brustForce;
    public bool readBoom;
    [Header("黄鸟技能")]
    public float force;
    public bool skillRead = true;

    //私有组件    
    private TrailRenderer tr;
    private Rigidbody2D rb;
    private Vector3 mousePos;
    private SpringJoint2D sj;
    private SpriteRenderer sr;
    private LineRenderer lr_left;
    private LineRenderer lr_right;

    //轨道布置
    private GameObject lastTrailPoint;
    private Queue<Sprite> trail =new Queue<Sprite>();

    private float distence = 1f;
    void Start()
    {
        //公有组件获取
        sj = GetComponent<SpringJoint2D>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        lr_left = SlingShoot_left.GetComponent<LineRenderer>();
        lr_right = SlingShoot_right.GetComponent<LineRenderer>();
        foreach(Sprite item in GameManager.instance.L_trail)
        {
            trail.Enqueue(item);
        }

        //个性化组件获取
        switch (what)
        {
            case Kinds.Red: break;
            case Kinds.Black:
                ClipPlay = false;
                readBoom = true;
                onSlingShoot = true;
                ps = GetComponentInChildren<ParticleSystem>(); break;
            case Kinds.White: break;
            case Kinds.Yellow: break;
            case Kinds.other: break;
            default: break;
        }

        //公有状态设置
        needDraw = false;
        onSlingShoot = true;
        Debug.Log("初始化完成");
    }
    void Update()
    {
        ClikCheck();
        SetLine();

        if(what == Kinds.White)
        Drop();
        if(needDraw)
        Draw();
    }
    void OnMouseDown()
    {
        rb.freezeRotation = false;
        rb.isKinematic = true;
        isClike = true;
    }
    private void OnMouseUp()
    {
        needDraw = true;
        rb.isKinematic = false;
        isClike = false;
        isFire = true;
        onSlingShoot = false;


        //个性化参数

        boomIsRead = true;
        //起飞音效
        switch(what)
        {
            case Kinds.Red:AudioController.instance.PlayRedFly();break;
            case Kinds.White:AudioController.instance.PlayWhiteFly();break;
            case Kinds.Black:AudioController.instance.PlayBlackFly();break;
            case Kinds.Yellow:AudioController.instance.PlayYellowFly();break;
            default:AudioController.instance.PlayRedFly();break;
        }
        FlyMode.instance.Fly(this.gameObject.transform, Flyforce);
    }
    private void ClikCheck()
    {
        if (isFire)
        {
            sj.enabled = false;
            rb.freezeRotation = false;
        }
        if (isClike)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 pos = new Vector3(mousePos.x, mousePos.y, 0);
            float dis = Vector3.Distance(pos, point.transform.position);
            if (dis < maxDistence)
            {
                this.transform.position = pos;
            }
            else
            {
                Vector3 BirdPos = new Vector3(transform.position.x, transform.position.y, 0);
                Vector3 PointPos = new Vector3(point.transform.position.x,point.transform.position.y, 0);
                Vector3 direct = (pos - PointPos).normalized;

                direct = new Vector3(direct.x*maxDistence,direct.y*maxDistence,0);
                
                transform.position = PointPos + direct;
                
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("pig") || collision.gameObject.CompareTag("land") || collision.gameObject.CompareTag("prop"))
        {
            needDraw = false;
            rb.freezeRotation = false;
            OnGround = true;
            switch(what)
            {
                case Kinds.Black:Invoke("ReadExploed", 1f);break;
                case Kinds.Red:sr.sprite = sprite;Invoke("Destroy", 2f);break;
                case Kinds.White:sr.sprite = sprite;Invoke("Destroy", 2f);break;
                case Kinds.Yellow:sr.sprite = sprite;Invoke("Destroy", 2f);break;
                default:break;
            }
            
        }
    }
    #region 弹弓划线
    private void SetLine()
    {
        if(onSlingShoot)
        {
            lr_right.enabled = true;
            lr_left.enabled = true;

            lr_left.SetPosition(0, transform.position);
            lr_left.SetPosition(1, transform.TransformVector(SlingShoot_left.transform.position));
            lr_right.SetPosition(0, transform.position);
            lr_right.SetPosition(1, transform.TransformVector(SlingShoot_right.transform.position));
        }
        else
        {
            lr_right.enabled = false;
            lr_left.enabled = false;
        }

    }
    #endregion

    private void Destroy()
    {
        CamerController.instance.ReLoad();
        GameManager.instance.NextBird();
        Destroy(this.gameObject);
    }
    
    private void CameraContron()
    {
        if(what==Kinds.White)
        {
             if (isFire && transform.position.x > CamerController.instance.starX)
            {
                  if(!boomIsRead)
                  {
                        CamerController.instance.SetFlyPlace(dropPlace);
                        return;         
                   }
                   CamerController.instance.SetFlyPlace(this.transform.position);
            }
            return;
        }
        if (isFire && transform.position.x > CamerController.instance.starX)
        {
            CamerController.instance.SetFlyPlace(this.transform.position);
        }
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
     public void Drop()
    {
        if(isFire&&!OnGround&&boomIsRead)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Instantiate(boom, transform.position, Quaternion.identity);
                boomIsRead = false;
                rb.AddForce(new Vector2(5, 5) * SForce, ForceMode2D.Impulse);
                dropPlace = this.transform.position;
            }
        }
    }
    private void Draw()
    {
        if(lastTrailPoint!=null)
        {
            if(Vector3.Distance(transform.position,lastTrailPoint.transform.position)<distence)
                return;
        }
        Sprite s = trail.Dequeue();
        GameObject t = new GameObject();
        t.AddComponent<SpriteRenderer>();
        t.GetComponent<SpriteRenderer>().sprite = s;
        t.GetComponent<SpriteRenderer>().sortingLayerName = "font";
        t.transform.position = transform.position;
        lastTrailPoint = t;
        trail.Enqueue(s);
        if(!GameManager.instance.traillOrder)
        {
            GameManager.instance.traill_01.Add(t);
        }
        else
        {
            GameManager.instance.traill_02.Add(t);
        }
    }
    void OnDestroy()
    {
        GameManager.instance.traillOrder = !GameManager.instance.traillOrder;

        if(GameManager.instance.traill_01.Count!=0&&GameManager.instance.traillOrder==false)
        {
            GameManager.instance.ClearTheTrail(true);
        }
        if(GameManager.instance.traill_02.Count!=0&&GameManager.instance.traillOrder==true)
        {
            GameManager.instance.ClearTheTrail(false);
        }

    }
}
