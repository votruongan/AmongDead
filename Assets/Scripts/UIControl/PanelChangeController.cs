using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelChangeController : MonoBehaviour
{
    public GameObject backButton;
    public int currentPanel;
    public MenuPanelControl[] MenuPanels;
    public static PanelChangeController pccInstance;
    // Start is called before the first frame update
    void Start()
    {
        pccInstance = this;
        currentPanel = 0;
    }
    public void Back()
    {
        currentPanel--;
        if (currentPanel <= 0)
        {
            currentPanel = 0;
            backButton.SetActive(false);
        }
        MenuPanels[currentPanel + 1].SetActive(false);
        MenuPanels[currentPanel].SetActive(true);
    }
    public void NextPanel()
    {
        currentPanel++;
        if (currentPanel == MenuPanels.Length)
        {
            currentPanel = MenuPanels.Length - 1;
        }
        MenuPanels[currentPanel + 1].SetActive(false);
        MenuPanels[currentPanel].SetActive(true);
        backButton.SetActive(true);
    }
    public void GotoGameSingle(int gameMode){
        ImmortalInfoHolder.AddBool("isMultiplayer", false);
        ImmortalInfoHolder.AddInt("gameMode", gameMode);
        SceneManager.LoadScene("SampleScene");
    }
    public void GotoGameMulti(int gameMode){
        
    }
    public void GotoPanel(int index)
    {
        currentPanel = index;
        backButton.SetActive(true);
        if (currentPanel >= MenuPanels.Length)
        {
            currentPanel = MenuPanels.Length - 1;
        }
        else if (currentPanel < 0)
        {
            currentPanel = 0;
            backButton.SetActive(false);
        }
        foreach (MenuPanelControl mpc in MenuPanels)
        {
            mpc.SetActive(false);
        }
        MenuPanels[currentPanel].SetActive(true);
    }
}
