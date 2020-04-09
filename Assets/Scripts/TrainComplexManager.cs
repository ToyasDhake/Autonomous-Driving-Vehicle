using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class TrainComplexManager : MonoBehaviour
{
    public GameObject individual;

    private bool isTraning = false;
    private int populationSize = 10;
    private int generationNumber = 0;
    private int[] layers = new int[] { 5, 10, 10, 2 };
    private List<NeuralNetwork> nets;
    private List<CarController> boomerangList = null;
    private int count = 0;
    private bool follow = false;
    public GameObject camera;
    public Text txt;


    void Timer()
    {
        isTraning = false;
    }

    void Start()
    {
    }


    void Update()
    {
        if (isTraning == false || count == 0)
        {
            if (generationNumber == 0)
            {
                InitBoomerangNeuralNetworks();
            }
            else
            {
                nets.Sort();
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
                for (int i = 0; i < populationSize / 2; i++)
                {
                    nets[i] = new NeuralNetwork(nets[i + (populationSize / 2)]);
                    nets[i].Mutate();

                    nets[i + (populationSize / 2)] = new NeuralNetwork(nets[i + (populationSize / 2)]);
                }

            }


            generationNumber++;
            txt.text = "Generation number: " + generationNumber.ToString();
            isTraning = true;
            Invoke("Timer", 60f);
            CreateBoomerangBodies();
        }
        int tempcount = 0;
        foreach (CarController temp in boomerangList)
        {
            if (temp.alive == true)
            {
                tempcount++;
            }

        }
        count = tempcount;

        if (follow)
        {
            Vector3 cameraRotation = new Vector3(65, 0, 0);
            camera.transform.rotation = Quaternion.Euler(cameraRotation);
            Vector3 tempVector3 = boomerangList[populationSize - 1].transform.GetChild(0).transform.position;
            tempVector3[1] += 10;
            tempVector3[2] -= 5;
            camera.transform.position = tempVector3;

        }
        else
        {
            Vector3 cameraRotation = new Vector3(90, 0, 0);
            camera.transform.rotation = Quaternion.Euler(cameraRotation);
            Vector3 tempVector3 = new Vector3(182, 249, -26.5f);
            camera.transform.position = tempVector3;
        }
    }

    private void CreateBoomerangBodies()
    {
        if (boomerangList != null)
        {
            for (int i = 0; i < boomerangList.Count; i++)
            {
                GameObject.Destroy(boomerangList[i].gameObject);
            }

        }

        boomerangList = new List<CarController>();

        for (int i = 0; i < populationSize; i++)
        {
            CarController boomer = (((GameObject)Instantiate(individual, new Vector3((i % 5) * 100, 0, (i / 5) * (-80)), transform.rotation)).transform.GetChild(0).GetComponent("CarController") as CarController);
            boomer.Init(nets[i]);
            boomerangList.Add(boomer);

        }
        count = populationSize;
    }

    void InitBoomerangNeuralNetworks()
    {
        //population must be even, just setting it to 20 incase it's not
        if (populationSize % 2 != 0)
        {
            populationSize = 20;
        }

        nets = new List<NeuralNetwork>();


        for (int i = 0; i < populationSize; i++)
        {
            NeuralNetwork net = new NeuralNetwork(layers);
            net.Mutate();
            nets.Add(net);
        }
    }

    public void followToggle()
    {
        follow = !follow;
    }
    public void back()
    {
        SceneManager.LoadScene(0);
    }
}
