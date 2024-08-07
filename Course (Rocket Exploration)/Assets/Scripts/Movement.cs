using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 200f;
    [SerializeField] float rotationThrust = 100f;

    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem rightBoosterParticles;
    [SerializeField] ParticleSystem leftBoosterParticles;

    Rigidbody rb;
    AudioSource audioSource;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }


    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }

        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();

        mainEngineParticles.Stop();
    }


    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    void RotateLeft()
    {
        ApplyRotation(Vector3.forward);

        if (!rightBoosterParticles.isPlaying)
        {
            rightBoosterParticles.Play();
            leftBoosterParticles.Stop();
        }
    }

    void RotateRight()
    {
        ApplyRotation(Vector3.back);

        if (!leftBoosterParticles.isPlaying)
        {
            leftBoosterParticles.Play();
            rightBoosterParticles.Stop();
        }
    }

    void StopRotating()
    {
        rightBoosterParticles.Stop();
        leftBoosterParticles.Stop();
    }


    void ApplyRotation(Vector3 vector)
    {
        rb.freezeRotation = true;
        transform.Rotate(vector * rotationThrust * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
