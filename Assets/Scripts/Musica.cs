using UnityEngine;
using UnityEngine.SceneManagement;
public class Musica : MonoBehaviour
{
    private AudioSource _audioSource;
    private void Awake()
    {
        if (esEscena())
        {
            DontDestroyOnLoad(transform.gameObject);
            _audioSource = GetComponent<AudioSource>();
            getEscena();
        }
    }
    void Update()
    {
        getEscena();
    }
    public void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }

    void getEscena()
    {
        if (esEscena())
        {
            GameObject.FindGameObjectWithTag("Music").GetComponent<Musica>().PlayMusic();
        }
        else
        {
            GameObject.FindGameObjectWithTag("Music").GetComponent<Musica>().StopMusic();
        }
    }

    bool esEscena()
    {
        return ((SceneManager.GetActiveScene() != SceneManager.GetSceneByName("MainMenu")) &&
        (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Victoria")) &&
        (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Derrota")));
    }
}