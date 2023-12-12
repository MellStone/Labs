using UnityEngine;

[CreateAssetMenu(fileName = "KeyState", menuName = "ScriptableObjects/KeyStateSO", order = 1)]
public class KeyStateSO : ScriptableObject
{
    private int currentKeyCount = 0;
    private int keyCountToOpen = 2;
    public bool isKeyCollected = false;

    public void CollectKey()
    {
        currentKeyCount++;
        if (currentKeyCount > keyCountToOpen)
            isKeyCollected = true;
    }

    public bool IsKeyCollected()
    {
        return isKeyCollected;
    }
}
