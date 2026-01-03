using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CharacterCommandController[] characterControllers;
    public WinCondition[] winConditions;
    public ResettableEntity[] resettableEntities;
    public int currentCharacterIndex = 0;
    public InputManager inputManager;
    public SequencePlayer sequencePlayer;
    
    public static GameManager instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        currentCharacterIndex = 0;
        characterControllers = FindObjectsByType<CharacterCommandController>(FindObjectsInactive.Exclude, FindObjectsSortMode.InstanceID);
        Array.Sort(characterControllers, (a, b) => a.spawnIndex.CompareTo(b.spawnIndex));
        winConditions = FindObjectsByType<WinCondition>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        resettableEntities = FindObjectsByType<ResettableEntity>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        foreach (var characterController in characterControllers)
        {
            characterController.pathPreviz.Hide();
        }
        characterControllers[currentCharacterIndex].pathPreviz.Show();
    }
    
    public void EnqueueCommand(Command command)
    {
        if (currentCharacterIndex >= characterControllers.Length)
        {
            currentCharacterIndex = 0;
        }
        var characterController = characterControllers[currentCharacterIndex];
        if (characterController != null)
        {
            characterController.commands.Add(command);
        }
        
        characterController.pathPreviz.UpdatePreviz(characterController);
    }

    public void NextCharacter()
    {
        characterControllers[currentCharacterIndex].pathPreviz.Hide();
        currentCharacterIndex++;
        currentCharacterIndex %= characterControllers.Length;
        characterControllers[currentCharacterIndex].pathPreviz.Show();
    }
    
    public void EraseCharacterCommands()
    {
        if (currentCharacterIndex >= characterControllers.Length)
        {
            currentCharacterIndex = 0;
        }
        var characterController = characterControllers[currentCharacterIndex];
        if (characterController != null)
        {
            characterController.commands.Clear();
        }
        characterController.pathPreviz.ClearPreviz();
    }
    
    public void EraseLastCommand()
    {
        if (currentCharacterIndex >= characterControllers.Length)
        {
            currentCharacterIndex = 0;
        }
        var characterController = characterControllers[currentCharacterIndex];
        if (characterController != null && characterController.commands.Count > 0)
        {
            characterController.commands.RemoveAt(characterController.commands.Count - 1);
        }
        characterController.pathPreviz.UpdatePreviz(characterController);
    }
    
    public void ResetResettables()
    {
        foreach (var resettable in resettableEntities)
        {
            resettable.ResetEntity();
        }
    }
}