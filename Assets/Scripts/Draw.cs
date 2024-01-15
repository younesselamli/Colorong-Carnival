using UnityEngine;
using UnityEngine.UI;

public class Draw : MonoBehaviour
{
    public Camera m_camera;
    public GameObject brushPrefab;
    public Slider brushSizeSlider;
    public Toggle eraserToggle;

    public Color currentColor = Color.black;
    public float brushSize = 0.3f;
    public bool isEraser = false;

    private Vector2? lastPos;

    private void Start()
    {
        // Set up brush size slider listener
        brushSizeSlider.onValueChanged.AddListener(SetBrushSize);

        // Set up eraser toggle listener
        eraserToggle.onValueChanged.AddListener(SetEraser);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            StartDrawing();
        }
        else if (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved))
        {
            ContinueDrawing();
        }
        else if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            StopDrawing();
        }
    }

    void StartDrawing()
    {
        lastPos = GetInputPosition();
        if (eraserToggle.isOn)
        {
            currentColor = Color.white; // Eraser color
        }
        else
        {
            currentColor = Color.black; // Set default drawing color
        }

        CreateBrush();
    }

    void ContinueDrawing()
    {
        if (!lastPos.HasValue)
            return;

        Vector2 currentPos = GetInputPosition();
        DrawLine(lastPos.Value, currentPos);
        lastPos = currentPos;
    }

    void StopDrawing()
    {
        lastPos = null;
    }

    void CreateBrush()
    {
        GameObject brushInstance = Instantiate(brushPrefab);
        LineRenderer currentLineRenderer = brushInstance.GetComponent<LineRenderer>();
        currentLineRenderer.material.color = currentColor;

        Vector2 inputPos = GetInputPosition();
        currentLineRenderer.positionCount = 2;
        currentLineRenderer.SetPosition(0, inputPos);
        currentLineRenderer.SetPosition(1, inputPos);
        currentLineRenderer.startWidth = brushSize;
        currentLineRenderer.endWidth = brushSize;
    }

    void DrawLine(Vector2 startPos, Vector2 endPos)
    {
        GameObject brushInstance = Instantiate(brushPrefab);
        LineRenderer currentLineRenderer = brushInstance.GetComponent<LineRenderer>();
        currentLineRenderer.material.color = currentColor;

        currentLineRenderer.positionCount = 2;
        currentLineRenderer.SetPosition(0, startPos);
        currentLineRenderer.SetPosition(1, endPos);
        currentLineRenderer.startWidth = brushSize;
        currentLineRenderer.endWidth = brushSize;
    }

    Vector2 GetInputPosition()
    {
        if (Input.touchCount > 0)
        {
            return m_camera.ScreenToWorldPoint(Input.GetTouch(0).position);
        }
        else
        {
            return m_camera.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    public void SetColor(int colorIndex)
    {
        // You can define your own color options here
        Color[] colors = { Color.red, Color.green, Color.blue, Color.yellow, Color.white };
        currentColor = colors[colorIndex];
        eraserToggle.isOn = (currentColor == Color.white); // If white, set eraser toggle to true
    }

    public void SetBrushSize(float size)
    {
        brushSize = size;
    }

    public void SetEraser(bool isEraser)
    {
        this.isEraser = isEraser;

        if (isEraser)
        {
            currentColor = Color.white; // Eraser color
        }
        else
        {
            // Set the color back to the selected color when not using the eraser
            SetColor(0); // Assuming the first color in the array is the default color
        }
    }
}
