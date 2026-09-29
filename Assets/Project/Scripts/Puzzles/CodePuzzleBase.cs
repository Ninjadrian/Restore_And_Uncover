using TMPro;
using UnityEngine;

public abstract class CodePuzzleBase : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] protected Camera puzzleCamera;

    [Header("UI")]
    [SerializeField] protected GameObject cleanlinessPanel;
    [SerializeField] protected GameObject timePanel;
    [SerializeField] protected GameObject counterDayPanel;
    [SerializeField] protected GameObject toolPanel;
    [SerializeField] protected GameObject puzzleObjects;

    [Header("Camera Movement")]
    [SerializeField] protected float mouseSensitivity = 200f;
    [SerializeField] protected float minLookX = -40;
    [SerializeField] protected float maxLookX = 40;
    [SerializeField] protected float minLookY = 160;
    [SerializeField] protected float maxLookY = 200;

    [Header("Interaction")]
    [SerializeField] protected float interactDistance = 0.5f;
    [SerializeField] protected LayerMask interactMask;

    [Header("Code")]
    [SerializeField] protected string codeValue;

    [SerializeField] private TMP_Text codeText;

    private float xRotation;
    private float yRotation = 180f;
    private string value = "";

    protected virtual void Update()
    {
        if (GameManager.Instance.gameState != GameState.Puzzle)
            return;

        HandleMouseLook();

        if (Input.GetKeyUp(KeyCode.E))
        {
            Interact();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            DeactivePuzzle();
        }
    }

    public virtual void ActivePuzzle()
    {
        xRotation = 0f;
        yRotation = 180f;
        value = "";

        UpdateCodeText();

        puzzleCamera.gameObject.SetActive(true);
        puzzleObjects.SetActive(true);

        cleanlinessPanel.SetActive(false);
        timePanel.SetActive(false);
        counterDayPanel.SetActive(false);
        toolPanel.SetActive(false);
    }

    protected void DeactivePuzzle()
    {
        puzzleCamera.gameObject.SetActive(false);
        puzzleObjects.SetActive(false);

        cleanlinessPanel.SetActive(true);
        timePanel.SetActive(true);
        counterDayPanel.SetActive(true);
        toolPanel.SetActive(true);

        GameManager.Instance.gameState = GameState.Play;
    }

    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minLookX, maxLookX);

        yRotation += mouseX;
        yRotation = Mathf.Clamp(yRotation, minLookY, maxLookY);

        puzzleCamera.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    private void Interact()
    {
        Ray ray = puzzleCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (!Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactMask))
            return;

        Collider col = hit.collider;

        if (col.CompareTag("Exit"))
        {
            DeactivePuzzle();
            return;
        }
        else if (col.CompareTag("Number") && TryGetButtonNumber(col.name, out char number))
        {
            EnterNumber(number);            
        }
    }

    private bool TryGetButtonNumber(string objectName, out char number)
    {
        number = objectName switch
        {
            "Number1" => '1',
            "Number2" => '2',
            "Number3" => '3',
            "Number4" => '4',
            "Number5" => '5',
            "Number6" => '6',
            "Number7" => '7',
            "Number8" => '8',
            "Number9" => '9',
            _ => '\0'
        };

        return number != '\0';
    }

    private void EnterNumber(char number)
    {
        value += number;        

        if (value.Length > codeValue.Length)
        {
            value = value.Substring(value.Length - codeValue.Length);
        }

        UpdateCodeText();

        if (value == codeValue)
        {
            OnCodeCorrect();
            DeactivePuzzle();
        }
    }

    private void UpdateCodeText()
    {
        if (codeText != null)
        {
            codeText.text = value;
        }
    }

    protected abstract void OnCodeCorrect();
}

