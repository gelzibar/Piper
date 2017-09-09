using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{

    // I'm thinking that the exit would be best as just a physical object
    // This object will be aware of a "Level State Object"

    private LevelEvaluator le;

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
        le = GameObject.Find("Level Evaluator").GetComponent<LevelEvaluator>();
    }
    void OnUpdate()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.name == "Citizen")
        {
			le.CheckIfCitizenSave(col.GetInstanceID());
        }
    }
}
