using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class FXManager : MonoBehaviour
{
    public static FXManager Instance;

    public string explosionPCKey = "Explosion_PC";
    public string explosionQuestKey = "Explosion_Quest";

    public string impactPCKey = "Impact_PC";
    public string impactQuestKey = "Impact_Quest";

    public string muzzlePCKey = "ParticleTir_PC";
    public string muzzleQuestKey = "ParticleTir_Quest";

    private AsyncOperationHandle<GameObject> explosionHandle;
    private AsyncOperationHandle<GameObject> impactHandle;
    private AsyncOperationHandle<GameObject> muzzleHandle;

    private GameObject explosionPrefab;
    private GameObject impactPrefab;
    private GameObject muzzlePrefab;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        LoadFX();
    }

    private void LoadFX()
    {
#if UNITY_ANDROID
        string explosionKey = explosionQuestKey;
        string impactKey = impactQuestKey;
        string muzzleKey = muzzleQuestKey;
#else
        string explosionKey = explosionPCKey;
        string impactKey = impactPCKey;
        string muzzleKey = muzzlePCKey;
#endif

        explosionHandle = Addressables.LoadAssetAsync<GameObject>(explosionKey);
        impactHandle = Addressables.LoadAssetAsync<GameObject>(impactKey);
        muzzleHandle = Addressables.LoadAssetAsync<GameObject>(muzzleKey);

        explosionHandle.Completed += h => explosionPrefab = h.Result;
        impactHandle.Completed += h => impactPrefab = h.Result;
        muzzleHandle.Completed += h => muzzlePrefab = h.Result;
    }

    public GameObject GetExplosionFX() => explosionPrefab;
    public GameObject GetImpactFX() => impactPrefab;
    public GameObject GetMuzzleFX() => muzzlePrefab;

    private void OnDestroy()
    {
        if (explosionHandle.IsValid()) Addressables.Release(explosionHandle);
        if (impactHandle.IsValid()) Addressables.Release(impactHandle);
        if (muzzleHandle.IsValid()) Addressables.Release(muzzleHandle);
    }
}
