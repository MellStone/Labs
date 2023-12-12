using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FakeLoadingSlider : MonoBehaviour
{
    public Slider loadingSlider;
    public GameObject button;
    public Button continueButton;

    private bool isLoading = false;

    void Start()
    {
        if (loadingSlider != null && continueButton != null)
        {
            continueButton.onClick.AddListener(LoadNextScene);
        }
        isLoading = true;

        SceneManager.sceneLoaded += OnSceneLoaded; // Подписываемся на событие
    }

    void Update()
    {
        if (isLoading)
        {
            loadingSlider.value += Time.deltaTime;

            if (loadingSlider.value >= loadingSlider.maxValue)
            {
                button.SetActive(true);
            }
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Обработка события загрузки сцены
        Debug.Log("Scene loaded: " + scene.name);
        // Дополнительные действия при загрузке сцены
    }

    public void LoadNextScene()
    {
        SceneManager.LoadSceneAsync("BaseScene");
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
