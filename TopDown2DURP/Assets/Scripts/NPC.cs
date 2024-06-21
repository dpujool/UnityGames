using System.Collections;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Windows;

public class NPC : MonoBehaviour, Interactive
{
    [SerializeField] private GameManagerSO gameManager;

    [Header("Movement System")]
    [SerializeField] private float movementVelocity;
    [SerializeField] private float timeBetweenWaiting;
    [SerializeField] private float detectionRadius;
    [SerializeField] private float maximDistance;
    [SerializeField] private LayerMask whatIsObstacle;

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
    private Coroutine moveCoroutine;

    private void Awake()
    {
        initialPosition = transform.position;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        moveCoroutine = StartCoroutine(GoToDestinyAndWait());
    }

    #region Movement Methods
    private IEnumerator GoToDestinyAndWait()
    {
        while (!speaking)
        {
                CalculateNewDestiny();

                while (transform.position != destinationPosition)
                {
                    transform.position = Vector3.MoveTowards(transform.position, destinationPosition, movementVelocity * Time.deltaTime);
                    yield return null;
                }

                anim.SetBool("walking", false);
                yield return new WaitForSeconds(timeBetweenWaiting);
        }
    }

    private void CalculateNewDestiny()
    {
        bool validTile = false;
        int tries = 0;

        while (!validTile && tries < 15)
        {
            int prob = Random.Range(0, 4);

            switch (prob)
            {
                case 0:
                    destinationPosition = transform.position + Vector3.left;
                    anim.SetBool("walking", true);
                    ChangeAnim(-1,0);
                    break;
                case 1:
                    destinationPosition = transform.position + Vector3.right;
                    anim.SetBool("walking", true);
                    ChangeAnim(1, 0);
                    break;
                case 2:
                    destinationPosition = transform.position + Vector3.up;
                    anim.SetBool("walking", true);
                    ChangeAnim(0, 1);
                    break;
                case 3:
                    destinationPosition = transform.position + Vector3.down;
                    anim.SetBool("walking", true);
                    ChangeAnim(0, -1);
                    break;
            }

            validTile = FreeTileAndDistanceLimitIn();
            tries++;
        }
    }

    private void ChangeAnim(float inputH, float inputV)
    {
        anim.SetFloat("inputH", inputH);
        anim.SetFloat("inputV", inputV);

        if (inputH > 0)
        {
            anim.transform.eulerAngles = new Vector3(0, -180, 0);
        }
        else
        {
            anim.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    private bool FreeTileAndDistanceLimitIn()
    {

        if(Vector3.Distance(initialPosition,destinationPosition) > maximDistance)
        {
            return false;
        }
        else
        {
            var sollision = Physics2D.OverlapCircle(destinationPosition, detectionRadius, whatIsObstacle);
            if (sollision != null)
            {
                
            }
            return !Physics2D.OverlapCircle(destinationPosition, detectionRadius, whatIsObstacle);
        }
    }
    #endregion

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
        StopCoroutine(moveCoroutine);
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
        moveCoroutine = StartCoroutine(GoToDestinyAndWait());
    }
    #endregion
}
