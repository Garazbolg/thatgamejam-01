using UnityEngine;

[ExecuteAlways]
public class GradientFromBank : MonoBehaviour
{
    public GradientsBank gradientsBank;
    public int gradientIndex;

    public Material targetMaterial;
    public string gradientProperty = "_GradientTex";

    Texture2D bakedTexture;

    void OnValidate()
    {
        Apply();
    }

    void Apply()
    {
        if (gradientsBank == null) return;
        if (gradientsBank.gradients.Length == 0) return;
        if (targetMaterial == null) return;

        gradientIndex = Mathf.Clamp(
            gradientIndex,
            0,
            gradientsBank.gradients.Length - 1
        );

        bakedTexture = BakeGradient(
            gradientsBank.gradients[gradientIndex].gradient
        );

        targetMaterial.SetTexture(gradientProperty, bakedTexture);
    }

    Texture2D BakeGradient(Gradient gradient)
    {
        Texture2D tex = new Texture2D(256, 1, TextureFormat.RGBA32, false);
        tex.wrapMode = TextureWrapMode.Clamp;
        tex.filterMode = FilterMode.Bilinear;

        for (int i = 0; i < 256; i++)
        {
            float t = i / 255f;
            tex.SetPixel(i, 0, gradient.Evaluate(t));
        }

        tex.Apply();
        return tex;
    }
}
