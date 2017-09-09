using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : Unit
{

    void Start()
    {
		OnStart();
    }

	void FixedUpdate() {
		OnFixedUpdate();
	}
    void Update()
    {
		OnUpdate();
    }

    protected override void OnStart() { 
		base.OnStart();
		maxIdle = 1.0f;
	}
    protected override void OnFixedUpdate() { 
		base.OnFixedUpdate();
	}
    protected override void OnUpdate() { 
		base.OnUpdate();
	}
}
