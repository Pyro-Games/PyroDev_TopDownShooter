using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public static class EventManager
{
    public static event Action onSpecialZoneTriggered;
    public static void Fire_onSpecialZoneTriggered() { onSpecialZoneTriggered?.Invoke(); }

    public static event Action<float, float> onPlayerHPChanged;
    public static void Fire_onPlayerHPChanged(float currentHP, float maxHP) { onPlayerHPChanged?.Invoke(currentHP, maxHP); }



    public static event Action<Vector3> onPlayerPosChanged;
    public static void Fire_onPlayerPosChanged(Vector3 worldPos) { onPlayerPosChanged?.Invoke(worldPos); }

}
