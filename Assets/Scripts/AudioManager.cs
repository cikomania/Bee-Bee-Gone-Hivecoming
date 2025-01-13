using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("-Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] public AudioSource moveSource;
    [SerializeField] public AudioSource rainSource;

    [Header("-Audio Clip")]
    public AudioClip background;
    public AudioClip score;
    public AudioClip death;
    public AudioClip rain;
    public AudioClip thunder;
    public AudioClip enemyGetsHit;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (!SFXSource.enabled)
        {
            SFXSource.enabled = true;
        }
        SFXSource.PlayOneShot(clip);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!musicSource.enabled)
        {
            musicSource.enabled = true;
        }

        if (moveSource.isPlaying)
        {
            moveSource.Stop();
        }

        if (scene.buildIndex == 7 || scene.buildIndex == 9 || scene.buildIndex == 12)
        {
            PlaySFX(thunder);
            if (!rainSource.isPlaying)
            {
                if (!rainSource.enabled)
                {
                    rainSource.enabled = true;
                }
                rainSource.Play();
            }
        }
        else
        {
            if (rainSource.isPlaying)
            {
                rainSource.Stop();
            }
        }

        if (scene.name == "Main Menu")
        {            
            moveSource.Stop();
        }

        else if (scene.name == "Game Over" || scene.name == "Boss Fight Cutscene" || scene.name == "Ending Cutscene")
        {
            musicSource.Stop();
            moveSource.Stop();
            if (rainSource.isPlaying)
            {
                rainSource.Stop();
            }
        }

        else if (scene.name == "Boss Fight")
        {
            if (musicSource.isPlaying)
            {
                musicSource.Stop();
            }
            
            if (rainSource.isPlaying)
            {
                rainSource.Stop();
            }
        }
        else
        {
            if (!musicSource.isPlaying)
            {
                musicSource.Play();
            }
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
