using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCustomManager : MonoBehaviour
{
    [SerializeField] private string firstAdditionalSceneName;
    [SerializeField] private string secondAdditionalSceneName;

    private bool isFirstSceneLoad = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isFirstSceneLoad)
            {
                UnloadScene(secondAdditionalSceneName);
                LoadScene(firstAdditionalSceneName);
            }
            else
            {
                UnloadScene(firstAdditionalSceneName);
                LoadScene(secondAdditionalSceneName);
            }

            isFirstSceneLoad = !isFirstSceneLoad; // Toggle the boolean value
        }
    }

    void LoadScene(string sceneName)
    {
        if (!SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
    }

    void UnloadScene(string sceneName)
    {
        if (SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}
