using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public static class EventManager
{
    public static event Action onSpecialZoneTriggered;
    public static void Fire_onSpecialZoneTriggered() { onSpecialZoneTriggered?.Invoke(); }

}
