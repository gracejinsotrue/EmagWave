using UnityEngine;
using UnityEngine.UI;

public class ElectromagneticWave : MonoBehaviour
{
    public int points = 100;      
    public float wavelength = 5f; 
    public float amplitude = 2f;  
    public float speed = 2f;      

    private LineRenderer electricField;
    private LineRenderer magneticField;
    private float time = 0f;

    //declare speed and amplitude sliders
    public Slider amplitudeSlider; 
    public Slider speedSlider;     

    void Start()
    {
        //amplitude
        if (amplitudeSlider == null)
        {
            amplitudeSlider = GameObject.Find("AmplitudeSlider")?.GetComponent<Slider>();
        }
        if (amplitudeSlider != null)
        {
            amplitudeSlider.onValueChanged.AddListener(UpdateAmplitude);
        }
        else
        {
            Debug.LogError("Amplitude slider not assigned or found. Please assign it in the Inspector or name it 'AmplitudeSlider'.");
        }


        //speed slider
        if (speedSlider == null)
        {
            speedSlider = GameObject.Find("SpeedSlider")?.GetComponent<Slider>();
        }
        if (speedSlider != null)
        {
            speedSlider.onValueChanged.AddListener(UpdateSpeed);
        }
        else
        {
            Debug.LogError("Speed slider not assigned or found. Please assign it in the Inspector or name it 'SpeedSlider'.");
        }

        // Create and configure LineRenderers
        electricField = CreateLineRenderer(Color.red);
        magneticField = CreateLineRenderer(Color.blue);
    }

    void Update()
    {
        time += Time.deltaTime * speed;

        DrawWave(electricField, Vector3.right, amplitude); // Electric field
        DrawWave(magneticField, Vector3.up, amplitude / 2); // Magnetic field
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
        amplitude = value;
        Debug.Log("Amplitude updated: " + amplitude);
    }

    void UpdateSpeed(float value)
    {
        speed = value;
       // Debug.Log("Speed updated: " + speed);
    }

    void DrawWave(LineRenderer lr, Vector3 axis, float amp)
    {
        for (int i = 0; i < points; i++)
        {
            float z = i * (wavelength / points);
            float y = amp * Mathf.Sin((2 * Mathf.PI / wavelength) * z - time);
            lr.SetPosition(i, axis * y + Vector3.forward * z);
        }
    }
}
