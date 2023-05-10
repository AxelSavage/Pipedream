using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    public GameObject pipePrefab;
    private List<GameObject> pipes = new List<GameObject>();
    private PipeSolver pipeSolver;

    void Start()
    {
        pipeSolver = new PipeSolver();
        int numPipes = 5;
        float diameter = 1.0f;
        float flowRate = 5.0f;
        float pumpPower = 100.0f;

        Pipe[] pipes = new Pipe[numPipes];
        GameObject[] pipeGameObjects = new GameObject[numPipes];

        // Create and connect pipes
        for (int i = 0; i < numPipes; i++)
        {
            pipes[i] = new Pipe { Id = i + 1, Diameter = diameter, Length = 10.0f + i * 2.0f, FlowRate = flowRate };

            pipeSolver.AddPipe(pipes[i]);
            pipeGameObjects[i] = CreatePipeGameObject(pipes[i]);

            if (i > 0)
            { 
                pipeSolver.AddConnection(pipes[i - 1], pipes[i]);
                     pipeGameObjects[i].transform.position = pipeGameObjects[i - 1].transform.position + new Vector3(0, (pipes[i - 1].Length + pipes[i].Length) / 2, 0);
            }
        }

        // Add a pump to the bottom pipe
        pipeSolver.AddPump(pipes[0], pumpPower);
    }

    GameObject CreatePipeGameObject(Pipe pipe)
    {
        GameObject pipeObj = Instantiate(pipePrefab);
        pipeObj.name = $"Pipe_{pipe.Id}";
        pipeObj.transform.localScale = new Vector3(pipe.Diameter, pipe.Length / 2.0f, pipe.Diameter);
        pipeObj.transform.rotation = Quaternion.Euler(0, 0, 180.0f);
        pipes.Add(pipeObj);
        return pipeObj;
    }
}
