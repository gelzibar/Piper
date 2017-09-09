using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // I need to map out the next set of behaviors to incorporate
    // Mouse Click Behavior
    // - Generate a sound
    // - Display Range
    // - Create Beacon

    // Beacon
    // - Created On Left Click
    // - Only one may be present at a time
    // - On next click, original is eliminated and replaced

    private AudioSource leftClick;
    private Influence mouseInfluence;
    private GameObject myMouse;
    private Rigidbody rbMouse;
    public GameObject pBeacon;
    private GameObject curBeacon;

    public bool isLive;

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
        leftClick = GameObject.Find("Mouse Click").GetComponent<AudioSource>();
        mouseInfluence = GameObject.Find("Mouse Influence").GetComponent<Influence>();
        myMouse = GameObject.Find("Mouse Token");
        rbMouse = myMouse.GetComponent<Rigidbody>();

        isLive = true;
    }

    void OnUpdate()
    {
        if (isLive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                leftClick.Play();
                mouseInfluence.StartVisible();
                if (curBeacon != null)
                {
                    Destroy(curBeacon);
                }
                curBeacon = Instantiate(pBeacon, rbMouse.position, rbMouse.rotation, this.transform);
                curBeacon.name = "Beacon";

            }
        }
    }

}
