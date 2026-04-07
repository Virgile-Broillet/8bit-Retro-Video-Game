using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject characterMenu;
    public GameObject optionsMenu;
    public GameObject creditsMenu;

    public void OpenCharacterMenu()
    {
        mainMenu.SetActive(false);
        characterMenu.SetActive(true);
    }

    public void OpenOptions()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void OpenCredits()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void BackToMain()
    {
        mainMenu.SetActive(true);
        characterMenu.SetActive(false);
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }

    public void SelectHomme()
    {
        CharacterSelection.selectedCharacter = 0;
        SceneManager.LoadScene("MaisonInterieur");
    }

    public void SelectFemme()
    {
        CharacterSelection.selectedCharacter = 1;
        SceneManager.LoadScene("MaisonInterieur");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game"); // utile pour tester dans Unity
        Application.Quit();
    }
}