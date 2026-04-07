using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    public GameObject playerHomme;
    public GameObject playerFemme;

    void Start()
    {
        if (CharacterSelection.selectedCharacter == 0)
        {
            playerHomme.SetActive(true);
            playerFemme.SetActive(false);
        }
        else
        {
            playerHomme.SetActive(false);
            playerFemme.SetActive(true);
        }
    }
}