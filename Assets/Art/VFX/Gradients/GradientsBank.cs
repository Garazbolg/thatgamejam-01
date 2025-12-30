using UnityEngine;

[System.Serializable]
public class NamedGradient
{
    public string name;
    public Gradient gradient;
}

[CreateAssetMenu(
    fileName = "GradientsBank",
    menuName = "VFX/Gradients Bank"
)]
public class GradientsBank : ScriptableObject
{
    public NamedGradient[] gradients;
}
