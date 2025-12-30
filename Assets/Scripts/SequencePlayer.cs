using System;
using System.Collections;
using UnityEngine;

public class SequencePlayer : MonoBehaviour
{
    public CharacterCommandController[] characterControllers => GameManager.instance.characterControllers;
    public int currentCharacterIndex = 0;
    private Coroutine playSequenceCoroutine = null;
    public InputManager inputManager;
    public GameManager gameManager;

    public void SendOpCode(SequenceOpCode opCode)
    {
        switch (opCode)
        {
            case SequenceOpCode.Play:
                inputManager.acceptingInput = false;
                playSequenceCoroutine = StartCoroutine(PlaySequence());
                break;
            case SequenceOpCode.Stop:
            case SequenceOpCode.Invalid:
            case SequenceOpCode.Death:
                StopCoroutine(playSequenceCoroutine);
                gameManager.ResetResettables();
                playSequenceCoroutine = null;
                inputManager.acceptingInput = true;
                break;
            case SequenceOpCode.Victory:
                StopCoroutine(playSequenceCoroutine);
                playSequenceCoroutine = null;
                inputManager.acceptingInput = true;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(opCode), opCode, null);
        }
    }
    
    public void TogglePlaySequence()
    {
        if(playSequenceCoroutine != null)
        {
            SendOpCode(SequenceOpCode.Stop);
        }
        else
        {
            SendOpCode(SequenceOpCode.Play);
        }
    }
    
    private IEnumerator PlaySequence()
    {
        yield return null;
        int moveIndex = 0;
        while (true)
        {
            bool anyCommandsLeft = false;
            foreach (var characterController in characterControllers)
            {
                if (characterController != null)
                {
                    if (moveIndex < characterController.commands.Count)
                    {
                        anyCommandsLeft = true;
                    }
                    characterController.TakeTurn(moveIndex);
                }
            }
            if (!anyCommandsLeft)
            {
                SendOpCode(SequenceOpCode.Stop);
                break;
            }
            moveIndex++;
            yield return new WaitForSeconds(.25f); // Wait for .25 second between moves
        }
    }
}