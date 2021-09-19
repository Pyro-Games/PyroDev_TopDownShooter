using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player))
        {
            // other.GetComponentInParent<Player>()?.IncreaseHP();


            // Player.Instance.IncreaseHP();

            EventManager.Fire_onSpecialZoneTriggered();

        }
    }
}
