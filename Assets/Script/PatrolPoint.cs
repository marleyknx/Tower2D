using System.Collections.Generic;
using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
   public List<Transform> Points;
    [SerializeField] private LineRenderer lineRenderer;

    public void Awake()
    {
        foreach (Transform child in transform) Points.Add(child);
        lineRenderer.positionCount = Points.Count;
    }


    private void Update()
    {
        for (int i = 0; i < Points.Count; i++)
        {
            lineRenderer.SetPosition(i, Points[i].position);
        }
    }
}
