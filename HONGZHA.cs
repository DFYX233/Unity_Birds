using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HONGZHA : MonoBehaviour
{
    public GameObject boom;

    void Update() {
        
    }
    void Start() {
        StartCoroutine(Drop());
    }
    IEnumerator Drop()
    {
        for(int i =0;i<50;i++)
        {
        Instantiate(boom,transform.position,Quaternion.identity);
        yield return new WaitForSeconds(1f);

        }
    }
}
