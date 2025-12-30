using UnityEngine;
using UnityEngine.InputSystem;

public class InputActionMapEnabler : MonoBehaviour
{
    public InputActionAsset actionsReference;
    public string actionMapName;

    private void OnEnable()
    {
        var actionMap = actionsReference.FindActionMap(actionMapName);
        if (actionMap != null)
        {
            actionMap.Enable();
        }
    }

    private void OnDisable()
    {
        var actionMap = actionsReference.FindActionMap(actionMapName);
        if (actionMap != null)
        {
            actionMap.Disable();
        }
    }
}