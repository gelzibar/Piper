using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    protected Rigidbody myRigidbody;
    protected Vector3 target;

    protected bool hasEscaped;
    protected bool isIdle, isResting;
    protected bool hasTarget;

    protected float curIdle, maxIdle;

    protected virtual void OnStart()
    {
        myRigidbody = GetComponent<Rigidbody>();
        hasEscaped = false;

        isIdle = true;
        isResting = true;
        hasTarget = false;
        curIdle = 0.0f;
    }
    protected virtual void OnFixedUpdate()
    {
        if (hasTarget)
        {
            float speed = 2.0f;
            Vector3 newDir = Vector3.MoveTowards(myRigidbody.position, target, speed * Time.fixedDeltaTime);
            /* myRigidbody.rotation = Quaternion.LookRotation(newDir); */
            myRigidbody.position = newDir;
        }
    }
    protected virtual void OnUpdate()
    {

        // hasTarget and isIdle
        // isIdle is evaluated whether some outside force has impacted the Citizen.
        // if the citizen has been issued a player command, then it HAS A TARGET and IS NOT IDLE
        // once it reaches its command target, then it has NO TARGET and IS IDLE
        // ResetTarget();
        if (!isResting)
        {
            if (Vector3.Distance(myRigidbody.position, target) <= 0.5f || curIdle > maxIdle)
            {
                int randomChoice = Random.Range(0, 3);
                if (randomChoice <= 1)
                {
                    SetTarget(false);
                    SetIdle(true);
                }
                else if (randomChoice > 1)
                {
                    SetTarget(false);
                    SetIdle(true);
                    isResting = true;
                }
            }
        }
        else if (isResting)
        {
            if (curIdle > maxIdle)
            {
                int randomChoice = Random.Range(0, 3);
                if (randomChoice > 1)
                {
                    SetTarget(false);
                    SetIdle(true);
                }
                else if (randomChoice <= 1)
                {
                    SetTarget(false);
                    SetIdle(true);
                    isResting = true;
                }
            }
        }

        curIdle += Time.deltaTime;

        if (!hasTarget && isIdle)
        {
            WanderTarget();
        }
    }

    protected void ResetIdle()
    {
        isIdle = false;
        curIdle = 0.0f;
    }
    protected void SetIdle(bool value)
    {
        isIdle = value;
        curIdle = 0.0f;

        if (!value)
        {
            isResting = false;
        }
    }

    protected void SetTarget(bool value)
    {
        hasTarget = value;
    }

    protected void WanderTarget()
    {
        Vector3 tempVec = new Vector3();
        do
        {
            tempVec = Random.insideUnitCircle * 3;
        } while (CheckMinDistance(tempVec));
        // insideUnitCircle assigns to x, y-- swap to x and z
        tempVec = new Vector3(myRigidbody.position.x + tempVec.x, 0, myRigidbody.position.z + tempVec.y);
        target = tempVec;
        hasTarget = true;
    }

    protected bool CheckMinDistance(Vector3 dest)
    {
        return Vector3.Distance(myRigidbody.position, dest) <= 0.5f;
    }

    protected void OnTriggerEnter(Collider col)
    {
        if (name != "Vision Trigger")
        {
            if (col.name == "Mouse Influence" && !hasEscaped)
            {
                Debug.Log("Mouse Influence");
                Vector3 tempVec = col.gameObject.GetComponent<Rigidbody>().position;
                target = new Vector3(tempVec.x, tempVec.y, tempVec.z);
                ResetIdle();
                SetTarget(true);
            }
            else if (col.name == "Exit Beacon")
            {
                Debug.Log("Exit Beacon");
                hasEscaped = true;
                myRigidbody.useGravity = false;
                ResetIdle();
                SetTarget(true);

                Vector3 tempVec = col.gameObject.transform.position;
                tempVec = new Vector3(tempVec.x, tempVec.y + 10, tempVec.z);
                target = new Vector3(tempVec.x, tempVec.y, tempVec.z);
                Destroy(gameObject, 1.0f);
            }
        }
    }
}