using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMode : MonoBehaviour
{
    public Transform slingshoot;
    public static FlyMode instance;
    void Awake() {
        instance = this;
    }
    public void Fly(Transform Bird,float force)
        {
            float dis = Vector3.Distance(Bird.position,slingshoot.position);
            Vector2 dic = (slingshoot.position-Bird.position).normalized;
            Rigidbody2D rb = Bird.GetComponent<Rigidbody2D>();
            rb.AddForce(dis*dic*force,ForceMode2D.Impulse);
        }
}
