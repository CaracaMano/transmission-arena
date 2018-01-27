using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour {
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("Arena");
        }
    }
}
