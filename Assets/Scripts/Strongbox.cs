using UnityEngine;

public class Strongbox : MonoBehaviour
{
    public GameObject strongboxCamera;
    public GameObject cleanlinessPanel;
    public GameObject timePanel;
    public GameObject counterDayPanel;
    public GameObject toolPanel;

    public float mouseSensitivity = 200f;
    public float minLookX = -40;
    public float maxLookX = 40f;

    public float minLookY = 160f;
    public float maxLookY = 200f;

    private float xRotation = 0f;
    private float yRotation = 180f;

    public float interactDistance = 0.5f;
    public LayerMask interactMask;

    private bool isTrying = false;

    private BoxCollider boxCol;

    private void Update()
    {
        if (isTrying)
        {
            HandleMouseLook();
            Interact();
        }
    }

    public void changeCamera()
    {
        boxCol = GetComponent<BoxCollider>();
        boxCol.enabled = false;

        isTrying = true;

        cleanlinessPanel.SetActive(false);
        timePanel.SetActive(false);
        counterDayPanel.SetActive(false);
        toolPanel.SetActive(false);

        strongboxCamera.SetActive(true);
        
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minLookX, maxLookX);

        yRotation += mouseX;
        yRotation = Mathf.Clamp(yRotation, minLookY, maxLookY);

        strongboxCamera.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    void Interact()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactMask))
        {
            Debug.Log("El objeto es: " + hit.collider.name);
        }
    }
}
