using System.Collections;
using UnityEngine;
using Tymski;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SceneReference _arena;
    [SerializeField] private SceneReference _mainMenu;

    private static SceneLoader _instance;

    public static SceneLoader Instance
    {
        get
        {
            if(_instance != null)
            {
                return _instance;
            }
            else
            {
                Debug.LogError($"Scene loader instance is null");
                return null;
            }
        }
        private set
        {
            if (_instance == null)
            {
                _instance = value;
            }
        }
    }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadArena()
    {
        StartCoroutine(LoadSceneAsync(_arena));
    }

    private IEnumerator LoadSceneAsync(SceneReference targetScene)
    {
        var sceneLoading = SceneManager.LoadSceneAsync(targetScene);

        sceneLoading.allowSceneActivation = false;

        while (sceneLoading.progress < 0.9f)
        {
            yield return null;
        }

        sceneLoading.allowSceneActivation = true;
        yield return null;
    }
}
