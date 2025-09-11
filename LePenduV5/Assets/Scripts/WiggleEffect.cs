using UnityEngine;

public class WiggleEffect : MonoBehaviour
{
    [Header("Wiggle Settings")]
    [Tooltip("La vitesse du tremblement.")]
    public float speed = 5.0f;

    [Tooltip("L'amplitude du mouvement sur l'axe X.")]
    public float amountX = 0.1f;

    [Tooltip("L'amplitude du mouvement sur l'axe Y.")]
    public float amountY = 0.1f;

    [Tooltip("L'amplitude de la rotation.")]
    public float rotationAmount = 1.0f;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private float timeOffsetX;
    private float timeOffsetY;
    private float timeOffsetZ;

    void Start()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;

        timeOffsetX = Random.Range(0f, 10f);
        timeOffsetY = Random.Range(0f, 10f);
        timeOffsetZ = Random.Range(0f, 10f);
    }

    void Update()
    {
        float wiggleX = (Mathf.PerlinNoise(Time.time * speed + timeOffsetX, 0) * 2 - 1) * amountX;
        float wiggleY = (Mathf.PerlinNoise(0, Time.time * speed + timeOffsetY) * 2 - 1) * amountY;

        transform.localPosition = initialPosition + new Vector3(wiggleX, wiggleY, 0);

        float wiggleRot = (Mathf.PerlinNoise(Time.time * speed + timeOffsetZ, Time.time * speed + timeOffsetZ) * 2 - 1) * rotationAmount;

        transform.localRotation = initialRotation * Quaternion.Euler(0, 0, wiggleRot);
    }
}