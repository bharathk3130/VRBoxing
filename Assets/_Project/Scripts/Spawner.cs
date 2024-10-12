using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public enum CubeType
{
    LeftJab,
    RightJab,
    LeftHook,
    RightHook,
    Duck
}

public class Spawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject PunchingCubeGO;
    public GameObject DodgingCubeGO;
    public GameObject Panel;
    
    [Header("Spawn Points")]
    public Transform PanelSpawnPoint;
    public Transform LeftJabSpawnPoint;
    public Transform RightJabSpawnPoint;
    public Transform LeftHookSpawnPoint;
    public Transform RightHookSpawnPoint;
    public Transform DuckSpawnPoint;

    [Header("Settings")]
    public float ComboInterval = 4;
    public float CubeInterval = 0.5f;
    
    // Combos
    static CubeType[] _combo1 = { CubeType.LeftJab, CubeType.Duck, CubeType.RightHook };
    static CubeType[] _combo2 = { CubeType.LeftJab, CubeType.LeftJab, CubeType.RightJab };
    static CubeType[] _combo3 = { CubeType.RightJab, CubeType.LeftJab, CubeType.Duck, CubeType.RightHook };
    static CubeType[] _combo4 = { CubeType.RightJab, CubeType.Duck, CubeType.LeftJab, CubeType.RightJab, CubeType.LeftHook };
    static CubeType[] _combo5 = { CubeType.RightJab, CubeType.Duck, CubeType.LeftJab, CubeType.RightJab, CubeType.LeftHook };

    // Make an array of combos and initialize it to the above 5 combos
    CubeType[][] _combos = { _combo1, _combo2, _combo3, _combo4, _combo5 };
    
    void Start()
    {
        InvokeRepeating("SpawnRandomCombo", 1, ComboInterval);
    }

    void SpawnRandomCombo()
    {
        int randomComboIndex = Random.Range(0, _combos.Length);
        CubeType[] combo = _combos[randomComboIndex];
        SpawnPanel(combo);
    }
    
    void SpawnPanel(CubeType[] cubeTypes)
    {
        GameObject panelInstance = Instantiate(Panel, PanelSpawnPoint.position, Quaternion.identity);
        Panel panel = panelInstance.GetComponent<Panel>();
        
        panel.Initialize(cubeTypes);
        
        StartCoroutine(SpawnCubes(cubeTypes));
    }
    
    IEnumerator SpawnCubes(CubeType[] cubeTypes)
    {
        yield return new WaitForSeconds(CubeInterval);
        
        foreach (CubeType cubeType in cubeTypes)
        {
            SpawnCube(cubeType);
            yield return new WaitForSeconds(CubeInterval);
        }
    }

    void SpawnCube(CubeType cubeType)
    {
        GameObject cube = GetCube(cubeType);
        Transform spawnPoint = GetSpawnPoint(cubeType);

        GameObject cubeInstance = Instantiate(cube, spawnPoint.position, spawnPoint.rotation);

        if (cubeType != CubeType.Duck)
        {
            PunchingCube punchingCube = cubeInstance.GetComponent<PunchingCube>();
            punchingCube.Initialize(cubeType);
        }
    }
    
    GameObject GetCube(CubeType cubeType)
    {
        if (cubeType == CubeType.Duck)
        {
            return DodgingCubeGO;
        } else
        {
            return PunchingCubeGO;
        }
    }
    
    Transform GetSpawnPoint(CubeType cubeType)
    {
        switch (cubeType)
        {
            case CubeType.LeftJab:
                return LeftJabSpawnPoint;
            case CubeType.RightJab:
                return RightJabSpawnPoint;
            case CubeType.LeftHook:
                return LeftHookSpawnPoint;
            case CubeType.RightHook:
                return RightHookSpawnPoint;
            case CubeType.Duck:
                return DuckSpawnPoint;
            default:
                return null;
        }
    }
}
