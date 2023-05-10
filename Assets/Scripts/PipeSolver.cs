using System;
using System.Collections.Generic;
using UnityEngine;

public class PipeSolver : Dijkstra<Pipe>
{
    private Dictionary<Pipe, Dictionary<Pipe, float>> graph = new Dictionary<Pipe, Dictionary<Pipe, float>>();
    private Dictionary<Pipe, float> flow = new Dictionary<Pipe, float>();
    private float gravity = 9.81f; // m/s^2
    private float density = 1000; // kg/m^3
    private float viscosity = 1.0f; // Pa.s (This is an example value. Use the appropriate value for the fluid in the pipes.)

    public void AddPipe(Pipe pipe)
    {
        graph[pipe] = new Dictionary<Pipe, float>();
        flow[pipe] = 0;
        base.AddVertex(pipe);
    }

    public void AddConnection(Pipe start, Pipe end)
    {
        float length = Mathf.Sqrt(Mathf.Pow(start.Length, 2) + Mathf.Pow(start.Diameter / 2 + end.Diameter / 2, 2));
        float diameter = Mathf.Min(start.Diameter, end.Diameter);
        float resistance = 128.0f * viscosity * length / (Mathf.Pow(diameter, 4) * Mathf.PI);
        float weight = density * gravity * length * Mathf.Sin(Mathf.Atan(start.Length / (start.Diameter / 2 + end.Diameter / 2)));
        graph[start][end] = resistance;
        graph[end][start] = resistance;

        // Incorporate the pump power into the edge weight (e.g., by reducing the weight)
        double pumpPowerImpact = 0;
        if (start.HasPump)
        {
            pumpPowerImpact = start.PumpPower;
        }
        if (end.HasPump)
        {
            pumpPowerImpact = Math.Max(pumpPowerImpact, end.PumpPower);
        }

        // Apply the pump power impact to the weight based on the specific logic you want to use.
        // For example, you could subtract a percentage of the pump power from the weight.
        double adjustedWeight = weight - (pumpPowerImpact * 0.01);
        base.AddEdge(start, end, adjustedWeight);
    }

    public void AddPump(Pipe pipe, float power)
    {
        pipe.HasPump = true;
        pipe.PumpPower = power;
    }

    public void RunSimulation(float timeStep)
    {
        foreach (var pipe in graph.Keys)
        {
            flow[pipe] = 0;
        }

        foreach (var start in graph.Keys)
        {
            foreach (var end in graph[start].Keys)
            {
                float resistance = graph[start][end];
                float pressureDrop = flow[start] * Math.Abs(flow[start]) * resistance;
                float flowRate = flow[start] - pressureDrop * timeStep / density;

                if (start.HasPump)
                {
                    float pumpPower = start.PumpPower;
                    float head = pumpPower / (flowRate * density * gravity);
                    pressureDrop += head;
                    flowRate = pumpPower / (pressureDrop * density);
                }

                flow[start] = flowRate;
                flow[end] = -flowRate;
            }
        }
    }
}