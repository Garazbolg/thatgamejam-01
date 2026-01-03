using System;
using System.Collections;
using UnityEngine;

public class SequencePlayer : MonoBehaviour
{
    public CharacterCommandController[] characterControllers => GameManager.instance.characterControllers;
    private Coroutine playSequenceCoroutine = null;
    public InputManager inputManager;
    public GameManager gameManager;
    
    public GameObject invalidMoveEffectPrefab;


    public void SendOpCode(SequenceOpCode opCode, Vector3 position = default, int characterIndex = -1)
    {
        switch (opCode)
        {
            case SequenceOpCode.Play:
                inputManager.acceptingInput = false;
                gameManager.characterControllers[gameManager.currentCharacterIndex].pathPreviz.Hide();
                playSequenceCoroutine = StartCoroutine(PlaySequence());
                break;
            case SequenceOpCode.Invalid:
            case SequenceOpCode.Death:
                StopCoroutine(playSequenceCoroutine);
                if (invalidMoveEffectPrefab != null)
                {
                    var go = Instantiate(invalidMoveEffectPrefab, position, Quaternion.identity);
                }
                gameManager.ResetResettables();
                playSequenceCoroutine = null;
                inputManager.acceptingInput = true;
                gameManager.characterControllers[gameManager.currentCharacterIndex].pathPreviz.Show();
                break;
            case SequenceOpCode.Stop:
                StopCoroutine(playSequenceCoroutine);
                gameManager.ResetResettables();
                playSequenceCoroutine = null;
                inputManager.acceptingInput = true;
                gameManager.characterControllers[gameManager.currentCharacterIndex].pathPreviz.Show();
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
            
            yield return null;

            bool winConditionMet = true;
            foreach (var winCondition in gameManager.winConditions)
            {
                if (winCondition != null && !winCondition.IsWinConditionMet())
                {
                    winConditionMet = false;
                    break;
                }
            }
            
            if (winConditionMet)
            {
                SendOpCode(SequenceOpCode.Victory);
                break;
            }
            
            if (!anyCommandsLeft)
            {
                SendOpCode(SequenceOpCode.Stop);
                break;
            }
            moveIndex++;
            foreach (var characterController in characterControllers)
            {
                if (characterController != null)
                {
                    characterController.PreviewTurn(moveIndex);
                }
            }
            yield return new WaitForSeconds(.25f); // Wait for .25 second between moves
        }
    }
}