using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LoseMenu : MonoBehaviour
{
    [SerializeField] GameObject background;

    public static LoseMenu instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Multiple LoseMenu found");
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
}
