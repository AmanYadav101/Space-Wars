using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movemeny_JoyStick : MonoBehaviour
{

    public GameObject joyStick;
    public GameObject joyStickBG;
    public Vector2 joyStickVec;
    private Vector2 joyStickTouchPos;
    private Vector2 joyStickOriginalPos;
    private float joyStickradius;




    // Start is called before the first frame update
    void Start()
    {
        joyStickOriginalPos = joyStickBG.transform.position;
        joyStickradius = joyStickBG.GetComponent<RectTransform>().sizeDelta.y / 4;
    }
    
    public void PointDown()
    {
        joyStick.transform.position = Input.mousePosition;
        joyStickBG.transform.position = Input.mousePosition;
        joyStickTouchPos = Input.mousePosition;
    }
    public void Drag(BaseEventData baseEventData) 
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        joyStickVec = (dragPos-joyStickTouchPos).normalized;

        float joyStickDist = Vector2.Distance(dragPos, joyStickTouchPos);
        if (joyStickDist < joyStickradius)
        {
            joyStick.transform.position = joyStickTouchPos + joyStickVec * joyStickDist;
        }
        else
        {
            joyStick.transform.position = joyStickTouchPos + joyStickVec * joyStickradius;
        }
    }

    public void PointerUp()
    {
        joyStickVec = Vector2.zero;
        joyStick.transform.position = joyStickOriginalPos;
        joyStickBG.transform.position= joyStickOriginalPos;
    }
}
