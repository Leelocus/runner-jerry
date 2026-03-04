using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneActions : MonoBehaviour
{
    [SerializeField] private string targetScene;

    public void LoadTargetScene()
    {
        if (!string.IsNullOrWhiteSpace(targetScene))
        {
            SceneManager.LoadScene(targetScene);
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
