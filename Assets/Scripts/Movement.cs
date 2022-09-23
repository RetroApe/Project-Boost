using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    [SerializeField] float rocketSpeed = 50f;
    [SerializeField] private float rotationSpeed = 200f;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
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
