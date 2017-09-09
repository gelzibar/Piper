using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEvaluator : MonoBehaviour
{

    // What is Level State looking for for a win condition?
    // - A min number of entities that has reached the exit
    // - FAILURE: There are not enough surviving entities to successfully complete the level
    // - Possible a level timer. If not a hard timer, then a soft timer that could be attached to a ranking bonus.

    // What happens on win condition
    // Player loses control available on the board
    // Board is grayed out to indicate inactivity
    // Stats screen is posted: Display "You Win" "Citizens Save X/Y" and three buttons "Exit" "Restart" "Continue"

    private Mouse myMouse;
    private PlayerController myPlayerController;
    public int citizensSavedToWin;
    public int curSavedCitizens;
    public List<int> savedCitizenID;

    public bool isComplete;
    public bool isLive;

	public float matchTime;

    void Start()
    {
        OnStart();
    }

    void Update()
    {
        OnUpdate();
    }

    void OnStart()
    {
        myMouse = GameObject.Find("Mouse Token").GetComponent<Mouse>();
        myPlayerController = GameObject.Find("Player Controller").GetComponent<PlayerController>();

        savedCitizenID = new List<int>();
        curSavedCitizens = 0;
        isComplete = false;
        isLive = true;

		matchTime = 0.0f;
    }
    void OnUpdate()
    {
        if (curSavedCitizens >= citizensSavedToWin)
        {
            isComplete = true;
            isLive = false;
            myMouse.isLive = false;
            myPlayerController.isLive = false;
        } else {
			matchTime += Time.deltaTime;
		}
    }

    public int GetCurSaved()
    {
        return curSavedCitizens;
    }

    public void CheckIfCitizenSave(int instanceID)
    {
        bool idPresent = false;
        foreach (int entry in savedCitizenID)
        {
            if (entry == instanceID)
            {
                idPresent = true;
            }
        }

        if (!idPresent)
        {
            curSavedCitizens++;
            savedCitizenID.Add(instanceID);
        }
    }
}
