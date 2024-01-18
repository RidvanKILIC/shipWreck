using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mark : MonoBehaviour
{
    [SerializeField] Sprite icn;
    // Start is called before the first frame update
    public Vector2 getPos()
    {
        return new Vector2(this.gameObject.transform.position.x,this.gameObject.transform.position.z);
    }
    public Sprite getIcn()
    {
        return icn;
    }
}
