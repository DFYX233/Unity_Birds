using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyFire : MonoBehaviour
{
    public GameObject black;
    public float fireForce;
    private Transform pos;
    void Start() {
        pos=  GetComponent<Transform>();
    }

    void Update() {
        if(Input.GetMouseButtonDown(0))
        {
               Vector3 target =  GetPos();
               Vector3 target_path = calulate(target);
                Fire(target_path);
        }
    }
    void OnMouseDown() {

    }
    Vector3 GetPos()
    {
            Vector3 target  = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            print(target.ToString());
            return target;

    }
    Vector3 calulate(Vector3 target)
    {
        Vector3 path = (target - this.transform.position).normalized;
        return path;
    }
    void Fire(Vector3 target)
    {
        GameObject bird= Instantiate(black,this.transform.position,Quaternion.identity);
        bird.GetComponent<Rigidbody2D>().AddForce(target*fireForce,ForceMode2D.Impulse);
    }
}
