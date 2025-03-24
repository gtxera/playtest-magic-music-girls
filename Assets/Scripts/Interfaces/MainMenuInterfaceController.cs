using UnityEngine;
using UnityEngine.UI;

public class MainMenuInterfaceController : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField] GameObject mainMenuScreen;
    [SerializeField] GameObject settingScreen;

    [Header("Settings variables")]
    [SerializeField] GameObject audioPanel;
    [SerializeField] GameObject controlsPanel;
    [SerializeField] GameObject othersPanel;
    [SerializeField] Button audioBtn; 
    [SerializeField] Button controlsBtn; 
    [SerializeField] Button othersBtn;

    public void ChangeScreen(int index)
    {
        mainMenuScreen.SetActive(false);
        settingScreen.SetActive(false);

        switch (index)
        {
            case 0:
                mainMenuScreen.SetActive(true); 
                break;
            case 1:
                settingScreen.SetActive(true);
                ChangeSettingPanel(0);
                break;
        }
    }

    public void ChangeSettingPanel(int index)
    {
        audioPanel.SetActive(index == 0);
        audioBtn.interactable = index != 0;
        controlsPanel.SetActive(index == 1);
        controlsBtn.interactable = index != 1;
        othersPanel.SetActive(index == 2);
        othersBtn.interactable = index != 2;
    }
}
