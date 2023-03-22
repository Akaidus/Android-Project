using UnityEngine;

public class SkinsPanel : MonoBehaviour
{
    [SerializeField] GameObject panel;

    public void OpenPanel()
    {
        panel.SetActive(true);
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
    }
}
