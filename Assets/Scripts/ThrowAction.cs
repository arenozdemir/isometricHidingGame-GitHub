using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;
public class ThrowAction : MonoBehaviour
{
    private bool takedRock;
    public bool aiming;
    [SerializeField] private Rig rig;
    private PlayerInput playerInput;

    private bool takeRock;
    private bool throwRock;
    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.FindAction("PickUp").performed += PickUp;

        playerInput.FindAction("Throw").started += Throw;
        playerInput.FindAction("Throw").performed += Throw;
        playerInput.FindAction("Throw").canceled += Throw;
    }
    private void PickUp(InputAction.CallbackContext context)
    {
        takeRock = context.ReadValueAsButton();
        GetComponent<Animator>().CrossFade("Throw 1", 0.1f);
        takedRock = true;
    }
    private void Throw(InputAction.CallbackContext context)
    {
        throwRock = context.ReadValueAsButton();
        if (!throwRock && takedRock)
        {
            GetComponent<Animator>().CrossFade("Throw 2", 0.1f);
            takedRock = false;
        }
    }
    private void Update()
    {
        RigWeightCalculator();
    }
    private void RigWeightCalculator()
    {
        if (throwRock)
        {
            rig.weight = Mathf.Lerp(rig.weight, .6f, Time.deltaTime * 2f);
        }
        else
        {
            rig.weight = Mathf.Lerp(rig.weight, .2f, Time.deltaTime * 2f);
        }
    }
    private void OnEnable()
    {
        playerInput.Enable();
    }
    private void OnDisable()
    {
        playerInput.Disable();
    }
}