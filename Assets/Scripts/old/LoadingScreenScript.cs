using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LoadingScreenScript : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextSceneName;

    void OnTriggerEnter()
    {
        //StartCoroutine(PlayVideo());

        LoadNextScene();
    }


    void LoadNextScene()
    {
        SceneManager.LoadSceneAsync(nextSceneName);
    }
}
