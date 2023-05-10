using System;
using System.Collections.Generic;

public class Dijkstra<T>
{
    private Dictionary<T, Dictionary<T, double>> graph = new Dictionary<T, Dictionary<T, double>>();
    private Dictionary<T, T> previous = new Dictionary<T, T>();
    private Dictionary<T, double> distances = new Dictionary<T, double>();

    public void AddVertex(T vertex)
    {
        graph[vertex] = new Dictionary<T, double>();
    }

    public void AddEdge(T start, T end, double weight)
    {
        if (!graph.ContainsKey(start) || !graph.ContainsKey(end))
        {
            throw new ArgumentException("Both vertices must be in the graph.");
        }

        graph[start][end] = weight;
        graph[end][start] = weight;
    }

    public List<T> FindShortestPath(T start, T end)
    {
        if (!graph.ContainsKey(start) || !graph.ContainsKey(end))
        {
            throw new ArgumentException("Both vertices must be in the graph.");
        }

        PriorityQueue<T> priorityQueue = new PriorityQueue<T>();
        HashSet<T> unvisited = new HashSet<T>();

        foreach (var vertex in graph.Keys)
        {
            unvisited.Add(vertex);
            distances[vertex] = double.MaxValue;
            previous[vertex] = default(T);
        }

        distances[start] = 0;
        priorityQueue.Enqueue(start, 0);

        while (unvisited.Count > 0 && priorityQueue.Count > 0)
        {
            T current = priorityQueue.Dequeue();

            if (current.Equals(end))
            {
                break;
            }

            unvisited.Remove(current);

            foreach (var neighbor in graph[current])
            {
                if (unvisited.Contains(neighbor.Key))
                {
                    double tentativeDistance = distances[current] + neighbor.Value;
                    if (tentativeDistance < distances[neighbor.Key])
                    {
                        distances[neighbor.Key] = tentativeDistance;
                        previous[neighbor.Key] = current;
                        priorityQueue.Enqueue(neighbor.Key, tentativeDistance);
                    }
                }
            }
        }

        if (previous[end] == null && !end.Equals(start))
        {
            return null; // No path found
        }

        List<T> path = new List<T>();
        T currentStep = end;

        while (currentStep != null)
        {
            path.Add(currentStep);
            currentStep = previous[currentStep];
        }

        path.Reverse();
        return path;
    }
}