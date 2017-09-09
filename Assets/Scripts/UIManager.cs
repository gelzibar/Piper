using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	private GameObject uiActive, uiFinal;
	private Text citizenTracker, finalTracker;
	private Text finalTime, matchTime;
	private LevelEvaluator le;

	private int totalCitizens;

	void Start () {
		OnStart();
	}
	
	void Update () {
		OnUpdate();
	}

	void OnStart() {
		citizenTracker = GameObject.Find("Citizen Tracker").GetComponent<Text>();
		finalTracker = GameObject.Find("Final Tracker").GetComponent<Text>();
		finalTime = GameObject.Find("Final Time").GetComponent<Text>();
		matchTime = GameObject.Find("Match Time").GetComponent<Text>();
		le = GameObject.Find("Level Evaluator").GetComponent<LevelEvaluator>();

		uiActive = GameObject.Find("Active Screen");
		uiFinal = GameObject.Find("Final Screen");
		uiFinal.SetActive(false);
		totalCitizens = CalculateTotalCitizens();
	}

	void OnUpdate() {
		citizenTracker.text = BuildCitizenTrackerString();
		finalTracker.text = BuildCitizenTrackerString();
		matchTime.text = BuildMatchTime();
		finalTime.text = BuildMatchTime();
		if(le.isComplete) {
			uiActive.SetActive(false);
			uiFinal.SetActive(true);
		}
	}

	string BuildCitizenTrackerString() {
		string completeString = "";
		string title = "Citizens Saved: ";
		string numerator = le.curSavedCitizens + " / ";
		string denom = totalCitizens.ToString();
		completeString = title + numerator + denom;
		return completeString;
	}

	string BuildMatchTime() {
		string complete = "";
		string title = "Time: ";
		string value = le.matchTime.ToString("F2");
		complete = title + value;
		return complete;
	}

	int CalculateTotalCitizens() {
		Object[] citizenObjects = GameObject.FindObjectsOfType(typeof(Citizen));
		return citizenObjects.Length;
	}
}
