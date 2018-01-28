using UnityEngine;

public class TitleWiggle : MonoBehaviour
{
    public float MinScale;
    public float MaxScale;
    public float StartingAngle;
    public float AngleSpeed;
    
    private float angle;

    public SpriteRenderer controllerSprite;

    private void Start()
    {
        angle = StartingAngle;
    }

    private void Update()
    {
        angle += AngleSpeed * Time.deltaTime;
        var t = (Mathf.Sin(angle * Mathf.Deg2Rad)+1)/2;
        var scale = Mathf.Lerp(MinScale, MaxScale, t);

        transform.localScale = Vector3.one * scale;
    }
}