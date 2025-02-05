using UnityEngine;

public class ElectromagneticWave : MonoBehaviour
{
    public int points = 100;      // Number of points in the wave
    public float wavelength = 5f; // Wavelength of the wave
    public float amplitude = 2f;  // Amplitude of the wave
    public float speed = 2f;      // Propagation speed

    private LineRenderer electricField;
    private LineRenderer magneticField;
    private float time = 0f;

    void Start()
    {
        // Create and configure LineRenderers for E and B fields
        electricField = CreateLineRenderer(Color.red);   // Red for Electric Field
        magneticField = CreateLineRenderer(Color.blue);  // Blue for Magnetic Field
    }

    void Update()
    {
        time += Time.deltaTime * speed;
        DrawWave(electricField, Vector3.right, amplitude); // E field along x-axis
        DrawWave(magneticField, Vector3.up, amplitude / 2); // B field along y-axis
    }

    LineRenderer CreateLineRenderer(Color color)
    {
        GameObject lineObj = new GameObject("Wave");
        lineObj.transform.parent = transform;
        LineRenderer lr = lineObj.AddComponent<LineRenderer>();

        lr.positionCount = points;
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = color;
        lr.endColor = color;

        return lr;
    }

    void DrawWave(LineRenderer lr, Vector3 axis, float amp)
    {
        for (int i = 0; i < points; i++)
        {
            float z = i * (wavelength / points); // Position along z-axis
            float y = amp * Mathf.Sin((2 * Mathf.PI / wavelength) * z - time); // Wave equation
            lr.SetPosition(i, axis * y + Vector3.forward * z);
        }
    }
}
