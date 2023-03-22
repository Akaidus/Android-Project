using UnityEngine;

public class HomeMenu : MonoBehaviour
{
    [SerializeField] GameObject homeMenu;
    [SerializeField] GameObject reallyQuitPromt;

    public void OpenMenu()
    {
        homeMenu.SetActive(true);
        reallyQuitPromt.SetActive(false);
    }
    
    public void ExitMenu()
    {
        homeMenu.SetActive(false);
    }
    
    public void QuitPromt()
    {
        reallyQuitPromt.SetActive(true);
    }

    public void QuitNo()
    {
        reallyQuitPromt.SetActive(false);    
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
