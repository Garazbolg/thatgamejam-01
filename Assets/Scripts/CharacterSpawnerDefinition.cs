using UnityEngine;

public class CharacterSpawnerDefinition : MonoBehaviour
{
    public Color color;
    public float lineAlpha;
    public float characterAlpha;
    public int spawnIndex;
    
    public PathPreviz pathPreviz;
    public SpriteRenderer spriteRenderer;
    public LineRenderer lineRenderer;
    public CharacterCommandController characterController;

    private void OnValidate()
    {
        var characterColor = color;
        characterColor.a = characterAlpha;
        spriteRenderer.color = characterColor;
        var lineColor = color;
        lineColor.a = lineAlpha;
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
        characterController.spawnIndex = spawnIndex;
        pathPreviz.waitColor = lineColor;
    }
}