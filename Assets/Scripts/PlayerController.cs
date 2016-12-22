using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NineSlicePos
{
    None = 0,
    Bottom_Left = 1,
    Bottom_Centre = 2,
    Bottom_Right = 3,
    Mid_Left = 4,
    Mid_Centre = 5,
    Mid_Right = 6,
    Upper_Left = 7,
    Upper_Centre = 8,
    Upper_Right = 9
}

public class PlayerController : MonoBehaviour {

    public DroneMover droneMover;

    bool swiping = false;
    Vector2 lastTouchPosition;
    bool eventSent = false;

    public float swipeMinAmount;

    Vector2 lastMousePos;

    // Use this for initialization
    void Start ()
    {
        swipeMinAmount = Screen.width / 30f;

    }

    // Update is called once per frame
    void Update ()
    {
        if (Application.isMobilePlatform)
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).deltaPosition.sqrMagnitude != 0)
                {
                    if (!swiping)
                    {
                        swiping = true;
                        lastTouchPosition = Input.GetTouch(0).position;
                    }
                    else
                    {
                        if (!eventSent)
                        {
                            Vector2 swipeDirection = Input.GetTouch(0).position - lastTouchPosition;

                            //If more horizontal than vertical
                            if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
                            {
                                //Swiped left && swipe amount acceptable
                                if (swipeDirection.x < -swipeMinAmount)
                                {
                                    LeftInput();
                                }
                                else if (swipeDirection.x > swipeMinAmount)
                                {
                                    RightInput();
                                }
                            }
                            else
                            {
                                if (swipeDirection.y < -swipeMinAmount)
                                {
                                    DownInput();
                                }
                                else if (swipeDirection.y > swipeMinAmount)
                                {
                                    UpInput();
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                swiping = false;
                eventSent = false;
                //tapped = false;
            }
        }
        else if (Application.isEditor)
        {
            if (Input.GetMouseButton(0))
            {
                /*if (!eventSent)
                    DoTapAction();
                */
                Vector2 currentMousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

                if (currentMousePos != lastMousePos)
                {
                    if (!swiping)
                    {
                        swiping = true;
                        lastMousePos = currentMousePos;
                    }
                    else
                    {
                        if (!eventSent)
                        {
                            Vector2 swipeDirection = currentMousePos - lastMousePos;

                            //If more horizontal than vertical
                            if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
                            {
                                //Swiped left && swipe amount acceptable
                                if (swipeDirection.x < -swipeMinAmount)
                                {
                                    LeftInput();
                                }
                                else if (swipeDirection.x > swipeMinAmount)
                                {
                                    RightInput();
                                }
                            }
                            else
                            {
                                if (swipeDirection.y < -swipeMinAmount)
                                {
                                    DownInput();
                                }
                                else if (swipeDirection.y > swipeMinAmount)
                                {
                                    UpInput();
                                }
                            }
                        }
                    }
                }

                lastMousePos = currentMousePos;
            }
            else
            {
                lastMousePos = Vector2.zero;
                swiping = false;
                eventSent = false;
                //tapped = false;
            }
        }
    }

    public void NineSliceInput(int nineSliceToInt)
    {
        if(nineSliceToInt > 0
            && nineSliceToInt < 10)
        {
            droneMover.SlideToPosition((NineSlicePos)nineSliceToInt);
        }
    }

    void LeftInput()
    {
        droneMover.DoLeftTurn();
        eventSent = true;
    }

    void RightInput()
    {
        droneMover.DoRightTurn();
        eventSent = true;
    }

    void DownInput()
    {
        droneMover.DoDownTurn();
        eventSent = true;
    }

    void UpInput()
    {
        droneMover.DoUpTurn();
        eventSent = true;
    }


}
