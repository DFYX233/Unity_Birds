using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamreMove : MonoBehaviour
{
    public bool isEnter;
    public bool left;
    private BoxCollider2D bc;

    void Start() {
        bc = GetComponent<BoxCollider2D>();
    }
    void OnMouseEnter() {
        if(Input.GetMouseButton(0))
            return;
        isEnter = true;
    }
    void OnMouseExit() {
        CamerController.ableToRight = true;
        CamerController.ableToLeft = true;
        isEnter = false;
    }
    void Update() {
        if(isEnter)
        CamerController.instance.LeftOrRight(left);
    }


}
