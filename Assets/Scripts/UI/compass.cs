using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class compass : MonoBehaviour
{
    [SerializeField] RawImage compassImg;
    [SerializeField] Transform _player;
    // Start is called before the first frame update
    void Update()
    {
        compassImg.uvRect = new Rect(_player.localEulerAngles.y / 360f, 0f, 1f, 1f);
    }
}
