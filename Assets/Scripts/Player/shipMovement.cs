using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
public class shipMovement : MonoBehaviour
{

    #region new Movement
    public Rigidbody rb;
    public float accelarationFactor = 30.0f
                , turnFactor = 3.0f
                , rotationAngle = 0
                , driftFactor = 1f
                , maxSpeed = 2f;

    float accelarionInput = 0
         , steeringInput = 0
         , velocityVsUp = 0;

    GameObject _player;

    [SerializeField] bool sailDown = false;
    [SerializeField] bool canMove = true;

    [SerializeField] SteeringWheel _steer;
    [SerializeField] buttonController _windButton;
    #endregion
    #region Float
    [SerializeField] float depthBeforeSubmerged = 1f;
    [SerializeField] float displacementAmount = 3f;
    [SerializeField] int floaterCount = 1;
    [SerializeField] float waterDrag = 0.99f;
    [SerializeField] float waterAngularDrag = 0.5f;
    #endregion
    [SerializeField] float motorFoamMultiplier;
    [SerializeField] float motorFoamBase;
    [SerializeField] float frontFoamMultiplier;

    [SerializeField] float trust;
    [SerializeField] float turningSpeed;


    //Rigidbody rb;

    ParticleSystem.EmissionModule motor, front;
    [SerializeField] GameObject _motor;
    [SerializeField] GameObject _front;

    shipAnimator _animator;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _animator = GetComponent<shipAnimator>();
        motor = _motor.GetComponent<ParticleSystem>().emission;
        _player = this.gameObject;
        //front = _front.GetComponent<ParticleSystem>().emission;
    }

    void FixedUpdate()
    {
        setInputVector(new Vector2(_steer.getValue(), _windButton.getValue()));
        if (canMove)
        {

            _animator.sailingAnim(accelarionInput);
            _animator.turningAnim(steeringInput);
            ApplySteering();
            ApplyEngineForce();
            if(accelarionInput <= 0)
                KillOrtogonalVelocity();
        }
        //if (Input.GetAxis("Horizontal") < -0.2f || Input.GetAxis("Horizontal") > 0.2f)
        //{
        //    transform.rotation = Quaternion.EulerRotation(transform.rotation.x, transform.rotation.ToEulerAngles().y + Input.GetAxis("Horizontal") * turningSpeed * Time.fixedDeltaTime, transform.rotation.z);
        //}
        //if (Input.GetAxis("Vertical") > 0.2f)
        //{
        //    transform.Translate(Vector3.forward * trust * Time.fixedDeltaTime * Input.GetAxis("Vertical"));
        //    //rb.AddRelativeForce(Vector3.forward * trust * Time.fixedDeltaTime * Input.GetAxis("Vertical"));
        //}

        if (accelarionInput != 0)
        {
            _animator.idleAnim(true);
        }
        else
        {
            _animator.idleAnim(false);
        }

        shipFloat();
        motor.rateOverDistanceMultiplier = motorFoamMultiplier * accelarionInput + motorFoamBase;
        //front.rateOverDistanceMultiplier = frontFoamMultiplier * rb.velocity.magnitude;


    }

    void shipFloat ()
    {
        rb.AddForceAtPosition(Physics.gravity / floaterCount, transform.position, ForceMode.Acceleration);
        float waveHeight = (waveManager.instance.getWaveHeight(transform.position.x) / 4);
        if (transform.position.y < waveHeight)
        {
            float displacementMultiplier = Mathf.Clamp01((waveHeight - transform.position.y) / depthBeforeSubmerged) * displacementAmount;
            rb.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), transform.position, ForceMode.Acceleration);
            rb.AddForce(displacementMultiplier * (-rb.velocity/2f) * waterDrag * Time.deltaTime, ForceMode.VelocityChange);
            rb.AddForce(displacementMultiplier * (-rb.angularVelocity/2f) * waterAngularDrag * Time.deltaTime, ForceMode.VelocityChange);
        }
    }

    void ApplyEngineForce()
    {
        velocityVsUp = Vector2.Dot(transform.up, rb.velocity);
        if (velocityVsUp > maxSpeed && accelarionInput > 0)
        {
            return;
        }
        else if (velocityVsUp > -maxSpeed * 0.5f && accelarionInput < 0)
        {
            return;
        }
        if (rb.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelarionInput > 0)
        {
            return;
        }
        if (accelarionInput == 0)
        {
            rb.drag = Mathf.Lerp(rb.drag, 3.0f, Time.fixedDeltaTime * 3);
        }
        else
        {
            rb.drag = 0;
        }
        Vector3 engineForceVector = transform.forward * accelarionInput * accelarationFactor;

        rb.AddForce(engineForceVector, ForceMode.Impulse);
        //transform.Translate(engineForceVector * Time.fixedDeltaTime);

    }

    void ApplySteering()
    {
        float minSpeedBeforeAllowTurningFactor = (rb.velocity.magnitude / 8);
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);
        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor;
        Quaternion deltaRotation = Quaternion.Euler(0f, -rotationAngle, 0f).normalized;
        rb.MoveRotation(deltaRotation);
    }
    public void setInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelarionInput = inputVector.y;
        //Debug.Log("Steer: "+steeringInput+" Wind: "+accelarionInput);
    }
    void KillOrtogonalVelocity()
    {
        //Vector3 forwardVelocity = transform.forward * Vector2.Dot(rb.velocity, transform.forward);
        //Vector3 rigidVelocity = transform.right * Vector2.Dot(rb.velocity, transform.right);
        //rb.velocity = forwardVelocity + rigidVelocity * driftFactor;
        rb.velocity = new Vector3(rb.velocity.x / driftFactor, rb.velocity.y, rb.velocity.z / driftFactor);
    }
}
