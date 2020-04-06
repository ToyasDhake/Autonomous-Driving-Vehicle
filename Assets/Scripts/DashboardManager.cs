using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DashboardManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void exitApplication()
    {
        Application.Quit();
    }

    public void loadManualCarControl()
    {
        SceneManager.LoadScene(1);
    }
}
