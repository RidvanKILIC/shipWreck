using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class compass : MonoBehaviour
{
    [SerializeField] RawImage compassImg;
    [SerializeField] Transform _player;
    [SerializeField] GameObject cmpMarkPrefab;

    [SerializeField] List<mark> cmpMarks = new List<mark>();
    List<GameObject> markObjs = new List<GameObject>();
    float compassUnit;
    private void Start()
    {
        compassUnit = compassImg.rectTransform.rect.width / 360f;
        addMarkersToList();
    }
    void addMarkersToList()
    {
        foreach(mark _mark in cmpMarks)
        {
            addMarker(_mark);
        }
    }
    // Start is called before the first frame update
    void Update()
    {
        compassImg.uvRect = new Rect(_player.localEulerAngles.y / 360f, 0f, 1f, 1f);
        foreach(GameObject _mark in markObjs)
        {
            _mark.GetComponent<RectTransform>().anchoredPosition = getPos(_mark.GetComponent<markContainer>().getMark());
        }
    }
    public void addMarker(mark mark)
    {
        GameObject newMarker = Instantiate(cmpMarkPrefab, compassImg.transform);
        newMarker.transform.Find("icn").GetComponent<Image>().overrideSprite = mark.getIcn();
        markObjs.Add(newMarker);
        newMarker.GetComponent<markContainer>().setMark(mark);
        newMarker.GetComponent<RectTransform>().anchoredPosition = getPos(mark);
    }
    Vector2 getPos(mark _mark)
    {
        Vector2 playerPos = new Vector2(_player.transform.position.x, _player.transform.position.z);
        Vector2 playerFwd = new Vector2(_player.transform.forward.x, _player.transform.forward.z);
        float angle = Vector2.SignedAngle(_mark.getPos() - playerPos, playerFwd);
        return new Vector2(compassUnit * angle, 0f);
    }
}
