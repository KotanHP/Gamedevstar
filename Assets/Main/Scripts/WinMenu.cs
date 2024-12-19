using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    [SerializeField] private GameObject background;

    public static WinMenu instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Multiple WinMenu found");
        }
        instance = this;
    }

    public void Open()
    {
        background.SetActive(true);
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
