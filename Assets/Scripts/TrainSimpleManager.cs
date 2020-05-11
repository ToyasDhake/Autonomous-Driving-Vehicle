using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class TrainSimpleManager : MonoBehaviour
{
    // Initialize global variables
    public GameObject individual;
    private bool isTraning = false;
    private int populationSize = 10;  // Population size
    private int generationNumber = 0;
    private int[] layers = new int[] { 5, 10, 10, 2 };  // Network discription
    private List<NeuralNetwork> nets;
    private List<CarController> carControllerList = null;
    private int count = 0;
    private bool follow = false;
    public GameObject camera;
    public Text txt;

    // Timer to kill individuals
    void Timer()
    {
        isTraning = false;
    }

    void Start()
    {
    }

    // Loops infinite times
    void Update()
    {
        // If training or atleast one is alive
        if (isTraning == false || count == 0)
        {
            if (generationNumber == 0)
            {
                // Initialize
                InitCarNeuralNetworks();
            }
            else
            {
                // Sort nets based on the fitnness
                nets.Sort();
                // Save best performing network
                string path = Application.dataPath + "/NeuralNetworkModels/Generation" + generationNumber.ToString() + ".txt";
                if (!File.Exists(path))
                {
                    File.WriteAllText(path, "");
                }

                for (int i = 0; i < nets[populationSize - 1].weights.Length; i++)
                {
                    for (int j = 0; j < nets[populationSize - 1].weights[i].Length; j++)
                    {
                        for (int k = 0; k < nets[populationSize - 1].weights[i][j].Length; k++)
                        {
                            if (k == nets[populationSize - 1].weights[i][j].Length - 1)
                                File.AppendAllText(path, nets[populationSize - 1].weights[i][j][k].ToString());
                            else
                                File.AppendAllText(path, nets[populationSize - 1].weights[i][j][k].ToString() + ",");
                        }
                        File.AppendAllText(path, "\n");
                    }
                    File.AppendAllText(path, ";\n");
                }
                // Mutate half of the population with lower score
                for (int i = 0; i < populationSize / 2; i++)
                {
                    nets[i] = new NeuralNetwork(nets[i + (populationSize / 2)]);
                    nets[i].Mutate();
                    nets[i + (populationSize / 2)] = new NeuralNetwork(nets[i + (populationSize / 2)]);
                }

            }

            // Increment genration count
            generationNumber++;
            txt.text = "Generation number: " + generationNumber.ToString();
            isTraning = true;
            // Reset timer
            Invoke("Timer", 60f);
            // Initiate individuals
            CreateCarBodies();
        }
        // Get count of alive individuals
        int tempcount = 0;
        foreach (CarController temp in carControllerList)
        {
            if (temp.alive == true)
            {
                tempcount++;
            }
        }
        count = tempcount;
        // If follow car set camera coordinates behind car
        if (follow)
        {
            Vector3 cameraRotation = new Vector3(65, 0, 0);
            camera.transform.rotation = Quaternion.Euler(cameraRotation);
            Vector3 tempVector3 = carControllerList[populationSize - 1].transform.GetChild(0).transform.position;
            tempVector3[1] += 10;
            tempVector3[2] -= 5;
            camera.transform.position = tempVector3;

        }
        // Else set camera coordinates in top
        else
        {
            Vector3 cameraRotation = new Vector3(90, 0, 0);
            camera.transform.rotation = Quaternion.Euler(cameraRotation);
            Vector3 tempVector3 = new Vector3(134, 147, -38);
            camera.transform.position = tempVector3;
        }
    }


    // Create individuals
    private void CreateCarBodies()
    {
        // Destroy all instanses of individuals if list is not empty
        if (carControllerList != null)
        {
            for (int i = 0; i < carControllerList.Count; i++)
            {
                GameObject.Destroy(carControllerList[i].gameObject);
            }

        }
        // Create list of neural networks
        carControllerList = new List<CarController>();
        for (int i = 0; i < populationSize; i++)
        {
            CarController boomer = (((GameObject)Instantiate(individual, new Vector3((i % 5) * 60, 0, (i / 5) * (-80)), transform.rotation)).transform.GetChild(0).GetComponent("CarController") as CarController);
            boomer.Init(nets[i]);
            carControllerList.Add(boomer);
        }
        count = populationSize;
    }


    // Initialize neural networks
    void InitCarNeuralNetworks()
    {
        // Population must be even, incase it is not
        if (populationSize % 2 != 0)
        {
            populationSize = 10;
        }
        // Create list of neural networks
        nets = new List<NeuralNetwork>();
        for (int i = 0; i < populationSize; i++)
        {
            NeuralNetwork net = new NeuralNetwork(layers);
            net.Mutate();
            nets.Add(net);
        }
    }

    // Follow car
    public void followToggle()
    {
        follow = !follow;
    }

    // Load main menu
    public void back()
    {
        SceneManager.LoadScene(0);
    }
}
