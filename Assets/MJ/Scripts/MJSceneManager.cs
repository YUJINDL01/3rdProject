using UnityEngine;
using UnityEngine.SceneManagement;

public class MJSceneManager : MonoBehaviour
{
    public void SStart()
    {
        SceneManager.LoadScene("Start");
    }

    public void Written()
    {
        SceneManager.LoadScene("Written_RoomCreate");
    }

    public void Control()
    {
        SceneManager.LoadScene("TwoVersionScene");
    }

    public void AIto()
    {
        SceneManager.LoadScene("AIMap");
    }

public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();

#endif
    }
}
