using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public GameManager instance;
    
    public bool acceptingInput = true;
    
    public InputActionReference commandUpAction;
    public InputActionReference commandDownAction;
    public InputActionReference commandLeftAction;
    public InputActionReference commandRightAction;
    public InputActionReference commandWaitAction;
    
    public InputActionReference nextCharacterAction;
    public InputActionReference playSequenceAction;
    public InputActionReference eraseSequenceAction;

    private void OnEnable()
    {
        commandUpAction.action.performed += OnCommandUp;
        commandDownAction.action.performed += OnCommandDown;
        commandLeftAction.action.performed += OnCommandLeft;
        commandRightAction.action.performed += OnCommandRight;
        commandWaitAction.action.performed += OnCommandWait;

        nextCharacterAction.action.performed += OnNextCharacter;
        playSequenceAction.action.performed += OnPlaySequence;
        eraseSequenceAction.action.performed += OnEraseSequence;

        commandUpAction.action.Enable();
        commandDownAction.action.Enable();
        commandLeftAction.action.Enable();
        commandRightAction.action.Enable();
        commandWaitAction.action.Enable();

        nextCharacterAction.action.Enable();
        playSequenceAction.action.Enable();
        eraseSequenceAction.action.Enable();
    }
    
    private void OnDisable()
    {
        commandUpAction.action.performed -= OnCommandUp;
        commandDownAction.action.performed -= OnCommandDown;
        commandLeftAction.action.performed -= OnCommandLeft;
        commandRightAction.action.performed -= OnCommandRight;
        commandWaitAction.action.performed -= OnCommandWait;

        nextCharacterAction.action.performed -= OnNextCharacter;
        playSequenceAction.action.performed -= OnPlaySequence;
        eraseSequenceAction.action.performed -= OnEraseSequence;

        commandUpAction.action.Disable();
        commandDownAction.action.Disable();
        commandLeftAction.action.Disable();
        commandRightAction.action.Disable();
        commandWaitAction.action.Disable();

        nextCharacterAction.action.Disable();
        playSequenceAction.action.Disable();
        eraseSequenceAction.action.Disable();
    }
    
    private void OnCommandUp(InputAction.CallbackContext context)
    {
        if(acceptingInput)
            instance.EnqueueCommand(Command.MoveUp);
    }
    
    private void OnCommandDown(InputAction.CallbackContext context)
    {
        if(acceptingInput)
            instance.EnqueueCommand(Command.MoveDown);
    }
    
    private void OnCommandLeft(InputAction.CallbackContext context)
    {
        if(acceptingInput)
            instance.EnqueueCommand(Command.MoveLeft);
    }
    
    private void OnCommandRight(InputAction.CallbackContext context)
    {
        if(acceptingInput)
            instance.EnqueueCommand(Command.MoveRight);
    }
    
    private void OnCommandWait(InputAction.CallbackContext context)
    {
        if(acceptingInput)
            instance.EnqueueCommand(Command.Wait);
    }
    
    private void OnNextCharacter(InputAction.CallbackContext context)
    {
        if(acceptingInput)
            instance.NextCharacter();
    }
    
    private void OnPlaySequence(InputAction.CallbackContext context)
    {
        instance.sequencePlayer.TogglePlaySequence();
    }
    
    private void OnEraseSequence(InputAction.CallbackContext context)
    {
        if(acceptingInput)
            instance.EraseCharacterCommands();
    }
}