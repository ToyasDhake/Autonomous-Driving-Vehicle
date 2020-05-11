using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;


public class TestComplexManager : MonoBehaviour
{
    // Start is called before the first frame update
    private float[][][] weights = new float[3][][];
    private NeuralNetwork net;
    public GameObject individual;
    private bool follow = false;
    public GameObject camera;
    private CarController carController;

    void Start()
    {
        // Initialize array for weights
        weights[0] = new float[10][];
        weights[1] = new float[10][];
        weights[2] = new float[2][];
        for (int i = 0; i < 10; i++)
            weights[0][i] = new float[5];
        for (int i = 0; i < 10; i++)
            weights[1][i] = new float[10];
        for (int i = 0; i < 2; i++)
            weights[2][i] = new float[10];
        // Read weights from model file
        string path = Application.dataPath + "/TrainedModel.txt";
        StreamReader reader = new StreamReader(path);
        for (int i = 0; i < 10; i++)
        {
            string temp = reader.ReadLine();
            string[] vals = temp.Split(',');
            for (int j = 0; j < 5; j++)
                weights[0][i][j] = float.Parse(vals[j]);
        }
        string temp1 = reader.ReadLine();
        for (int i = 0; i < 10; i++)
        {
            string temp = reader.ReadLine();
            string[] vals = temp.Split(',');
            for (int j = 0; j < 10; j++)
                weights[1][i][j] = float.Parse(vals[j]);
        }
        string temp2 = reader.ReadLine();
        for (int i = 0; i < 2; i++)
        {
            string temp = reader.ReadLine();
            string[] vals = temp.Split(',');
            for (int j = 0; j < 10; j++)
                weights[2][i][j] = float.Parse(vals[j]);
        }
        reader.Close();
        // Initialize car and control network
        net = new NeuralNetwork(weights);
        carController = ((GameObject)Instantiate(individual, new Vector3(0, 0, 0), transform.rotation)).transform.GetChild(0).GetComponent("CarController") as CarController;
        carController.Init(net);
    }

    // Update is called once per frame
    void Update()
    {
        // If follow set camera coordinates behind car
        if (follow)
        {
            Vector3 cameraRotation = new Vector3(65, 0, 0);
            camera.transform.rotation = Quaternion.Euler(cameraRotation);
            Vector3 tempVector3 = carController.transform.GetChild(0).transform.position;
            tempVector3[1] += 10;
            tempVector3[2] -= 5;
            camera.transform.position = tempVector3;

        }
        // Else set camera coordinates in top
        else
        {
            Vector3 cameraRotation = new Vector3(90, 0, 0);
            camera.transform.rotation = Quaternion.Euler(cameraRotation);
            Vector3 tempVector3 = new Vector3(-16, 77, -20);
            camera.transform.position = tempVector3;
        }
    }

    // Load main menu
    public void back()
    {
        SceneManager.LoadScene(0);
    }

    // Follow car
    public void followToggle()
    {
        follow = !follow;
    }
}
