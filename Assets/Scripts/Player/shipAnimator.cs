using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipAnimator : MonoBehaviour
{
    Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        _anim =transform.GetChild(1).gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void sailingAnim(float _vert)
    {
        _anim.SetFloat("Vertical", _vert);
    }
    public void turningAnim(float _hor)
    {
        _anim.SetFloat("Horizontal", _hor);
    }
    public void idleAnim(bool _idle)
    {
        _anim.SetBool("Idle", _idle);
    }
}
