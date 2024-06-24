using System.Collections;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Windows;

public class NPCNoMove : MonoBehaviour, Interactive
{
    [SerializeField] private GameManagerSO gameManager;

    [Header("Dialogue System")]
    [SerializeField, TextArea(1,5)] private string[] dialogueMission;
    [SerializeField, TextArea(1, 5)] private string[] dialogueResult;
    [SerializeField] private float timeBetweenLetters;
    [SerializeField] private GameObject dialogueFrame;
    [SerializeField] private TextMeshProUGUI dialogueText;
    private bool speaking = false;
    private int dialogueCurrentIndex = -1;

    #region Dialogue Methods
    private IEnumerator WriteSentenceMission()
    {
        speaking = true;
        dialogueText.text = string.Empty;

        char[] sentenceCharacters = dialogueMission[dialogueCurrentIndex].ToCharArray();

        foreach (char character in sentenceCharacters)
        {
            dialogueText.text += character;
            yield return new WaitForSeconds(timeBetweenLetters);
        }
        speaking = false;
    }
    private IEnumerator WriteResultSentence()
    {
        speaking = true;
        dialogueText.text = string.Empty;

        char[] sentenceCharacters = dialogueResult[dialogueCurrentIndex].ToCharArray();

        foreach (char character in sentenceCharacters)
        {
            dialogueText.text += character;
            yield return new WaitForSeconds(timeBetweenLetters);
        }
        speaking = false;
    }
    public void Interact()
    {
        gameManager.ChangePlayerState(true);
        dialogueFrame.SetActive(true);

        if (!speaking)
        {
            if (!gameManager.HasMando)
            {
                FollowingMissionSentence();
            }
            else
            {
                FollowingResultSentence();
            }
        }
        else
        {
            if (!gameManager.HasMando)
            {
                CompleteMissionSentence();
            }
            else
            {
                CompleteResultSentence();
            }
            
        }
    }

    private void FollowingMissionSentence()
    {
        dialogueCurrentIndex++;
        if(dialogueCurrentIndex >= dialogueMission.Length)
        {
            FinishDialogue();
        }
        else
        {
            StartCoroutine(WriteSentenceMission());
        }
    }
    private void FollowingResultSentence()
    {
        dialogueCurrentIndex++;
        if (dialogueCurrentIndex >= dialogueResult.Length)
        {
            FinishDialogue();
            gameManager.IsEnded= true;
        }
        else
        {
            StartCoroutine(WriteResultSentence());
        }
    }
    private void CompleteMissionSentence()
    {
        StopAllCoroutines();
        dialogueText.text = dialogueMission[dialogueCurrentIndex];
        speaking = false;
    }
    private void CompleteResultSentence()
    {
        StopAllCoroutines();
        dialogueText.text = dialogueResult[dialogueCurrentIndex];
        speaking = false;
    }
    private void FinishDialogue()
    {       
        dialogueText.text = string.Empty;
        dialogueCurrentIndex = -1;
        speaking = false;

        dialogueFrame.SetActive(false);
        gameManager.ChangePlayerState(false);
    }
    #endregion
}
