using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] Transform _exclamationCanvas;
    [SerializeField] Exclamation _complimentPrefab;
    [SerializeField] Exclamation _negativeRemarkPrefab;
    [SerializeField] Exclamation _negativePointsPrefab;
    [SerializeField] ScoreUI _scoreUI;
    
    [Header("Exclamation Text Settings")]
    [SerializeField] Vector2 _exclamationPosXRange = new(-150, 150);
    [SerializeField] Vector2 _exclamationRotZRange = new(-15, 15);
    [SerializeField] Vector2 _exclamationScaleRange = new(0.7f, 1.1f);

    int _score;

    string[] _compliments = { "Perfect!", "Amazing!", "Great!" };
    string[] _negativeRemarks = { "Oops!", "Ouch!" };
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void AddScore(int increment)
    {
        _score += increment;
        _scoreUI.UpdateScore(_score, increment > 0);
        
        if (increment > 0)
        {
            ShowCompliment();
        }
        else
        {
            ShowNegativeRemark(increment);
        }
    }

    void ShowCompliment()
    {
        int randomComplimentIndex = Random.Range(0, _compliments.Length);
        Exclamation compliment = Instantiate(_complimentPrefab, _exclamationCanvas);
        compliment.Initialize(_compliments[randomComplimentIndex], _exclamationPosXRange, _exclamationRotZRange, _exclamationScaleRange);
    }
    
    void ShowNegativeRemark(int penalty)
    {
        int randomNegativeIndex = Random.Range(0, _negativeRemarks.Length);
        Exclamation negativeRemark = Instantiate(_negativeRemarkPrefab, _exclamationCanvas);
        float xPos = negativeRemark.Initialize(_negativeRemarks[randomNegativeIndex], _exclamationPosXRange, _exclamationRotZRange, _exclamationScaleRange);
        
        Exclamation negativePoints = Instantiate(_negativePointsPrefab, _exclamationCanvas);
        negativePoints.InitializeAt(penalty.ToString(), -xPos, _exclamationRotZRange, _exclamationScaleRange);
    }
}