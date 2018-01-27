using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour {
    public string sceneName;

    public AudioClip themeMusic;
    public AudioPool audioPool;

    private void Start()
    {
        audioPool.PlayAudio(themeMusic);
    }

    private void Update()
    {

        
        
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
