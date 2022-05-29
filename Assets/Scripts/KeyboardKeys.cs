using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyboardKeys : MonoBehaviour
{
    void Update()
    {
        ProcessKeyboardKeys();
    }

    void ProcessKeyboardKeys()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(0);
        }
    }
}
