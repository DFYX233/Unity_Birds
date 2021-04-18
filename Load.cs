using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    void Update() {
        if(Input.anyKey)
        {
            LoadNextScene();
        }
    }
    
    public void LoadNextScene()
    {
        int n = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(++n);
    }
}
