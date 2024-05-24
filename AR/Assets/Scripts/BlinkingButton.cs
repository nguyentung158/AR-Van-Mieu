using UnityEngine;
using UnityEngine.UI;

public class BlinkingButton : MonoBehaviour
{
    [SerializeField]
    public Button yourButton; // Gán nút của bạn trong Inspector
    public float blinkSpeed = 1f; // Tốc độ nhấp nháy
    public bool isSelected = false;

    private Image buttonImage;
    private float baseAlpha;
    private float direction = 1;

    void Start()
    {
        buttonImage = yourButton.GetComponent<Image>();
        baseAlpha = buttonImage.color.a;
    }

    void Update()
    {
        if (!isSelected)
        {
            float alphaChange = blinkSpeed * Time.deltaTime * direction;
            float newAlpha = buttonImage.color.a + alphaChange;

            if (newAlpha > baseAlpha)
            {
                newAlpha = baseAlpha;
                direction = -1;
            }
            else if (newAlpha < 0)
            {
                newAlpha = 0;
                direction = 1;
            }

            buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, newAlpha);
        } else
        {
            buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, 1);
        }
    }


}
