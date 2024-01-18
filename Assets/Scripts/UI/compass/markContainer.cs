using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class markContainer : MonoBehaviour
{
    mark _mark;
    public mark getMark()
    {
        return _mark;
    }
    public void setMark(mark _mrk)
    {
        _mark = _mrk;
    }
}
