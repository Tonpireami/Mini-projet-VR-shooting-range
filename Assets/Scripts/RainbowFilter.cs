using UnityEngine;
using UnityEngine.UI;

public class RainbowFilter : MonoBehaviour
{
    public float speed = 1f;
    private Image img;
    private float hue = 0f;
    private float alpha;

    void Start()
    {
        img = GetComponent<Image>();
        alpha = img.color.a;
    }

    void Update()
    {
        hue += Time.deltaTime * speed;
        if (hue > 1f) hue -= 1f;

        Color c = Color.HSVToRGB(hue, 1f, 1f);
        c.a = alpha;
        img.color = c;
    }
}
