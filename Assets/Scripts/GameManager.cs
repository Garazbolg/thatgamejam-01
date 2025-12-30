using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CharacterCommandController[] characterControllers;
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
        resettableEntities = FindObjectsByType<ResettableEntity>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
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
        currentCharacterIndex++;
        currentCharacterIndex %= characterControllers.Length;
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
    
    public void ResetResettables()
    {
        foreach (var resettable in resettableEntities)
        {
            resettable.ResetEntity();
        }
    }
}