using System;
using System.Collections;
using UnityEngine;

public class DissolveEffect : MonoBehaviour
{
    [SerializeField] Material _dissolveMaterial;
    [SerializeField] float _dissolveDuration = 1.0f;
    [SerializeField] Renderer _cubeRenderer;

    static readonly int _shaderDissolveValue = Shader.PropertyToID("_DissolveAmount");
    MaterialPropertyBlock _mpb;

    Action _onComplete;

    void Awake()
    {
        _mpb = new MaterialPropertyBlock();
    }

    public void Play(Action callback = null)
    {
        _onComplete = callback;
        _cubeRenderer.material = _dissolveMaterial;
        StartCoroutine(Dissolve(_cubeRenderer));
    }

    IEnumerator Dissolve(Renderer targetRenderer)
    {
        float elapsedTime = 0f;

        while (elapsedTime < _dissolveDuration)
        {
            float dissolveValue = Mathf.Lerp(0f, 1f, elapsedTime / _dissolveDuration);
            UpdateDissolveValue(targetRenderer, dissolveValue);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        UpdateDissolveValue(targetRenderer, 1f);
        _onComplete?.Invoke();
    }

    void UpdateDissolveValue(Renderer targetRenderer, float value)
    {
        targetRenderer.GetPropertyBlock(_mpb);
        _mpb.SetFloat(_shaderDissolveValue, value);
        targetRenderer.SetPropertyBlock(_mpb);
    }
}