using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour, IAttackable
{
    [SerializeField] private bool isMe;

    [SerializeField] private float maxHP;
    private float currentHP;

    [SerializeField] private int speed = 5;
    [SerializeField] Transform ragdollParent;

    [SerializeField] private Transform headerPos;


    [HideInInspector] public Vector3 Pos;

    public bool isDeath { get; private set; }



    private Vector3 aimPos;

    private Transform mT;
    private Rigidbody mRB;
    private Animator mAnimator;

    private int groundLayer, attackLayer, damageLayer;

    private void Awake()
    {
        // Instance = this;

        Debug.Log("Player Awake");

        mT = transform;
        mRB = GetComponent<Rigidbody>();
        mAnimator = GetComponent<Animator>();


        mRB.centerOfMass = Vector3.zero;


        Pos = aimPos = mT.position;

        currentHP = 0;

        groundLayer = LayerMask.GetMask(Layers.Ground);
        attackLayer = LayerMask.GetMask(Layers.AttackLayer);
        damageLayer = LayerMask.GetMask(Layers.DamageCollider);


        ragdollCollider = ragdollParent.GetComponentsInChildren<Collider>();
        rigidbodies = ragdollParent.GetComponentsInChildren<Rigidbody>();
        SetRagdoll(false);

    }

    private void Start()
    {
        EventManager.Fire_onPlayerHPChanged(currentHP, maxHP);
    }

    private void Update()
    {


        if (!isMe) return;

        if (Input.GetMouseButtonDown(0))
        {
            GetAimPosition();
        }
    }

    Vector3 lookAtPos;
    private void FixedUpdate()
    {
        if (isDeath) return;

        mRB.position = Vector3.MoveTowards(mRB.position, aimPos, Time.fixedDeltaTime * speed);
        Pos = mRB.position;

        if (lookAtPos != Vector3.zero)
            mRB.rotation = Quaternion.Slerp(mRB.rotation, Quaternion.LookRotation(lookAtPos), Time.fixedDeltaTime * 20);


        if (Vector3.SqrMagnitude(aimPos - Pos) > 0.1f * 0.1f)
            mAnimator.SetFloat(AnimParameters.velTotal, 1f, 0.2f, Time.fixedDeltaTime * 2);
        else
            mAnimator.SetFloat(AnimParameters.velTotal, 0f, 0.2f, Time.fixedDeltaTime * 2);



        if (!isMe) return;

        EventManager.Fire_onPlayerPosChanged(headerPos.position);
        CamStickController.AimPos = Pos;


    }

    private void GetAimPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit, 100, damageLayer, QueryTriggerInteraction.Collide))
        {
            Attack(raycastHit.point);
            AttackDefiniton attackDef = new AttackDefiniton();
            attackDef.attacker = this;
            // attackDef.target = raycastHit.transform.GetComponentInParent<IDamagable>();
            attackDef.damagePoint = 20;

            raycastHit.transform.GetComponentInParent<IDamagable>().GetDamage(attackDef);

            // GiveDamage(attackDef);
            return;
        }

        if (Physics.Raycast(ray, out raycastHit, 100, attackLayer, QueryTriggerInteraction.Collide))
        {
            Attack(raycastHit.point);

            return;
        }
        if (Physics.Raycast(ray, out raycastHit, 100, groundLayer, QueryTriggerInteraction.Collide))
        {
            aimPos = raycastHit.point;
            lookAtPos = aimPos - Pos;
            lookAtPos.y = 0;

            return;
        }
    }

    private void Attack(Vector3 pos)
    {
        lookAtPos = pos - Pos;
        lookAtPos.y = 0;

        mAnimator.SetTrigger(AnimParameters.attack);

    }




    private void IncreaseHP()
    {
        maxHP += 50;
        currentHP += 50;

        EventManager.Fire_onPlayerHPChanged(currentHP, maxHP);

        Debug.Log("IncreaseHP");
    }



    // public void GiveDamage(AttackDefiniton attackDefiniton)
    // {
    //     attackDefiniton.target.GetDamage(attackDefiniton);
    // }

    public void GetDamage(AttackDefiniton attackDefiniton)
    {
        Debug.Log(gameObject.name + " GetDamage");

        currentHP -= attackDefiniton.damagePoint;

        if (currentHP <= 0)
        {
            isDeath = true;

            mAnimator.CrossFade(AnimParameters.Die, 0.2f);

            Debug.Log(gameObject.name + " death");
        }
    }



    Collider[] ragdollCollider;
    Rigidbody[] rigidbodies;
    public void SetRagdoll(bool state)
    {
        for (int i = 0; i < ragdollCollider.Length; i++)
            ragdollCollider[i].isTrigger = !state;

        for (int i = 0; i < rigidbodies.Length; i++)
            rigidbodies[i].isKinematic = !state;

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
