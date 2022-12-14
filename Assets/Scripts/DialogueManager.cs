using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private GameObject backPanel;
    private GameObject dialogueTextObject;
    private TMP_Text dialogueText;
    private GameObject saveButton;
    private TMP_Text saveButtonText;
    private GameObject doomButton;
    private TMP_Text doomButtonText;

    private string[] currentDialogue;
    private int currentIndex;

    private NavigationManager activeVictimNav;
    [SerializeField]
    private Transform exit;
    [SerializeField]
    private Transform monster;
   

    // Start is called before the first frame update
    void Start()
    {
        exit = GameObject.FindGameObjectWithTag("Exit").transform;
        backPanel = transform.Find("Panel").gameObject;
        backPanel.SetActive(false);
        dialogueTextObject = transform.Find("DialogueText").gameObject;
        dialogueText = dialogueTextObject.GetComponent<TMP_Text>();
        dialogueTextObject.SetActive(false);
        saveButton = transform.Find("SaveButton").gameObject;
        saveButtonText = saveButton.transform.Find("SaveButtonText").gameObject.GetComponent<TMP_Text>();
        saveButton.SetActive(false);
        doomButton = transform.Find("DoomButton").gameObject;
        doomButtonText = doomButton.transform.Find("DoomButtonText").gameObject.GetComponent<TMP_Text>();
        doomButton.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && PlayerWorldInteractions.inDialogue && currentIndex < currentDialogue.Length-2)
        {
            AdvanceDialogue();
        }
    }

    public void SetDialogue(string[] dialogueArray,NavigationManager active)
    {
        currentIndex = 0;
        currentDialogue = dialogueArray;
        backPanel.SetActive(true);
        saveButton.SetActive(false);
        doomButton.SetActive(false);
        dialogueTextObject.SetActive(true);
        dialogueText.text = currentDialogue[0];
        PlayerWorldInteractions.inDialogue = true;
        this.activeVictimNav = active;
        print("Set active");
    }

    public void AdvanceDialogue()
    {
        currentIndex++;
        if (currentIndex >= currentDialogue.Length-2)
        {
            dialogueText.text = "";
            dialogueTextObject.SetActive(false);
            saveButton.SetActive(true);
            saveButtonText.SetText(currentDialogue[currentIndex]);
            doomButton.SetActive(true);
            doomButtonText.SetText(currentDialogue[currentIndex + 1]);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            dialogueText.text = currentDialogue[currentIndex];
        }
    }

    public void EndDialogue()
    {
        backPanel.SetActive(false);
        saveButton.SetActive(false);
        doomButton.SetActive(false);
        PlayerWorldInteractions.inDialogue = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SaveChosen()
    {
        EndDialogue();
        print("Do we still know active?:" + (activeVictimNav != null).ToString());
        activeVictimNav.Target(exit.transform.position);
        activeVictimNav.GetComponent<VictimBehavior>().onWay = true;
    }

    public void DoomChosen()
    {
        EndDialogue();
        activeVictimNav.Target(monster.position);
        activeVictimNav.GetComponent<VictimBehavior>().onWay = true;
    }



}