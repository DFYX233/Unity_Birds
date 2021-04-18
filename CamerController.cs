using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerController : MonoBehaviour
{
    public static bool ableToLeft = true;
    public static bool ableToRight = true;
    public float endBorder;
    public bool ableToMove;
    public Vector3 origin;
    public static Transform bird;
    public static CamerController instance;
    public Transform slingShoot;
    public float starX;
    private void Start()
    {
        if (instance != null)
        {
            instance = null;
        }
        instance = this;

        ableToMove = true;
        origin = this.GetComponent<Transform>().position;
    }
    private void Update()
    {

    }
    void FixedUpdate() {
        // if(this.transform.position.x>endBorder)
        // {
        //     ableToMove = false;
        //     //transform.position  = new Vector3(endBorder,transform.position.y,transform.position.z);
        //     //transform.position+=new Vector3(-1,0,0);
        //     ableToRight = false;
        // }
        // if(this.transform.position.x<origin.x)
        // {
        //     ableToMove = false;
        //     //transform.position  = new Vector3(origin.x,transform.position.y,transform.position.z);
        //     //transform.position+=new Vector3(1,0,0);
        //     ableToLeft = false;

        // }
        // if(this.transform.position.x<endBorder||this.transform.position.x>origin.x||GameManager.instance.isWin<0)
        // {
        //     ableToMove = true;
        // }
        
        transform.position = new Vector3(Mathf.Clamp(transform.position.x,origin.x,endBorder),transform.position.y,
        transform.position.z);
    }

    public void SetFlyPlace(Vector3 FlyPlace)
    {
        if(ableToMove)
        {
            if (this.transform.position.x >= 68)
                return;
            Vector3 offset = new Vector3(FlyPlace.x,transform.position.y,transform.position.z);
            if(offset.x>origin.x)
            {
                this.transform.position = new Vector3(FlyPlace.x, transform.position.y,
                transform.position.z);
            }
        }

    }
    public void ReLoad()
    {
        for(float n = 0;n<1;n+=Time.deltaTime)
        {
            transform.position = new Vector3(Mathf.Lerp(transform.position.x,origin.x,0.5f),
            transform.position.y,transform.position.z);
        }
    }
  
    public void StopMove()
    {
        ableToMove = false;
    }

    public void LeftOrRight(bool left)
    {
        if(ableToMove)
        {
            if(left&&ableToLeft)
            {
                transform.transform.position += new Vector3(-0.5f,0,0);
            }
            else if(!left&&ableToRight)
            {
                transform.position+= new Vector3(0.5f,0,0);
            }
        }
    }
}
