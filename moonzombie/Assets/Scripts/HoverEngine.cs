using UnityEngine;

public class HoverEngine : MonoBehaviour
{
    public float hoverAmplitude = 0.5f;    // Height range of hover motion
    public float hoverFrequency = 1f;      // Speed of hover oscillation
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float hoverOffset = Mathf.Sin(Time.time * hoverFrequency) * hoverAmplitude;
        transform.position = startPos + new Vector3(0, hoverOffset, 0);
    }
}
