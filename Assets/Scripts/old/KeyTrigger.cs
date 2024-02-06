using UnityEngine;

public class KeyTrigger : MonoBehaviour
{
    public KeyStateSO keyState;

    void OnTriggerEnter(Collider other)
    {
        keyState.CollectKey();
        Destroy(gameObject);
    }
}
