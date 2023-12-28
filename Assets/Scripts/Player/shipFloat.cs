using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class shipFloat : MonoBehaviour
{
    [SerializeField] Rigidbody _rb;
    [SerializeField] float depthBeforeSubmerged = 1f;
    [SerializeField] float displacementAmount = 3f;
    [SerializeField] int floaterCount = 1;
    [SerializeField] float waterDrag = 0.99f;
    [SerializeField] float waterAngularDrag = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        _rb.maxAngularVelocity = 1f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        addWaveforce();
    }
    void addWaveforce()
    {
        _rb.AddForceAtPosition(Physics.gravity / floaterCount, transform.position, ForceMode.Acceleration);
        float waveHeight = (waveManager.instance.getWaveHeight(transform.position.x)/4);
        if (transform.position.y < waveHeight)
        {
            float displacementMultiplier = Mathf.Clamp01((waveHeight-transform.position.y) / depthBeforeSubmerged) * displacementAmount;
            _rb.AddForceAtPosition(new Vector3(0f,Mathf.Abs(Physics.gravity.y)*displacementMultiplier,0f),transform.position,ForceMode.Acceleration);
            _rb.AddForce(displacementMultiplier * -_rb.velocity * waterDrag * Time.deltaTime, ForceMode.VelocityChange);
            _rb.AddForce(displacementMultiplier * -_rb.angularVelocity * waterAngularDrag * Time.deltaTime, ForceMode.VelocityChange);
        }
    }
}
