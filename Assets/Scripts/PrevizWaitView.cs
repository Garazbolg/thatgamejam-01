using UnityEngine;
using UnityEngine.UI;

public class PrevizWaitView : MonoBehaviour
{
    public TMPro.TMP_Text waitText;
    public Image backgroundImage;

    public void SetColor(Color color)
    {
        waitText.color = color;
        backgroundImage.color = color;
    }
}