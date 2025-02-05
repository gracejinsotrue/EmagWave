using UnityEngine;
using UnityEngine.UI;

public class ElectromagneticWave : MonoBehaviour
{
    //default numerical assingments but they are bound to change based on slider
    public int points = 100;      
    public float wavelength = 5f; // Wavelength of the wave
    public float amplitude = 2f;  // Amplitude of the wave
    public float speed = 2f;      // Propagation speed

    private LineRenderer electricField;
    private LineRenderer magneticField;
    private float time = 0f;

    public Slider amplitudeSlider; // assignment for Amplitude Slider

    void Start()
    {
        //Here is the place where I am putting slider code
        // Automatically find the slider in the scene if not assigned
        if (amplitudeSlider == null)
        {
            amplitudeSlider = FindObjectOfType<Slider>();
        }

        if (amplitudeSlider != null)
        {
            amplitudeSlider.onValueChanged.AddListener(value => UpdateAmplitude(value));
        }
        else
        {
            Debug.LogError("Amplitude slider not found in the scene. Please assign it in the Inspector.");
        }





        // Create and configure LineRenderers for E and B fields
        electricField = CreateLineRenderer(Color.red);   // Red for Electric Field
        magneticField = CreateLineRenderer(Color.blue);  // Blue for Magnetic Field
    }

    void Update()
    {
        time += Time.deltaTime * speed;

        // Draw the waves with the current amplitude
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

    void UpdateAmplitude(float value)
    {
        amplitude = value; // Update the amplitude based on the slider value
        Debug.Log("Amplitude updated: " + amplitude);
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
