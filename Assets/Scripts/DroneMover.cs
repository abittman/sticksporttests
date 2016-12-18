using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMover : MonoBehaviour {

    public GameObject droneMeshObj;
    public float moveSpeed = 10f;

    public Transform botLeftPos;
    public Transform botCentrePos;
    public Transform botRightPos;
    public Transform midLeftPos;
    public Transform midCentrePos;
    public Transform midRightPos;
    public Transform upLeftPos;
    public Transform upCentrePos;
    public Transform upRightPos;

    float currentYRot = 0f;
    float goalYRot = 0f;

    bool isTurning = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        MoveForward();
        if(isTurning)
        {
            DoTurning();
        }
	}

    public void MoveForward()
    {
        transform.position += transform.forward * Time.deltaTime * moveSpeed;
    }

    public void SlideToPosition(NineSlicePos nextNineSlicePos)
    {
        Vector3 newPos = Vector3.zero;

        switch(nextNineSlicePos)
        {
            case NineSlicePos.Bottom_Left:
                newPos = botLeftPos.position;
                break;
            case NineSlicePos.Bottom_Centre:
                newPos = botCentrePos.position;
                break;
            case NineSlicePos.Bottom_Right:
                newPos = botRightPos.position;
                break;
            case NineSlicePos.Mid_Left:
                newPos = midLeftPos.position;
                break;
            case NineSlicePos.Mid_Centre:
                newPos = midCentrePos.position;
                break;
            case NineSlicePos.Mid_Right:
                newPos = midRightPos.position;
                break;
            case NineSlicePos.Upper_Left:
                newPos = upLeftPos.position;
                break;
            case NineSlicePos.Upper_Centre:
                newPos = upCentrePos.position;
                break;
            case NineSlicePos.Upper_Right:
                newPos = upRightPos.position;
                break;
        }

        droneMeshObj.transform.position = newPos;
    }

    void DoTurning()
    {
        //temporary. Will do over time soon
        transform.eulerAngles = new Vector3(0f, goalYRot, 0f);
        currentYRot = goalYRot;
        isTurning = false;
    }

    public void DoLeftTurn()
    {
        goalYRot = currentYRot - 90f;
        if(goalYRot < 0f)
        {
            goalYRot += 360f;
        }
        isTurning = true;
    }

    public void DoRightTurn()
    {
        goalYRot = currentYRot + 90f;
        if (goalYRot > 270f)
        {
            goalYRot -= 360f;
        }
        isTurning = true;
    }

    public void DoDownTurn()
    {

    }

    public void DoUpTurn()
    {

    }
}
