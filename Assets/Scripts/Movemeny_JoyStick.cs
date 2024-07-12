using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movemeny_JoyStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public GameObject joyStick;
    public GameObject joyStickBG;
    public Vector2 joyStickVec;
    private Vector2 joyStickTouchPos;
    private Vector2 joyStickOriginalPos;
    private float joyStickradius;

    // Add a sensitivity variable
    public float sensitivity = 0.5f;

    void Start()
    {
        joyStickOriginalPos = joyStickBG.transform.position;
        joyStickradius = joyStickBG.GetComponent<RectTransform>().sizeDelta.y / 4;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        joyStick.transform.position = eventData.position;
        joyStickBG.transform.position = eventData.position;
        joyStickTouchPos = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 dragPos = eventData.position;
        joyStickVec = (dragPos - joyStickTouchPos).normalized;








        float joyStickDist = Vector2.Distance(dragPos, joyStickTouchPos);
/*        joyStick.transform.position  = joyStickTouchPos + joyStickVec * joyStickDist;
*/
        if (joyStickDist < joyStickradius)
        {
            joyStick.transform.position = joyStickTouchPos + joyStickVec * joyStickDist;
        }
        else
        {
            joyStick.transform.position = joyStickTouchPos + joyStickVec * joyStickDist;
            joyStickBG.transform.position = joyStickTouchPos + joyStickVec * joyStickDist;

        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        joyStickVec = Vector2.zero;
        joyStick.transform.position = joyStickOriginalPos;
        joyStickBG.transform.position = joyStickOriginalPos;
    }
}
