using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T:SingletonMonoBehaviour<T>
{
    private static T _Instance = null;
    private static bool _IsSingletonInitialized = false;
    private static bool _IsDestory = false;

    public static T Instance
    {
        get { return GetInstance(); }
    }

    public static T GetInstance()
    {
        if (!_IsDestory && _Instance == null)
        {
            _Instance = FindObjectOfType<T>();
            if (_Instance == null)
            {
                GameObject gameObject = new GameObject(typeof(T).Name);
                _Instance = gameObject.AddComponent<T>();
                _Instance.SingletonInitialize();
            }
        }
        return _Instance;
    }

    public static bool IsInstanced { get { return _Instance != null; } }

    [SerializeField]
    private bool _isDontDestroy = true;

    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = gameObject.GetComponent<T>();
        }
        else if (_Instance != this)
        {
            _Instance.OnDestroy();
            _Instance = gameObject.GetComponent<T>();
        }

        if (_isDontDestroy)
        {
            DontDestroyOnLoad(this);
        }

        SingletonInitialize();
    }

    private void OnDestroy()
    {
        if (this == _Instance)
        {
            _Instance = null;
            _IsDestory = true;
        }

        OnFinalize();
        Destroy(this);
    }
    
    /// <summary>
    /// 一度だけ初期化処理をする
    /// </summary>
    private void SingletonInitialize()
    {
        if (!_IsSingletonInitialized)
        {
            OnInitialize();
            _IsSingletonInitialized = true;
        }
    }

    /// <summary>
    /// 初期化のときに呼ばれます
    /// </summary>
    protected virtual void OnInitialize()
    {

    }
    
    /// <summary>
    /// 破棄されるときに呼ばれます
    /// </summary>
    protected virtual void OnFinalize()
    {

    }
}
