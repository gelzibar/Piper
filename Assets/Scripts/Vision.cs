using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay(Collider col) {
		if(col.gameObject.layer == 8) {
			if(col.name == "Rat") {
				Vector3 direction = col.transform.position - transform.position;
				float distance = Vector3.Distance(transform.position, col.transform.position);
				LayerMask mask = 1 << 8;
				mask = ~mask;
				if(Physics.Raycast(transform.position, direction.normalized, distance, mask)) {
					Debug.Log("Vision Blocked");

				} else {
					Debug.Log("Flee in Terror!");
				}
			}

		}
	}
}
