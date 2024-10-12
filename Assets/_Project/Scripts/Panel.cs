using UnityEngine;

public class Panel : MonoBehaviour
{
    public Transform ComboTextsParent;
    
    // References for the texts to spawn
    public GameObject LeftJabText;
    public GameObject RightJabText;
    public GameObject LeftHookText;
    public GameObject RightHookText;
    public GameObject DuckText;

    // Settings
    public int Speed = 15;
    public int Lifetime = 20;
    
    public void Initialize(CubeType[] cubeTypes)
    {
        foreach (CubeType cubeType in cubeTypes)
        {
            GameObject textGameObject = GetText(cubeType);
            Instantiate(textGameObject, ComboTextsParent);
        }
        
        Destroy(gameObject, Lifetime);
    }
    
    void Update()
    {
        transform.Translate(-Vector3.forward * (Speed * Time.deltaTime));
    }

    public GameObject GetText(CubeType cubeType)
    {
        switch (cubeType)
        {
            case CubeType.LeftJab:
                return LeftJabText;
            case CubeType.RightJab:
                return RightJabText;
            case CubeType.LeftHook:
                return LeftHookText;
            case CubeType.RightHook:
                return RightHookText;
            case CubeType.Duck:
                return DuckText;
            default:
                return null;
        }
    }
}