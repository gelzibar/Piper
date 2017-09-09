using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Influence : MonoBehaviour
{

    private GameObject myMouse;
    private Rigidbody myRigidbody;
    private MeshRenderer myMeshRenderer;
    private SphereCollider mySphereCollider;

    private Vector3 linkedPos;
    private float maxVisible, curVisible;
    private bool isVisible;

    void Start()
    {
        OnStart();
    }

    void FixedUpdate()
    {
        OnFixedUpdate();
    }

    void Update()
    {
        OnUpdate();
    }

    void OnStart()
    {
        myMouse = transform.parent.gameObject;
        myRigidbody = GetComponent<Rigidbody>();
        LinkPositiontoMouse();

        myMeshRenderer = GetComponent<MeshRenderer>();
        maxVisible = 0.1f;
        curVisible = 0.0f;
        isVisible = false;

        mySphereCollider = GetComponent<SphereCollider>();
        myMeshRenderer.enabled = false;
        mySphereCollider.enabled = false;

    }

    void OnFixedUpdate()
    {
        myRigidbody.MovePosition(linkedPos);

    }

    void OnUpdate()
    {
        LinkPositiontoMouse();
        ManageVisible();

    }

    void LinkPositiontoMouse()
    {
        Vector3 tempVec = myMouse.GetComponent<Rigidbody>().position;
        linkedPos = new Vector3(tempVec.x, tempVec.y, tempVec.z);
    }

    void ManageVisible()
    {
        if (isVisible)
        {
            if (curVisible < maxVisible)
            {
                curVisible += Time.deltaTime;
            }
            else if (curVisible > maxVisible)
            {
                isVisible = false;
                curVisible = 0.0f;
                myMeshRenderer.enabled = false;
                mySphereCollider.enabled = false;
            }
        }
    }

    public void StartVisible()
    {
        if (!isVisible)
        {
            curVisible = 0.0f;
            isVisible = true;
            myMeshRenderer.enabled = true;
            mySphereCollider.enabled = true;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.name == "Citizen")
        {
            //Debug.Log("Citizen " + col.GetInstanceID() + " detected.");
        }
    }
}
