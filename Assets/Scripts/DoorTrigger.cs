using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{

    public enum TriggerState { Opens, Closes };

    public TriggerState state = TriggerState.Opens;
    public DoorController door;

    private void OnTriggerEnter(Collider other)
    {
        if (state == TriggerState.Opens)
        {
            door.OpenDoor();
        }

        if (state == TriggerState.Closes)
        {
            door.CloseDoor();
        }
    }
}
