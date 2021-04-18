using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndUi : MonoBehaviour
{
    public Image Fall;
    public GameObject Winlight;
    public Canvas BackGround;
    public Canvas StarGround;
    public Image star1;
    public Image star2;
    public Image star3;
    public static EndUi instance;

    void Start() {
             if(instance!=null)
            instance=null;
        instance=this;
    }
    public void EndCalulate(int status)
    {
        Invoke("ShowC1",2f);
        switch(status)
        {
            case 0:Invoke("ShowFall",3f);break;
            case 1:Invoke("ShowOne",3f);Invoke("PlayWin",4f);break;
            case 2:Invoke("ShowOne",3f);Invoke("ShowTwo",4f);Invoke("PlayWin",5f);break;
            case 3:Invoke("ShowOne",3f);Invoke("ShowTwo",4f);Invoke("ShowThree",6f);Invoke("PlayWin",7f);break;
            default:break;
        }
    }
    private void ShowFall()
    {
        Fall.gameObject.SetActive(true);
    }
    private void ShowOne()
    {
        star1.gameObject.SetActive(true);
        AudioController.instance.PlayStar1();
    }
    private void ShowTwo()
    {
        AudioController.instance.PlayStar2();
        star2.gameObject.SetActive(true);
    }
    private void ShowThree()
    {
        AudioController.instance.PlayStar3();
        star3.gameObject.SetActive(true);
        Winlight.gameObject.SetActive(true);
    }
    private void ShowC1()
    {
        BackGround.gameObject.SetActive(true);
    }
    private void PlayWin()
    {
        AudioController.instance.PlayWin();
    }

}
