using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveManager : MonoBehaviour
{
    public static waveManager instance;
    [SerializeField] float amplitude=1f;
    [SerializeField] float length = 1f;
    [SerializeField] float speed = 1f;
    [SerializeField] float offset = 1f;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
        
    }

    // Update is called once per frame
    void Update()
    {
        offset += Time.deltaTime * speed;
    }
    public float getWaveHeight(float x)
    {
        return amplitude * Mathf.Sin(x / length + offset);
    }
}
