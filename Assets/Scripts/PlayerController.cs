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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    { 
		if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            LeftInput();
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            RightInput();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            UpInput();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            DownInput();
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
    }

    void RightInput()
    {
        droneMover.DoRightTurn();
    }

    void DownInput()
    {

    }

    void UpInput()
    {

    }


}
