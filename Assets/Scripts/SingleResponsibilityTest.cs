using UnityEngine;

public class SingleResponsibilityTest : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("Horizontal Speed")]
    [SerializeField] private float moveSpeed;
    [Tooltip("Rate of change for movespeed")]
    [SerializeField] private float acceleration;
    [Tooltip("Deceleration rate when no input is provided")]
    [SerializeField] private float deceleration;

    [Header("Controls")]
    [Tooltip("use Keys to move")]
    [SerializeField] private KeyCode forwardKey = KeyCode.W;
    [SerializeField] private KeyCode backwardKey = KeyCode.S;
    [SerializeField] private KeyCode leftKey = KeyCode.A;
    [SerializeField] private KeyCode rightKey = KeyCode.D;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;

    [Header("Effects")]
    [SerializeField] private ParticleSystem partSys;

    [Header("Collision")]
    [SerializeField] LayerMask wallLayer;

    private Vector3 inputVector;
    private float currentSpeed;
    private CharacterController characterController;
    private float initialYPosition;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        initialYPosition = transform.position.y;
    }

    void Start()
    {

    }

    void Update()
    {
        HandleInput();
        Move(inputVector);
    }

    private void HandleInput()
    {
        float xIput = 0;
        float zIput = 0;

        if (Input.GetKey(forwardKey))
        {
            zIput++;
        }

        if (Input.GetKey(backwardKey))
        {
            zIput--;
        }

        if (Input.GetKey(leftKey))
        {
            xIput--;
        }

        if (Input.GetKey(rightKey))
        {
            xIput++;
        }
        inputVector = new Vector3(xIput, 0, zIput);
    }

    private void Move(Vector3 inputVector)
    {
        if (inputVector == Vector3.zero)
        {
            if (currentSpeed > 0)
            {
                currentSpeed -= deceleration * Time.deltaTime;
                currentSpeed = Mathf.Max(currentSpeed, 0);
            }
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, moveSpeed, Time.deltaTime * acceleration);
        }

        Vector3 movement = inputVector.normalized * currentSpeed * Time.deltaTime;
        characterController.Move(movement);
        transform.position = new Vector3(transform.position.x, initialYPosition, transform.position.z);
    }

    private void PlayAudioClip()
    {
        audioSource.Play();
    }

    private void PlayParticleEffect()
    {
        partSys.Play();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if ((wallLayer.value & (1 << hit.gameObject.layer)) > 0)
        {
            PlayAudioClip();
            PlayParticleEffect();
        }
    }
}