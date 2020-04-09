using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;


public class TestSimpleManager : MonoBehaviour
{
    // Start is called before the first frame update
    private float[][][] weights = new float[3][][];
    private NeuralNetwork net;
    public GameObject individual;
    private bool follow = false;
    public GameObject camera;
    private CarController boomer;

    void Start()
    {
        weights[0] = new float[10][];
        weights[1] = new float[10][];
        weights[2] = new float[2][];
        for (int i = 0; i < 10; i++)
            weights[0][i] = new float[5];
        for (int i = 0; i < 10; i++)
            weights[1][i] = new float[10];
        for (int i = 0; i < 2; i++)
            weights[2][i] = new float[10];
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
        net = new NeuralNetwork(weights);
        boomer = ((GameObject)Instantiate(individual, new Vector3(0, 0, 0), transform.rotation)).transform.GetChild(0).GetComponent("CarController") as CarController;
        boomer.Init(net);
    }

    // Update is called once per frame
    void Update()
    {
        if (follow)
        {
            Vector3 cameraRotation = new Vector3(65, 0, 0);
            camera.transform.rotation = Quaternion.Euler(cameraRotation);
            Vector3 tempVector3 = boomer.transform.GetChild(0).transform.position;
            tempVector3[1] += 10;
            tempVector3[2] -= 5;
            camera.transform.position = tempVector3;

        }
        else
        {
            Vector3 cameraRotation = new Vector3(90, 0, 0);
            camera.transform.rotation = Quaternion.Euler(cameraRotation);
            Vector3 tempVector3 = new Vector3(16, 72, -1.5f);
            camera.transform.position = tempVector3;
        }
    }

    public void back()
    {
        SceneManager.LoadScene(0);
    }
    public void followToggle()
    {
        follow = !follow;
    }
}
