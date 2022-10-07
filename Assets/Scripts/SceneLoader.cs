using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    #region Singleton
    private static SceneLoader instance;

    public static SceneLoader Instace
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SceneLoader>();
            }
            return instance;
        }
    }
    #endregion

    public static string SCENE_TO_LOAD_A = "SceneA";

    protected static float loadingProgress;

    public delegate bool onSenceLoadedDelegate(AsyncOperation asyncOp);
    public static event onSenceLoadedDelegate onSceneLoaded;

    public delegate bool onSenceUnloadedDelegate(AsyncOperation asyncOp);
    public static event onSenceUnloadedDelegate onSceneUnloaded;

    public delegate void onBeforeSenceUnloadDelegate(string sceneName);
    public static event onBeforeSenceUnloadDelegate onBeforeUnload;

    protected void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
    }

    public static void loadSceneAdditive(string scenePath)
    {
        AsyncOperation sceneLoaded = SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);
        if (sceneLoaded != null)
        {
            sceneLoaded.completed += onLoaded;
            loadingProgress = sceneLoaded.progress;
        }
    }

    protected static void onLoaded(AsyncOperation asyncOp)
    {
        asyncOp.completed -= onLoaded; 
        if (onSceneLoaded != null)
        {
            onSceneLoaded(asyncOp);
        }
    }

    public static void unloadScene(string sceneName)
    {
        onBeforeUnload(sceneName);

        AsyncOperation sceneUnloaded = SceneManager.UnloadSceneAsync(sceneName);
        if (sceneUnloaded != null)
        {
            sceneUnloaded.completed += onUnloaded;
        }
    }

    protected static void onUnloaded(AsyncOperation asyncOp)
    {
        asyncOp.completed -= onUnloaded;
        if (onSceneUnloaded != null)
        {
            onSceneUnloaded(asyncOp);
        }
    }


    protected virtual void Start()
    {
        loadSceneAdditive(SCENE_TO_LOAD_A);
    }

    protected virtual void OnDestroy()
    {
        unloadScene(SCENE_TO_LOAD_A);
    }
}
