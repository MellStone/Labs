using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public KeyStateSO keyState;
    private void Update()
    {
        if (keyState != null && keyState.IsKeyCollected())
        {
            Destroy(gameObject);
        }
    }
}
