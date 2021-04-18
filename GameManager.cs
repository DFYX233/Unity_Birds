using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> Birds;
    public List<GameObject> Read;
    private List<GameObject> Pigs;
    private List<GameObject> deadBird = new List<GameObject>();

    //轨道布置
    [HideInInspector]
    public List<GameObject> traill_01 = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> traill_02 = new List<GameObject>();
    public List<Sprite> L_trail = new List<Sprite>();
    public bool traillOrder = false; //false使用轨迹1，true使用轨迹2

    [Header("胜负判定")]

    public float OneStarScore;
    public float TwoStarScore;
    public float ThereStarScore;

    private float roundSocer;
    public int isWin = -1;
    public static GameManager instance;
    public int index=0;
    private void Start()
    {
        if(instance!=null)
        {
            instance = null;
        }
        instance = this;
        InvokeRepeating("WinOrLoose",1,1);
        Birds[index].SetActive(true);
        Read[index].SetActive(false);
    }
    public void NextBird()
    {
        
        index++;
        if (index > Birds.Count)
        {
           index++;
            return;
        }
        Birds[index].SetActive(true);
        Read[index].SetActive(false);
        deadBird.Add(Read[index]);
    }
    public List<GameObject> GetPig()
    {
        List<GameObject> pig = new List<GameObject>();
        GameObject[] p = GameObject.FindGameObjectsWithTag("pig");
        foreach(GameObject item in p)
        {
            pig.Add(item);
        }
        return pig;
    }
    private void WinOrLoose()
    {
        Pigs = GetPig();
        if(index > Birds.Count||Pigs.Count==0)
        {
            EndCalulate();
        }
    }
    void EndCalulate()
    {
        if(roundSocer<OneStarScore)
            isWin = 0;
        else if(roundSocer>=OneStarScore&&roundSocer<TwoStarScore)
            isWin = 1;
        else if(roundSocer>=TwoStarScore&&roundSocer<ThereStarScore)
            isWin = 2;
        else
            isWin = 3;
    }
    void Update() 
    {
        if(isWin>-1)
        {
            EndUi.instance.EndCalulate(isWin);
            CamerController.instance.StopMove();
        }
    }
    void ReSetSocer()
    {
        roundSocer = 0;
    }
    public void Addscore(float s)
    {
        roundSocer += s;
    }
    public void ClearTheTrail(bool order)
    {
        if(order)
        {
            foreach(var item in traill_01)
            {
                Destroy(item);
            }
        }
        else
        {
            foreach(var item in traill_02)
            {
                Destroy(item);
            }
        }
    }

}
