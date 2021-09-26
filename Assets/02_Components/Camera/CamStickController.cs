using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamStickController : MonoBehaviour
{
    public static Vector3 AimPos;
    private Vector3 curPos;

    private Transform mT;

    private void Awake()
    {
        mT = transform;
        curPos = mT.position;
    }

    // private void Update()
    // {
    //     Time.fixedDeltaTime = Time.deltaTime;
    // }


    private void LateUpdate()
    {
        curPos = Vector3.Lerp(curPos, AimPos, Time.deltaTime * 3);

        mT.position = curPos;
    }

    private void FixedUpdate()
    {
        curPos = Vector3.Lerp(curPos, AimPos, Time.fixedDeltaTime * 3);

        mT.position = curPos;
    }

}
