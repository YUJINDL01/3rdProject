using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sound : MonoBehaviour
{
    public static Sound instance = null;

    private string _targetSceneName = "WheelMode";

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return; 
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject);
        
        StartCoroutine(CheckSceneCoroutine());
    }

    private IEnumerator CheckSceneCoroutine()
    {
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == _targetSceneName);
        Destroy(gameObject);
    }
}