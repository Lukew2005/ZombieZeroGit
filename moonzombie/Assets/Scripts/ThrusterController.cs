using UnityEngine;

public class ThrusterController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float thrust = 10f;
    private Rigidbody rb;

    [Header("Thruster Effects")]
    public ParticleSystem thrusterParticles;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Optional: Stop particles at start in case they're playing by default
        if (thrusterParticles != null)
            thrusterParticles.Stop();
    }

    void Update()
    {
        bool isThrusting = Input.GetKey(KeyCode.W);

        if (isThrusting)
        {
            rb.AddForce(transform.forward * thrust);

            if (thrusterParticles != null && !thrusterParticles.isPlaying)
                thrusterParticles.Play();
        }
        else
        {
            if (thrusterParticles != null && thrusterParticles.isPlaying)
                thrusterParticles.Stop();
        }
    }
}
