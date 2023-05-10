using System;
using System.Collections.Generic;

public class PriorityQueue<T>
{
    private List<KeyValuePair<T, double>> elements = new List<KeyValuePair<T, double>>();

    public int Count
    {
        get { return elements.Count; }
    }

    public void Enqueue(T item, double priority)
    {
        elements.Add(new KeyValuePair<T, double>(item, priority));
        elements.Sort((x, y) => x.Value.CompareTo(y.Value));
    }

    public T Dequeue()
    {
        if (elements.Count == 0)
        {
            throw new InvalidOperationException("The priority queue is empty.");
        }

        T item = elements[0].Key;
        elements.RemoveAt(0);
        return item;
    }
}
