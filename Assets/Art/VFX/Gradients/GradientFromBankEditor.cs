using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GradientFromBank))]
public class GradientFromBankEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GradientFromBank ctrl = (GradientFromBank)target;

        // Champs de base
        ctrl.gradientsBank = (GradientsBank)EditorGUILayout.ObjectField(
            "Gradients Bank",
            ctrl.gradientsBank,
            typeof(GradientsBank),
            false
        );

        ctrl.targetMaterial = (Material)EditorGUILayout.ObjectField(
            "Target Material",
            ctrl.targetMaterial,
            typeof(Material),
            false
        );

        if (ctrl.gradientsBank == null)
        {
            EditorGUILayout.HelpBox(
                "Assign a GradientsBank",
                MessageType.Info
            );
            return;
        }

        GUILayout.Space(10);
        GUILayout.Label("Gradients", EditorStyles.boldLabel);

        for (int i = 0; i < ctrl.gradientsBank.gradients.Length; i++)
        {
            var entry = ctrl.gradientsBank.gradients[i];

            GUILayout.BeginHorizontal();

            // Aperçu gradient
            Rect r = GUILayoutUtility.GetRect(80, 18);
            EditorGUI.GradientField(r, entry.gradient);

            // Bouton nom
            if (GUILayout.Button(entry.name, GUILayout.Height(18)))
            {
                ctrl.gradientIndex = i;
                ApplyGradient(ctrl);
            }

            GUILayout.EndHorizontal();
        }
    }

    void ApplyGradient(GradientFromBank ctrl)
    {
        if (ctrl.gradientsBank == null) return;
        if (ctrl.targetMaterial == null) return;

        Gradient g = ctrl.gradientsBank.gradients[ctrl.gradientIndex].gradient;

        Texture2D tex = new Texture2D(256, 1, TextureFormat.RGBA32, false);
        tex.wrapMode = TextureWrapMode.Clamp;
        tex.filterMode = FilterMode.Bilinear;

        for (int i = 0; i < 256; i++)
        {
            float t = i / 255f;
            tex.SetPixel(i, 0, g.Evaluate(t));
        }

        tex.Apply();
        ctrl.targetMaterial.SetTexture("_GradientTex", tex);
    }
}
