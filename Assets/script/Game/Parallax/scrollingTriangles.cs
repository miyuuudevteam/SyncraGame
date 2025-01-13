using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingTriangles : MonoBehaviour
{
    [Header("Settings")]
    public RectTransform scrollArea; // Area where triangles are generated
    public GameObject trianglePrefab; // Prefab for triangle (Image component)
    public int triangleCount = 50; // Number of triangles to generate
    public float scrollSpeed = 50f; // Speed of scrolling
    public Color triangleColor = Color.white; // Color of the triangles
    [Range(0f, 1f)] public float triangleTransparency = 1f; // Transparency of the triangles

    private List<RectTransform> triangles = new List<RectTransform>();
    private float areaWidth;
    private float areaHeight;

    void Start()
    {
        // Get scroll area dimensions
        areaWidth = scrollArea.rect.width;
        areaHeight = scrollArea.rect.height;

        // Generate initial triangles
        for (int i = 0; i < triangleCount; i++)
        {
            CreateTriangle();
        }
    }

    void Update()
    {
        // Move triangles
        foreach (var triangle in triangles)
        {
            triangle.anchoredPosition -= new Vector2(scrollSpeed * Time.deltaTime, 0);

            // Recycle triangles that move out of the scroll area
            if (triangle.anchoredPosition.x < -areaWidth / 2)
            {
                RepositionTriangle(triangle);
            }
        }
    }

    void CreateTriangle()
    {
        GameObject triangleObj = Instantiate(trianglePrefab, scrollArea);
        RectTransform rectTransform = triangleObj.GetComponent<RectTransform>();

        // Set random position within the scroll area
        rectTransform.anchoredPosition = new Vector2(
            Random.Range(-areaWidth / 2, areaWidth / 2),
            Random.Range(-areaHeight / 2, areaHeight / 2)
        );

        // Randomize size
        float size = Random.Range(10f, 30f);
        rectTransform.sizeDelta = new Vector2(size, size);

        // Randomize rotation
        rectTransform.localRotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));

        // Set color and transparency
        Image triangleImage = triangleObj.GetComponent<Image>();
        Color adjustedColor = triangleColor;
        adjustedColor.a = triangleTransparency;
        triangleImage.color = adjustedColor;

        // Ensure the triangle is behind all other UI elements
        triangleObj.transform.SetAsFirstSibling();

        // Add to the list
        triangles.Add(rectTransform);
    }

    void RepositionTriangle(RectTransform triangle)
    {
        // Reposition at the right of the scroll area with random Y position
        triangle.anchoredPosition = new Vector2(
            areaWidth / 2,
            Random.Range(-areaHeight / 2, areaHeight / 2)
        );

        // Randomize size again if desired
        float size = Random.Range(10f, 30f);
        triangle.sizeDelta = new Vector2(size, size);

        // Randomize rotation again
        triangle.localRotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));

        // Update color and transparency if needed
        Image triangleImage = triangle.GetComponent<Image>();
        Color adjustedColor = triangleColor;
        adjustedColor.a = triangleTransparency;
        triangleImage.color = adjustedColor;
    }
}
