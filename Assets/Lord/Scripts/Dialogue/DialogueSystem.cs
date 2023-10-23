using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private List<Dialogue> dialogues = new();
    [SerializeField] private float textSpeed = 0.2f;
    [SerializeField] private TMPro.TMP_Text dialogueTextBox;
    private Dialogue currentDialogue;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            UiManager.Instance.OpenPanel("dialogue");
            PlayDialogue("start");
        }
    }

    public void PlayDialogue(string id)
    {
        foreach (Dialogue dialogue in dialogues)
        {
            if (dialogue.Id == id)
            {
                currentDialogue = dialogue;
                break;
            }
        }
        nextDialogue();
    }

    private void nextDialogue()
    {
        if (currentDialogue.currentDialogueIndex <= currentDialogue.Texts.Count - 1)
        {
            dialogueTextBox.text = "";
            StartCoroutine(WriteSentence());
        }
        else
        {
            currentDialogue.currentDialogueIndex = 0;
            UiManager.Instance.ClosePanel("dialogue");
        }
    }

    IEnumerator WriteSentence()
    {
        foreach (char Character in currentDialogue.Texts[currentDialogue.currentDialogueIndex].ToCharArray())
        {
            dialogueTextBox.text += Character;
            // SoundManager.Instance.PlayEffect("type");
            yield return new WaitForSeconds(textSpeed);
        }
        currentDialogue.currentDialogueIndex++;
    }
}

[System.Serializable]
public class Dialogue
{
    public string Id;
    public int currentDialogueIndex = 0;
    public List<string> Texts;
}