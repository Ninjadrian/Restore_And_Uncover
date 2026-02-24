using UnityEngine;

public class Phone : MonoBehaviour
{
    public Camera phoneCamera;

    public GameObject cleanlinessPanel;
    public GameObject timePanel;
    public GameObject counterDayPanel;
    public GameObject toolPanel;

    public GameObject puzzleObjects;

    public float mouseSensitivity = 200f;
    public float minLookX = -40;
    public float maxLookX = 40f;

    public float minLookY = 160f;
    public float maxLookY = 200f;

    [SerializeField] private float xRotation = 0f;
    [SerializeField] private float yRotation = 180f;

    public float interactDistance = 0.5f;
    public LayerMask interactMask;

    [SerializeField] private string codeValue = "7829";
    private string value = "000000000";
    private char buttonNumber;

    [SerializeField] private GameObject blueprint;

    private void Update()
    {
        if (GameManager.Instance.gameState == GameState.Puzzle)
        {
            HandleMouseLook();

            if (Input.GetKeyUp(KeyCode.E))
            {
                Interact();
            }
        }
    }

    public void ActivePuzzle()
    {
        xRotation = 0f;
        yRotation = 180f;

        phoneCamera.gameObject.SetActive(true);
        puzzleObjects.gameObject.SetActive(true);

        cleanlinessPanel.SetActive(false);
        timePanel.SetActive(false);
        counterDayPanel.SetActive(false);
        toolPanel.SetActive(false);
    }

    private void DeactivePuzzle()
    {
        phoneCamera.gameObject.SetActive(false);
        puzzleObjects.gameObject.SetActive(false);

        cleanlinessPanel.SetActive(true);
        timePanel.SetActive(true);
        counterDayPanel.SetActive(true);
        toolPanel.SetActive(true);

        GameManager.Instance.gameState = GameState.PLAY;
    }

    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minLookX, maxLookX);

        yRotation += mouseX;
        yRotation = Mathf.Clamp(yRotation, minLookY, maxLookY);

        phoneCamera.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    private void Interact()
    {
        Ray ray = phoneCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactMask))
        {
            Debug.Log("El objeto es: " + hit.collider.name);

            var col = hit.collider;

            if (col.CompareTag("Exit"))
            {
                Debug.Log("Saliendo");
                DeactivePuzzle();                
            } else if (col.CompareTag("Number"))
            {
                if (col.name.Equals("Number1")) buttonNumber = '1';
                else if (col.name.Equals("Number2")) buttonNumber = '2';
                else if (col.name.Equals("Number3")) buttonNumber = '3';
                else if (col.name.Equals("Number4")) buttonNumber = '4';
                else if (col.name.Equals("Number5")) buttonNumber = '5';
                else if (col.name.Equals("Number6")) buttonNumber = '6';
                else if (col.name.Equals("Number7")) buttonNumber = '7';
                else if (col.name.Equals("Number8")) buttonNumber = '8';
                else if (col.name.Equals("Number9")) buttonNumber = '9';

                Code(buttonNumber);
            }
        }
    }

    private void Code(char number)
    {
        value += number;

        value = value.Substring(value.Length - 9);

        Debug.Log("Code: " + value);

        if (value == codeValue)
        {
            blueprint.SetActive(true);
        }
    }
}
