using UnityEngine;

public abstract class AbstractInterval : MonoBehaviour
{
    [SerializeField] private float interval = 0.25f;
    private float time = 0.0f;

    private void OnEnable()
    {
        time = Time.time;
    }

    private void OnDisable()
    {
        time = 0.0f;
    }

    private void Update()
    {
        if (time < Time.time)
        {
            time = Time.time + interval;
            Tick();
        }
    }

    protected abstract void Tick();
}
