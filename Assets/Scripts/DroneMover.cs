using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TravelDirection
{
    None,
    Forward,
    Back,
    Left,
    Right
}

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

    bool isTurning = false;

    public float turnAnglePerSecond = 45f;
    float timeForCurrentTurn = 0f;

    Vector3 forwardRotation = new Vector3(0f, 0f, 0f);
    Vector3 backRotation = new Vector3(0f, 180f, 0f);
    Vector3 leftRotation = new Vector3(0f, 270f, 0f);
    Vector3 rightRotation = new Vector3(0f, 90f, 0f);
    Vector3 upRotation = new Vector3(-90f, 0f, 0f);
    Vector3 downRotation = new Vector3(90f, 0f, 0f);

    Vector3 currentRotation = Vector3.zero;
    Vector3 lastRotation = Vector3.zero;
    Vector3 goalRotation = Vector3.zero;
    Vector3 intendedEndDirection = Vector3.zero;

    TravelDirection lastDirection = TravelDirection.Forward;
    TravelDirection currentDirection  = TravelDirection.Forward;
    TravelDirection nextDirection = TravelDirection.Forward;

    bool goingUp = false;
    bool goingDown = false;

    float turnTimer = 0f;

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
        turnTimer += Time.deltaTime;
        
        Vector3 nextRot = Vector3.Lerp(lastRotation, goalRotation, turnTimer / timeForCurrentTurn);
        transform.eulerAngles = nextRot;

        if (turnTimer >= timeForCurrentTurn)
        {
            goalRotation = intendedEndDirection;
            transform.eulerAngles = intendedEndDirection;
            lastRotation = intendedEndDirection;
            currentRotation = lastRotation;
            currentDirection = nextDirection;

            turnTimer = 0f;

            isTurning = false;
        }
    }

    public void DoLeftTurn()
    {
        bool goNegative = false;
        //If has to go into negatives
        if(goalRotation.y <= 0f)
        {
            goNegative = true;
        }

        Vector3 lastGoalRot = goalRotation;

        switch (nextDirection)
        {
            case TravelDirection.Forward:
                nextDirection = TravelDirection.Left;
                if (goNegative)
                {
                    goalRotation = new Vector3(0f, leftRotation.y - 360f, 0f);
                }
                else
                {
                    goalRotation = leftRotation;
                }
                intendedEndDirection = leftRotation;
                break;
            case TravelDirection.Back:
                nextDirection = TravelDirection.Right;

                if (goNegative)
                {
                    goalRotation = new Vector3(0f, rightRotation.y - 360f, 0f);
                }
                else
                {
                    goalRotation = rightRotation;
                }
                intendedEndDirection = rightRotation;
                break;
            case TravelDirection.Left:
                nextDirection = TravelDirection.Back;

                if (goNegative)
                {
                    goalRotation = new Vector3(0f, backRotation.y - 360f, 0f);
                }
                else
                {
                    goalRotation = backRotation;
                }
                intendedEndDirection = backRotation;
                break;
            case TravelDirection.Right:
                nextDirection = TravelDirection.Forward;

                if (goNegative)
                {
                    goalRotation = new Vector3(0f, forwardRotation.y - 360f, 0f);
                }
                else
                {
                    goalRotation = forwardRotation;
                }
                intendedEndDirection = forwardRotation;
                break;
        }

        lastDirection = currentDirection;

        //If already turning - use nextDirection AND don't fully reset the timer
        if (isTurning)
        {
            //If it's a continued turn
            if (lastDirection != nextDirection)
            {
                timeForCurrentTurn += 90f / turnAnglePerSecond;
            }
            //If it's back
            else if (lastDirection == nextDirection)
            {
                lastRotation = lastGoalRot;
                timeForCurrentTurn = 90f / turnAnglePerSecond;
                turnTimer = timeForCurrentTurn - turnTimer;
            }

            currentDirection = nextDirection;
        }
        else
        {
            timeForCurrentTurn = 90f / turnAnglePerSecond;
        }

        goingUp = false;
        goingDown = false;

        isTurning = true;
    }

    public void DoRightTurn()
    {
        bool goSuperPositive = false;
        //If has to go into negatives
        if (goalRotation.y >= 270f)
        {
            goSuperPositive = true;
        }

        Vector3 lastGoalRot = goalRotation;

        switch (nextDirection)
        {
            case TravelDirection.Forward:
                nextDirection = TravelDirection.Right;
                if (goSuperPositive)
                {
                    goalRotation = new Vector3(0f, rightRotation.y + 360f, 0f);
                }
                else
                {
                    goalRotation = rightRotation;
                }
                intendedEndDirection = rightRotation;
                break;
            case TravelDirection.Back:
                nextDirection = TravelDirection.Left;
                if (goSuperPositive)
                {
                    goalRotation = new Vector3(0f, leftRotation.y + 360f, 0f);
                }
                else
                {
                    goalRotation = leftRotation;
                }
                intendedEndDirection = leftRotation;
                break;
            case TravelDirection.Right:
                nextDirection = TravelDirection.Back;
                if (goSuperPositive)
                {
                    goalRotation = new Vector3(0f, backRotation.y + 360f, 0f);
                }
                else
                {
                    goalRotation = backRotation;
                }
                intendedEndDirection = backRotation;
                break;
            case TravelDirection.Left:
                nextDirection = TravelDirection.Forward;
                if (goSuperPositive)
                {
                    goalRotation = new Vector3(0f, forwardRotation.y + 360f, 0f);
                }
                else
                {
                    goalRotation = forwardRotation;
                }
                intendedEndDirection = forwardRotation;
                break;
        }

        lastDirection = currentDirection;

        //If already turning - use nextDirection AND don't fully reset the timer
        if (isTurning)
        {
            //If it's a continued turn
            if (lastDirection != nextDirection)
            {
                timeForCurrentTurn += 90f / turnAnglePerSecond;
            }
            //If it's back
            else if (lastDirection == nextDirection)
            {
                lastRotation = lastGoalRot;
                timeForCurrentTurn = 90f / turnAnglePerSecond;
                turnTimer = timeForCurrentTurn - turnTimer;
            }

            currentDirection = nextDirection;
        }
        else
        {
            timeForCurrentTurn = 90f / turnAnglePerSecond;
        }

        goingUp = false;
        goingDown = false;

        isTurning = true;
    }

    public void DoDownTurn()
    {
        goalRotation = currentRotation;

        //If travelling straight ahead, down rotation is correct
        if (!goingDown && !goingUp)
        {
            goalRotation.x = downRotation.x;
            intendedEndDirection = goalRotation;
            goingDown = true;
        }
        //If already going down - opposite direction
        else if(goingDown)
        {
            switch(currentDirection)
            {
                case TravelDirection.Forward:
                    nextDirection = TravelDirection.Back;
                    goalRotation = backRotation;
                    intendedEndDirection = backRotation;
                    break;
                case TravelDirection.Back:
                    nextDirection = TravelDirection.Forward;
                    goalRotation = forwardRotation;
                    intendedEndDirection = forwardRotation;
                    break;
                case TravelDirection.Left:
                    nextDirection = TravelDirection.Right;
                    goalRotation = rightRotation;
                    intendedEndDirection = rightRotation;
                    break;
                case TravelDirection.Right:
                    nextDirection = TravelDirection.Left;
                    goalRotation = leftRotation;
                    intendedEndDirection = leftRotation;
                    break;
            }

            goingDown = false;
        }
        //If already going up, basically returns to where it was
        else if(goingUp)
        {
            switch (currentDirection)
            {
                case TravelDirection.Forward:
                    nextDirection = TravelDirection.Forward;
                    goalRotation = forwardRotation;
                    intendedEndDirection = forwardRotation;
                    break;
                case TravelDirection.Back:
                    nextDirection = TravelDirection.Back;
                    goalRotation = backRotation;
                    intendedEndDirection = backRotation;
                    break;
                case TravelDirection.Left:
                    nextDirection = TravelDirection.Left;
                    goalRotation = leftRotation;
                    intendedEndDirection = leftRotation;
                    break;
                case TravelDirection.Right:
                    nextDirection = TravelDirection.Right;
                    goalRotation = rightRotation;
                    intendedEndDirection = rightRotation;
                    break;
            }

            goingUp = false;
        }

        isTurning = true;
    }

    public void DoUpTurn()
    {
        goalRotation = currentRotation;

        //If travelling straight ahead, down rotation is correct
        if (!goingDown && !goingUp)
        {
            goalRotation.x = upRotation.x;
            intendedEndDirection = goalRotation;
            goingUp = true;
        }
        //If already going up - opposite direction
        else if (goingUp)
        {
            switch (currentDirection)
            {
                case TravelDirection.Forward:
                    nextDirection = TravelDirection.Back;
                    goalRotation = backRotation;
                    intendedEndDirection = backRotation;
                    break;
                case TravelDirection.Back:
                    nextDirection = TravelDirection.Forward;
                    goalRotation = forwardRotation;
                    intendedEndDirection = forwardRotation;
                    break;
                case TravelDirection.Left:
                    nextDirection = TravelDirection.Right;
                    goalRotation = rightRotation;
                    intendedEndDirection = rightRotation;
                    break;
                case TravelDirection.Right:
                    nextDirection = TravelDirection.Left;
                    goalRotation = leftRotation;
                    intendedEndDirection = leftRotation;
                    break;
            }

            goingUp = false;
        }
        //If already going down, basically returns to where it was
        else if (goingDown)
        {
            switch (currentDirection)
            {
                case TravelDirection.Forward:
                    nextDirection = TravelDirection.Forward;
                    goalRotation = forwardRotation;
                    intendedEndDirection = forwardRotation;
                    break;
                case TravelDirection.Back:
                    nextDirection = TravelDirection.Back;
                    goalRotation = backRotation;
                    intendedEndDirection = backRotation;
                    break;
                case TravelDirection.Left:
                    nextDirection = TravelDirection.Left;
                    goalRotation = leftRotation;
                    intendedEndDirection = leftRotation;
                    break;
                case TravelDirection.Right:
                    nextDirection = TravelDirection.Right;
                    goalRotation = rightRotation;
                    intendedEndDirection = rightRotation;
                    break;
            }

            goingDown = false;
        }

        isTurning = true;
    }
}
