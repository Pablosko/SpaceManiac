using UnityEngine;

public class ParallaxTextureScroll : MonoBehaviour
{
    public float scrollSpeedX = 0.1f;
    public float scrollSpeedY = 0.1f;
    private Material mat;
    private Vector2 offset;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        offset.x += scrollSpeedX * Time.deltaTime;
        offset.y += scrollSpeedY * Time.deltaTime;
        mat.mainTextureOffset = offset;
    }
}
