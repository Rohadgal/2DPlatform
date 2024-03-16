using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event EventDelegate MyEvent;

    // Método para invocar el evento
    public static void RaiseEvent() {
        if (MyEvent != null) {
            MyEvent(); // Invocar el evento
        }
    }
}
