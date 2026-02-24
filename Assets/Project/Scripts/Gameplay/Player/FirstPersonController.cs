using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float runSpeed = 9f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;

    private bool isCrouch = false;

    [Header("Camera Look")]
    public Transform cameraTransform;
    public float mouseSensitivity = 200f;
    public float minLookX = -80f;
    public float maxLookX = 80f;

    private CharacterController controller;
    private float verticalVelocity;
    private float xRotation = 0f;

    [Header("Interactor")]
    public float interactDistance = 3f;
    public LayerMask interactMask;

    private Drawer drawer;
    private Door door;
    private HatchDoor hatchDoor;

    public GameEvent puzzleEvent;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (GameManager.Instance.gameState == GameState.PLAY)
        {
            HandleMovement();
            HandleMouseLook();

            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }

            if (Input.GetKeyDown(KeyCode.CapsLock))
            {
                Crouch();
            }
        }
    }

    //Movimiento del personaje
    void HandleMovement()
    {
        float inputX = Input.GetAxis("Horizontal");
        float InputZ = Input.GetAxis("Vertical");

        // Dirección relativa a donde estamos mirando
        Vector3 move = transform.right * inputX + transform.forward * InputZ;

        // Correr
        if (Input.GetButton("Sprint"))
        {
            controller.Move(move * runSpeed * Time.deltaTime);
        }
        else
        {
            controller.Move(move * walkSpeed * Time.deltaTime);
        }

        // Gravedad
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }


        // Salto
        if (Input.GetButtonDown("Jump"))
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        verticalVelocity += gravity * Time.deltaTime;
        controller.Move(Vector3.up * verticalVelocity * Time.deltaTime);
    }

    //Rotación de la cámara
    void HandleMouseLook()
    {
        // Movimiento del ratón
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotar la cámara arriba/abajo
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minLookX, maxLookX);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotar cuerpo izquierda/derecha
        transform.Rotate(Vector3.up * mouseX);
    }

    //Interacciones con otros elementos
    void Interact()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactMask))
        {
            //Debug.Log("El objeto es: " + hit.collider.name);

            if (hit.collider.TryGetComponent<LightSwitch>(out var lightSwitch))
            {
                lightSwitch.SwitchLights();
            }
            else if (hit.collider.CompareTag("Drawer"))
            {
                drawer = hit.collider.GetComponent<Drawer>();
                drawer.OpenDrawer();
            }
            else if (hit.collider.CompareTag("Door"))
            {
                door = hit.collider.GetComponent<Door>();
                door.OpenDoor();
            }
            else if (hit.collider.CompareTag("HatchDoor"))
            {
                hatchDoor = hit.collider.GetComponent<HatchDoor>();
                hatchDoor.TryOpenHatchDoor();
            }
            else if (hit.collider.CompareTag("Puzzle"))
            {
                puzzleEvent.Raise();
            }
            else if (hit.collider.TryGetComponent<ToolPickUp>(out var toolPickUp))
            {
                toolPickUp.PickUp();
            }
            else if (hit.collider.TryGetComponent<RecyclablePickUp>(out var recyclablePickUp))
            {
                recyclablePickUp.PickUp();
            }
            else if (hit.collider.TryGetComponent<CollectablePickUp>(out var collectablePickUp))
            {
                collectablePickUp.PickUp();
            }
            else if (hit.collider.TryGetComponent<ValidateKeyCard>(out var keyCard))
            {
                keyCard.ActiveSwitchCard();
            }
        }
    }

    //Agacharse
    void Crouch()
    {
        if (!isCrouch)
        {
            controller.height = 1f;
            cameraTransform.localPosition = new Vector3(0f, 1.1f, 0f);
        }
        else if (isCrouch)
        {
            controller.height = 1.76f;
            cameraTransform.localPosition = new Vector3(0f, 1.6f, 0f);
        }
        isCrouch = !isCrouch;
    }
}
