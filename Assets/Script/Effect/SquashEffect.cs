using UnityEngine;

public class SquashEffect : MonoBehaviour
{
    private Vector3 currentScale = Vector3.one;
    private Vector3 restScale = Vector3.one;
    private Vector3 velocity;

    public void Initialize(Vector3 localScale)
    {
        currentScale = localScale;
        restScale = localScale;
    }

    public Vector3 UpdateSquashEffect(float time)
    {
        currentScale = Vector3.SmoothDamp(currentScale, restScale, ref velocity, time);
        return currentScale;
    }


    public void VisualHit(float x,float y,float z)
    {
        currentScale = new Vector3(x, y, z);
    }
}
