using UnityEngine;
using UnityEngine.UI;

public class RainbowFilter : MonoBehaviour
{
    public float speed = 1f;
    private Image img;
    private float hue = 0f;
    private float alpha; // on stocke l'alpha d'origine

    void Start()
    {
        img = GetComponent<Image>();
        alpha = img.color.a; // on garde l'opacité définie dans l'inspector
    }

    void Update()
    {
        hue += Time.deltaTime * speed;
        if (hue > 1f) hue -= 1f;

        Color c = Color.HSVToRGB(hue, 1f, 1f);
        c.a = alpha; // on remet l'opacité d'origine
        img.color = c;
    }
}
