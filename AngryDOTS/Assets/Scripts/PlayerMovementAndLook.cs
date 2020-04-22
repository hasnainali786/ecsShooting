using UnityEngine;
using CnControls;
using Unity.Entities;


public class PlayerMovementAndLook : MonoBehaviour
{
    public static PlayerMovementAndLook obj;
    [Header("Camera")]
    public Camera mainCamera;

    [Header("Movement")]
    public float speed = 4.5f;
    public LayerMask whatIsGround;

    [Header("Life Settings")]
    public float playerHealth = 1f;

    [Header("Animation")]
    public Animator playerAnimator;

    Rigidbody playerRigidbody;
    bool isDead;
    public bool isInRange = false;

    //cnc
    public float MovementSpeed = 10f;

    private Transform _mainCameraTransform;
    private Transform _transform;
    private CharacterController _characterController;
    //cnc

    void Awake()
    {
        obj = this;
        playerRigidbody = GetComponent<Rigidbody>();
        _mainCameraTransform = Camera.main.GetComponent<Transform>();
        _transform = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        if (isDead)
            return;


        //Arrow Key Input
        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");
        var inputVector = new Vector3(CnInputManager.GetAxis("Horizontal"), CnInputManager.GetAxis("Vertical"));
        Vector3 inputDirection = inputVector;
        Vector3 movementVector = Vector3.zero;
        if (inputVector.sqrMagnitude > 0.001f)
        {
            movementVector = _mainCameraTransform.TransformDirection(inputVector);
            movementVector.y = 0f;
            movementVector.Normalize();
            //	_transform.forward = movementVector;
        }

        movementVector += Physics.gravity;
        //	_characterController.Move(movementVector * 4f * Time.deltaTime);

        //Camera Direction
        var cameraForward = mainCamera.transform.forward;
        var cameraRight = mainCamera.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        //Try not to use var for roadshows or learning code
        Vector3 desiredDirection = cameraForward * inputDirection.z + cameraRight * inputDirection.x;

        //Why not just pass the vector instead of breaking it up only to remake it on the other side?
        MoveThePlayer(movementVector);
         // TurnThePlayer();
       
        AnimateThePlayer(movementVector);



    }

    void MoveThePlayer(Vector3 desiredDirection)
    {
        Vector3 movement = new Vector3(desiredDirection.x, 0f, desiredDirection.z);
        movement = movement.normalized * speed * Time.deltaTime;

        playerRigidbody.MovePosition(transform.position + movement);
    }

    void TurnThePlayer()
    {
        float minDist = Mathf.Infinity;

        Transform nearest = null;

        Collider[] cols = Physics.OverlapSphere(transform.position, 8);
        
        foreach (Collider hitt in cols)
        {
            print(hitt.tag);
            if (hitt.tag == "Enemy")
            {

                float dist = Vector3.Distance(transform.position, hitt.transform.position);
                
                if (dist <= 7)
                {
                    Debug.Log(" in range");
                    minDist = dist;
                    nearest = hitt.transform;
                    isInRange = true;
                    Vector3 playerToMouse = nearest.transform.position - transform.position;
                    playerToMouse.y = 0f;
                    playerToMouse.Normalize();

                    Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
                    playerRigidbody.MoveRotation(newRotation);
                }
                else
                {
                    isInRange=false;
                    //Debug.LogError("not in range");
                }
            }
            else
            {
                //  isInRange=false;
                return;
            }
        }
        //  Quaternion newRotation = Quaternion.LookRotation(new Vector3(nearest.transform.position.x, nearest.transform.position.y, nearest.transform.position.z));
        //  playerRigidbody.MoveRotation(newRotation);

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere(transform.position, 7);
    }

    void AnimateThePlayer(Vector3 desiredDirection)
    {
        if (!playerAnimator)
            return;

        Vector3 movement = new Vector3(desiredDirection.x, 0f, desiredDirection.z);
        float forw = Vector3.Dot(movement, transform.forward);
        float stra = Vector3.Dot(movement, transform.right);

        playerAnimator.SetFloat("Forward", forw);
        playerAnimator.SetFloat("Strafe", stra);
    }

    //Player Collision
    void OnTriggerEnter(Collider theCollider)
    {
        if (!theCollider.CompareTag("Enemy"))
            return;

        playerHealth--;
        print("ecs is tough");
        if (playerHealth <= 0)
        {
            Settings.PlayerDied();
        }
    }

    public void PlayerDied()
    {
        if (isDead)
            return;

        isDead = true;

        playerAnimator.SetTrigger("Died");
        playerRigidbody.isKinematic = true;
        GetComponent<Collider>().enabled = false;
    }
}
