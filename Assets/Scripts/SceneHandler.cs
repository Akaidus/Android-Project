using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] int sceneToLoad;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
