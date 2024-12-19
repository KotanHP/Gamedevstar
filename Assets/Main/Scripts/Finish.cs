using UnityEngine;

public class Finish : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(other.TryGetComponent<PlayerControls>(out PlayerControls controls))
            {
                controls.controlsOn = false;
            }
            if (WinMenu.instance != null)
            {
                WinMenu.instance.Open();
            }
            else
            {
                Debug.LogWarning("Player won, but WinMenu not found");
            }
        }
    }
}
