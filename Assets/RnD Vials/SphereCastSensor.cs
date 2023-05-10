using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCastSensor : AbstractInterval
{
    [SerializeField] private float radius;
    [SerializeField] private float range;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private QueryTriggerInteraction interaction;

    [Header("Runtime")]
    [SerializeField] private bool hit;

    protected override void Tick()
    {
        Ray ray = new Ray()
        {
            origin = transform.position,
            direction = transform.forward
        };
        hit = Physics.SphereCast(ray, radius, range, layerMask, interaction);
    }
}
