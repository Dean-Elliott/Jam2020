using UnityEngine;

public class Paintable : MonoBehaviour, IInteractable
{
    [SerializeField]
    private ColorType desiredColor = ColorType.Red;

    [SerializeField]
    private Color unpaintedColor = Color.white;

    [SerializeField]
    private Renderer[] renderers = { };

    private ColorType colorPainted = ColorType.None;

    public float Percentage { get; private set; }

    public static Color GetColor(ColorType colorType)
    {
        if (colorType == ColorType.Blue)
        {
            return Color.blue;
        }
        else if (colorType == ColorType.Red)
        {
            return Color.red;
        }
        else if (colorType == ColorType.Yellow)
        {
            return Color.yellow;
        }

        return Color.white;
    }

    public void Paint(ColorType color)
    {
        colorPainted = color;

        //close enough, its done
        if (desiredColor == color)
        {
            Percentage = 1f;
        }
        else
        {
            Percentage = 0f;
        }
    }

    private void Update()
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.material.color = colorPainted == ColorType.None ? unpaintedColor : Paintable.GetColor(colorPainted);
        }
    }
}