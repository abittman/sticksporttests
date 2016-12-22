using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BansheeGz.BGSpline.Curve;
using BansheeGz.BGSpline.Components;

public class SimplePlayerCurveInput : MonoBehaviour {

    public BGCurve followCurve;
    public BGCcCursorChangeLinear cursorChange;
    public BGCcCursorObjectTranslate cursorTranslate;
    public BGCcCursorObjectRotate cursorRotate;
    public BGCcSplitterPolyline splitterPolyLine;

    public float accelerationPerSecond = 5f;
    public float decelerationPerSecond = 5f;
    public float minSpeed = 10f;
    public float maxSpeed = 30f;

    public List<Vector3> polyLineList = new List<Vector3>();

    public int currentPos = 0;

    public float rotationSpeed = 45f;

    public bool canMove = true;

    float currentSpeed;
    public UnityEngine.UI.Image speedFillImage;

    // Use this for initialization
    void Start ()
    {
        polyLineList = splitterPolyLine.Positions;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (canMove)
        {
            MoveDrone();
            RotateDrone();

            if (Input.GetMouseButton(0))
            {
                SpeedUp();
            }
            else
            {
                SlowDown();
            }
            currentSpeed = cursorChange.Speed;
            speedFillImage.fillAmount = .33f + (((currentSpeed - minSpeed) / maxSpeed) * .33f);
        }
        //CheckThePos();
	}

    void CheckThePos()
    {
        float currentDistance = Vector3.Distance(transform.position, polyLineList[currentPos]);
        float nextDistance = Vector3.Distance(transform.position, polyLineList[currentPos + 1]);

        Debug.Log(cursorRotate.Rotation);

        if(nextDistance < currentDistance)
        {
            currentPos++;
            //Debug.Log("Nearest to " + polyLineList[currentPos]);
            //Debug.Log("Angle between " + Vector3.Angle(polyLineList[currentPos], polyLineList[currentPos + 1]));
        }
    }

    void MoveDrone()
    {
        Vector3 currentLinePos = cursorTranslate.Cursor.CalculatePosition();

        //transform.position = currentLinePos;

        Vector3 nextForwardPos = transform.position + transform.forward * Time.deltaTime * cursorChange.Speed;

        Vector3 moveTowardPos = Vector3.MoveTowards(nextForwardPos, currentLinePos, Time.deltaTime);

        transform.position = moveTowardPos;
    }

    void RotateDrone()
    {
        Quaternion currentCursorRot = cursorRotate.Rotation;

        Quaternion currentRot = transform.rotation;
        
        Vector3 directionTowardsPoint = (cursorTranslate.Cursor.CalculatePosition() - transform.position).normalized;
        Quaternion lookToPoint = Quaternion.LookRotation(directionTowardsPoint, currentCursorRot * Vector3.up);

        Quaternion rotToward = new Quaternion();

        //If distance is small, dont do extra rotation
        if (Vector3.Distance(transform.position, cursorTranslate.Cursor.CalculatePosition()) <= 1f)
        {
            rotToward = Quaternion.RotateTowards(currentRot, currentCursorRot, Time.deltaTime * rotationSpeed);
        }
        else
        {
            rotToward = Quaternion.RotateTowards(currentRot, lookToPoint, Time.deltaTime * rotationSpeed);
        }

        transform.rotation = rotToward;

        //transform.Rotate(rotToward.eulerAngles, Time.deltaTime * rotationSpeed);
    }

    void SpeedUp()
    {
        //followCurve.
        //transform.position += transform.forward * Time.deltaTime * accelerationPerSecond;
        //cursorChange.Speed = accelerationPerSecond;
        cursorChange.Speed += Time.deltaTime * accelerationPerSecond;
        cursorChange.Speed = Mathf.Clamp(cursorChange.Speed, minSpeed, maxSpeed);

        
    }

    void SlowDown()
    {
        //cursorChange.Speed = 0f;

        cursorChange.Speed -= Time.deltaTime * decelerationPerSecond;
        cursorChange.Speed = Mathf.Clamp(cursorChange.Speed, minSpeed, maxSpeed);
    }

    public void PointReached(int i)
    {
        Debug.Log(i);
    }
}
