using UnityEngine;

public class NPCInteraction2 : MonoBehaviour
{
    [Header("Bulle PNJ (PNG avec texte)")]
    public GameObject dialogueBubble; // DialoguePNJMan

    [Header("State")]
    private bool playerInRange = false;
    private bool dialogueOpen = false;

    void Start()
    {
        // Cache la bulle au lancement
        if (dialogueBubble != null)
            dialogueBubble.SetActive(false);
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
        if (dialogueBubble != null)
            dialogueBubble.SetActive(true);

        dialogueOpen = true;
    }

    void CloseDialogue()
    {
        if (dialogueBubble != null)
            dialogueBubble.SetActive(false);

        dialogueOpen = false;
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