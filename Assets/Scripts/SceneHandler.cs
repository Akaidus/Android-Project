using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] int sceneToLoad;

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
