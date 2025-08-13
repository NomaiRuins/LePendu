using UnityEngine;

public class WobbleBreathing : MonoBehaviour
{
    [Header("Wobble")]
    public float rotationAmount = 2f; 
    public float rotationSpeed = 2f;  

    [Header("Breathing")]
    public float scaleAmount = 0.02f; 
    public float breathingSpeed = 1f; 

    private Vector3 initialScale;

    void Start()
    {
        initialScale = transform.localScale;
    }

    void Update()
    {
        float wobble = Mathf.Sin(Time.time * rotationSpeed) * rotationAmount;
        transform.rotation = Quaternion.Euler(0, 0, wobble);

        float breath = Mathf.Sin(Time.time * breathingSpeed) * scaleAmount;
        transform.localScale = initialScale + Vector3.one * breath;
    }
}
