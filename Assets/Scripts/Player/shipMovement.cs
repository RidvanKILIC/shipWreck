using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipMovement : MonoBehaviour
{

    [SerializeField] float motorFoamMultiplier;
    [SerializeField] float motorFoamBase;
    [SerializeField] float frontFoamMultiplier;

    [SerializeField] float trust;
    [SerializeField] float turningSpeed;


    Rigidbody rb;

    ParticleSystem.EmissionModule motor, front;
    [SerializeField] GameObject _motor;
    [SerializeField] GameObject _front;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        motor = _motor.GetComponent<ParticleSystem>().emission;
        front = _front.GetComponent<ParticleSystem>().emission;
    }

    void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") < -0.2f || Input.GetAxis("Horizontal") > 0.2f)
        {
            transform.rotation = Quaternion.EulerRotation(transform.rotation.x, transform.rotation.ToEulerAngles().y + Input.GetAxis("Horizontal") * turningSpeed * Time.fixedDeltaTime, transform.rotation.z);
        }
        if (Input.GetAxis("Vertical") > 0.2f)
        {
            transform.Translate(Vector3.forward * trust * Time.fixedDeltaTime * Input.GetAxis("Vertical"));
            //rb.AddRelativeForce(Vector3.forward * trust * Time.fixedDeltaTime * Input.GetAxis("Vertical"));
        }

        motor.rateOverDistanceMultiplier = motorFoamMultiplier * Input.GetAxis("Vertical") + motorFoamBase;
        front.rateOverDistanceMultiplier = frontFoamMultiplier * rb.velocity.magnitude;


    }
}
