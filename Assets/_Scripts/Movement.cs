using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private InputAction thrust;
    [SerializeField] private InputAction rotation;
    [SerializeField] private float thrustMultiplier = 100f;
    [SerializeField] private float rotationMultiplier = 100f;
    private AudioSource audioSource;
    private Rigidbody rb;

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        Thrust();
        Rotate();
    }
   
    private void Thrust()
    {
        if (thrust.IsPressed())
        {
            if (!audioSource.isPlaying)
            {
            audioSource.Play();
            }
            rb.AddRelativeForce(Vector3.up * thrustMultiplier * Time.fixedDeltaTime);
        }
        else
        {
            audioSource.Stop();
        }
        
    }
    private void Rotate()
    {
        float rotationInput = rotation.ReadValue<float>();
        if (rotationInput > 0)
        {
            ApllyRotation(rotationMultiplier);
        }
        else if (rotationInput < 0)
        {
            ApllyRotation(-rotationMultiplier);
        }
    }

    private void ApllyRotation(float rotationDir)
    {
        rb.freezeRotation = true;
        transform.Rotate(-Vector3.forward * rotationDir * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
