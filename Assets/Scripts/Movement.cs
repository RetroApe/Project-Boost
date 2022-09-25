using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    [SerializeField] float rocketSpeed = 25f;
    [SerializeField] float rotationSpeed = 250f;
    [SerializeField] AudioClip rocketThrust;
    [SerializeField] ParticleSystem leftThruster;
    [SerializeField] ParticleSystem rightThruster;
    [SerializeField] ParticleSystem mainThruster;

    AudioSource audioSource;
    Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ThrustEffects();
        MovePlayer();
    }

    void ThrustEffects()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.PlayOneShot(rocketThrust);
            mainThruster.Play();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            audioSource.Stop();
            mainThruster.Stop();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            rightThruster.Play();
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            rightThruster.Stop();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            leftThruster.Play();
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            leftThruster.Stop();
        }
    }

    void MovePlayer()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(rocketSpeed * Time.deltaTime * Vector3.up, ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationSpeed);
        }
    }

    private void ApplyRotation(float rotationPolarity)
    {
        rb.freezeRotation = true;
        transform.Rotate(rotationPolarity * Time.deltaTime * Vector3.forward);
        rb.freezeRotation = false;
    }
}
