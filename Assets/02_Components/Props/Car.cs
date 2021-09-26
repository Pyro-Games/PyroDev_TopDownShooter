using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour, IDamagable
{

    public void GetDamage(AttackDefiniton attackDefiniton)
    {
        Debug.Log("Car explode");
    }

}
