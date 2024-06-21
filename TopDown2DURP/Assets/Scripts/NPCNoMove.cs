using System.Collections;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Windows;

public class NPCNoMove : MonoBehaviour, Interactive
{
    [SerializeField] private GameManagerSO gameManager;

    [Header("Dialogue System")]
    [SerializeField, TextArea(1,5)] private string[] dialogueStrings;
    [SerializeField] private float timeBetweenLetters;
    [SerializeField] private GameObject dialogueFrame;
    [SerializeField] private TextMeshProUGUI dialogueText;
    private bool speaking = false;
    private int dialogueCurrentIndex = -1;

    private Vector3 destinationPosition;
    private Vector3 initialPosition;
    private Animator anim;

    private void Awake()
    {
        initialPosition = transform.position;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    #region Dialogue Methods
    private IEnumerator WriteSentence()
    {
        speaking = true;
        dialogueText.text = string.Empty;

        char[] sentenceCharacters = dialogueStrings[dialogueCurrentIndex].ToCharArray();

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
            FollowingSentence();
        }
        else
        {
            CompleteSentence();
        }
    }

    private void FollowingSentence()
    {
        dialogueCurrentIndex++;
        if(dialogueCurrentIndex >= dialogueStrings.Length)
        {
            FinishDialogue();
        }
        else
        {
            StartCoroutine(WriteSentence());
        }
    }
    private void CompleteSentence()
    {
        StopAllCoroutines();
        dialogueText.text = dialogueStrings[dialogueCurrentIndex];
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
