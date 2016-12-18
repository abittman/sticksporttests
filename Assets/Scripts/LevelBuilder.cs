using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour {

    public Vector3 currentPieceScale = new Vector3(33f, 33f, 33f);

    public GameObject startPiece;

    public Transform activePieceParentPool;
    public Transform inactivePieceParentPool;

    public List<GameObject> allLevelBuildPieces = new List<GameObject>();

    List<GameObject> currentlyActiveObjects = new List<GameObject>();

    [Space]
    public GameObject currentPiece;
    public GameObject currentLeftPiece;
    public GameObject currentRightPiece;
    public GameObject currentUpPiece;
    public GameObject currentDownPiece;
    public GameObject currentForwardPiece;

	// Use this for initialization
	void Start ()
    {
        CreateLevelStart();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void CreateLevelStart()
    {
        startPiece.SetActive(true);
        currentPiece = startPiece;
        Vector3 currentPieceEuler = startPiece.transform.eulerAngles;
        Vector3 exitPos = transform.forward * currentPieceScale.z;

        GameObject leftPiece = SpawnNewPiece();
        currentlyActiveObjects.Add(leftPiece);
        currentLeftPiece = leftPiece;
        leftPiece.transform.eulerAngles = currentPieceEuler - new Vector3(0f, 90f, 0f);
        leftPiece.transform.position = currentPiece.transform.position + new Vector3(-currentPieceScale.x, 0f, currentPieceScale.z);
        leftPiece.SetActive(true);

        GameObject rightPiece = SpawnNewPiece();
        currentlyActiveObjects.Add(rightPiece);
        currentLeftPiece = rightPiece;
        rightPiece.transform.eulerAngles = currentPieceEuler + new Vector3(0f, 90f, 0f);
        rightPiece.transform.position = currentPiece.transform.position + new Vector3(currentPieceScale.x, 0f, currentPieceScale.z);
        rightPiece.SetActive(true);


        GameObject upPiece = SpawnNewPiece();
        currentlyActiveObjects.Add(upPiece);
        currentLeftPiece = upPiece;
        upPiece.transform.eulerAngles = currentPieceEuler + new Vector3(90f, 0f, 0f);
        upPiece.transform.position = currentPiece.transform.position + new Vector3(0f, currentPieceScale.y, currentPieceScale.z);
        upPiece.SetActive(true);

        GameObject downPiece = SpawnNewPiece();
        currentlyActiveObjects.Add(downPiece);
        currentLeftPiece = downPiece;
        downPiece.transform.eulerAngles = currentPieceEuler - new Vector3(90f, 0f, 0f);
        downPiece.transform.position = currentPiece.transform.position + new Vector3(0f, -currentPieceScale.y, currentPieceScale.z);
        downPiece.SetActive(true);

        GameObject forwardPiece = SpawnNewPiece();
        currentlyActiveObjects.Add(downPiece);
        currentLeftPiece = forwardPiece;
        forwardPiece.transform.eulerAngles = currentPieceEuler - new Vector3(0f, 0f, 0f);
        forwardPiece.transform.position = currentPiece.transform.position + new Vector3(0f, 0f, (currentPieceScale.z + currentPieceScale.z));
        forwardPiece.SetActive(true);
    }

    void CreateNextLevelPieces()
    {

    }

    GameObject SpawnNewPiece()
    {
        GameObject g = null;

        bool newObjectGained = false;
        do
        {
            int rand = Random.Range(0, allLevelBuildPieces.Count);

            if(currentlyActiveObjects.Exists(x => x == allLevelBuildPieces[rand]) == false)
            {
                g = allLevelBuildPieces[rand];
                newObjectGained = true;
            }

        } while (newObjectGained == false);

        return g;
    }
}
