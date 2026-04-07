using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [Header("Bulle PNJ")]
    public GameObject dialogueBubble;

    [Header("Epées")]
    public GameObject swordMan;
    public GameObject swordWoman;

    [Header("Teleport Cave")]
    public GameObject teleportCave;

    [Header("State")]
    private bool playerInRange = false;
    private bool hasGivenSword = false;
    private bool dialogueOpen = false;

    void Start()
    {
        // Cache la bulle au lancement
        if (dialogueBubble != null)
            dialogueBubble.SetActive(false);

        // Cache les épées
        if (swordMan != null)
            swordMan.SetActive(false);

        if (swordWoman != null)
            swordWoman.SetActive(false);

        // Bloque la cave
        if (teleportCave != null)
            teleportCave.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!dialogueOpen)
                Interact();
            else
                CloseDialogue();
        }
    }

    void Interact()
    {
        // Affiche la bulle
        if (dialogueBubble != null)
            dialogueBubble.SetActive(true);

        dialogueOpen = true;

        // Donne l’épée + débloque cave UNE SEULE FOIS
        if (!hasGivenSword)
        {
            GiveSwordAndUnlock();
            hasGivenSword = true;
        }
    }

    void CloseDialogue()
    {
        if (dialogueBubble != null)
            dialogueBubble.SetActive(false);

        dialogueOpen = false;
    }

    void GiveSwordAndUnlock()
    {
        // ⚔️ Active les épées
        if (swordMan != null)
            swordMan.SetActive(true);

        if (swordWoman != null)
            swordWoman.SetActive(true);

        // 🕳️ Active la cave
        if (teleportCave != null)
            teleportCave.SetActive(true);

        Debug.Log("Épée donnée + Cave débloquée !");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInRange = false;
    }
}