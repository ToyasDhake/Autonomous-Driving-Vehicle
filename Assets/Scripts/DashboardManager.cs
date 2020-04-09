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

    public void loadTrainSimple()
    {
        SceneManager.LoadScene(2);
    }

    public void loadTrainComplex()
    {
        SceneManager.LoadScene(3);
    }

    public void loadTestSimple()
    {
        SceneManager.LoadScene(4);
    }

    public void loadTestComplex()
    {
        SceneManager.LoadScene(5);
    }
}
