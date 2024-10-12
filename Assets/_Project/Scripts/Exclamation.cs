using TMPro;
using UnityEngine;

public class Exclamation : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;

    // Spawn randomly
    public float Initialize(string exclamation, Vector2 exclamationPosXRange, Vector2 exclamationRotZRange,
        Vector2 exclamationScaleRange)
    {
        float randomXPos = Random.Range(exclamationPosXRange.x, exclamationPosXRange.y);
        
        _text.text = exclamation;
        _text.transform.localPosition = Vector3.right * randomXPos;
        _text.transform.localEulerAngles = Vector3.forward * Random.Range(exclamationRotZRange.x, exclamationRotZRange.y);
        _text.transform.localScale = Vector3.one * Random.Range(exclamationScaleRange.x, exclamationScaleRange.y);

        Destroy(gameObject, 2);

        return randomXPos;
    }
    
    // Spawn at a certain position
    public void InitializeAt(string exclamation, float exclamationXPos, Vector2 exclamationRotZRange,
        Vector2 exclamationScaleRange)
    {
        _text.text = exclamation;
        _text.transform.localPosition = Vector3.right * exclamationXPos;
        _text.transform.localEulerAngles = Vector3.forward * Random.Range(exclamationRotZRange.x, exclamationRotZRange.y);
        _text.transform.localScale = Vector3.one * Random.Range(exclamationScaleRange.x, exclamationScaleRange.y);

        Destroy(gameObject, 2);
    }
}