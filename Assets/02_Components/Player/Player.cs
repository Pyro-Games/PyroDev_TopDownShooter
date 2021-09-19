using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    // public static Player Instance { get; private set; }

    [SerializeField] private int maxHP;
    [SerializeField] private int speed = 5;


    [HideInInspector] public Vector3 Pos;



    private Vector3 aimPos;

    private Transform mT;
    private Rigidbody mRB;

    private void Awake()
    {
        // Instance = this;


        mT = transform;
        mRB = GetComponent<Rigidbody>();
        mRB.centerOfMass = Vector3.zero;


        Pos = aimPos = mT.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetAimPosition();
        }
    }


    private void FixedUpdate()
    {
        mRB.position = Vector3.MoveTowards(mRB.position, aimPos, Time.fixedDeltaTime * speed);
        Pos = mRB.position;

        CamStickController.AimPos = Pos;

    }

    private void GetAimPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit, 100))
        {
            aimPos = raycastHit.point;
        }
    }

    private void IncreaseHP()
    {
        maxHP += 50;

        Debug.Log("IncreaseHP");
    }










    private void OnEnable()
    {
        EventManager.onSpecialZoneTriggered += IncreaseHP;
    }
    private void OnDisable()
    {
        EventManager.onSpecialZoneTriggered -= IncreaseHP;
    }


}
