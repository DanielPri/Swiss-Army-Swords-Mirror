using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{
    private AudioSource[] audioSources;
    Scene scene;
    public bool mainMenu;
    public bool cutscene;
    bool level1;
    bool level2;
    bool level3;
    bool finalBoss;
    static Music prefab;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (prefab == null)
        {
            prefab = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSources = GetComponents<AudioSource>();
        mainMenu = false;
        cutscene = false;
        level1 = false;
        level2 = false;
        level3 = false;
        finalBoss = false;
    }

    void Update()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.name == "MainMenu" && !mainMenu)
        {
            audioSources[0].Play();
            audioSources[1].Stop();
            audioSources[2].Stop();
            audioSources[3].Stop();
            mainMenu = true;
            level1 = false;
            level2 = false;
            level3 = false;
            finalBoss = false;
        }
        if (scene.name == "Cutscene" && !cutscene)
        {
            audioSources[0].Stop();
            audioSources[4].Play();
            cutscene = true;
        }
        if (scene.name == "Level 1" && !level1)
        {
            audioSources[4].Stop();
            audioSources[1].Play();
            level1 = true;
        }
        if (scene.name == "Level 2" && !level2)
        {
            audioSources[1].Stop();
            audioSources[2].Play();
            level2 = true;
        }
        if (scene.name == "Level 3" && !level3)
        {
            audioSources[2].Stop();
            audioSources[3].Play();
            level3 = true;
        }
        if (scene.name == "DragonBoss" && !finalBoss)
        {
            audioSources[3].Stop();
            finalBoss = true;
        }
    }
}