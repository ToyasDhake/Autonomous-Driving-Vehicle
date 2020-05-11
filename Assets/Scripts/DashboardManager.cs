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

    // Exit application
    public void exitApplication()
    {
        Application.Quit();
    }

    // Manual controlled car scene
    public void loadManualCarControl()
    {
        SceneManager.LoadScene(1);
    }

    // Train car for simple track
    public void loadTrainSimple()
    {
        SceneManager.LoadScene(2);
    }

    // Train car for complex track
    public void loadTrainComplex()
    {
        SceneManager.LoadScene(3);
    }

    // Test car on simple track
    public void loadTestSimple()
    {
        SceneManager.LoadScene(4);
    }

    // Test car on complex track
    public void loadTestComplex()
    {
        SceneManager.LoadScene(5);
    }
}
