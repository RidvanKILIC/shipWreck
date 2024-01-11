using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipAnimator : MonoBehaviour
{
    Animator _anim;
    [SerializeField] GameObject turnObj;
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
        Debug.Log("Horizontal value: " + _hor);
        //if (_hor > 0f)
        //{
        //    if (turnObj.transform.rotation.y != 1400f)
        //    {
        //        //LeanTween.rotateY(turnObj, 140f, 1f).setEase(LeanTweenType.easeInCubic);
        //        LeanTween.value(turnObj, turnObj.transform.rotation.y, 140f, 0.5f).setOnUpdate((a) => turnObj.transform.rotation = Quaternion.Euler(0f, a, 0f)).setEase(LeanTweenType.linear);
        //    }
        //}
        //else if (_hor < 0f)
        //{
        //    if (turnObj.transform.rotation.y != 70f)
        //    {
        //        //LeanTween.rotateY(turnObj, 70f, 1f).setEase(LeanTweenType.easeInCubic);
        //        LeanTween.value(turnObj, turnObj.transform.rotation.y, 70f, 0.5f).setOnUpdate((a) => turnObj.transform.rotation = Quaternion.Euler(0f, a, 0f)).setEase(LeanTweenType.linear);
        //    }
               
        //}
        //else
        //{
        //    if(turnObj.transform.rotation.y != 90f)
        //    {
        //        //LeanTween.rotateY(turnObj,90f, 1f).setEase(LeanTweenType.easeInCubic);
        //        LeanTween.value(turnObj, turnObj.transform.rotation.y, 90f, 0.5f).setOnUpdate((a) => turnObj.transform.rotation = Quaternion.Euler(0f, a, 0f)).setEase(LeanTweenType.linear);
        //    }
           
        //}
        _anim.SetFloat("Horizontal", _hor);
    }
    public void idleAnim(bool _idle)
    {
        _anim.SetBool("Idle", _idle);
    }
}
