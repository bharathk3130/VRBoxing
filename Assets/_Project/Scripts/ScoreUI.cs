using System;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] GameObject _score;

    TextMeshProUGUI _scoreText;
    Animator _scoreAnim;
    
    static readonly int _bounceHash = Animator.StringToHash("Bounce");

    void Awake()
    {
        _scoreText = _score.GetComponent<TextMeshProUGUI>();
        _scoreAnim = _score.GetComponent<Animator>();
    }

    public void UpdateScore(int score, bool bounce)
    {
        _scoreText.text = score.ToString();
        
        if (bounce)
        {
            Bounce();
        }
    }
    
    [ContextMenu("Bounce!")]
    void Bounce()
    {
        _scoreAnim.SetTrigger(_bounceHash);
    }
}