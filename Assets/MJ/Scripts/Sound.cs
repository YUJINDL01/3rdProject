using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sound : MonoBehaviour
{
    public static Sound instance = null;

    private string _targetSceneName1 = "WheelMode";
    private string _targetSceneName2 = "Written_TEST";

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
    {   // 씬이 WheelMode 또는 Written_Test로 바뀌었는지 확인
        while (true)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;

            if (currentSceneName == _targetSceneName1 || currentSceneName == _targetSceneName2)
            {
                Destroy(gameObject);
                yield break; // 코루틴 종료
            }

            yield return null; // 매 프레임 대기
        }
    }
}