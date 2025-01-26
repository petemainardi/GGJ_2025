using UnityEngine;

/// ===========================================================================================
/// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
/// ===========================================================================================
/// <summary>
/// Base class for a singleton object type that exists in the scene, of which only one should
/// exist, and (by default) persists between scenes.
/// </summary>
/// ===========================================================================================
/// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
/// ===========================================================================================
public abstract class SceneSingleton<T> : MonoBehaviour where T : SceneSingleton<T>
{
    protected virtual bool IsPersistent { get; } = true;

    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                    _instance = new GameObject(typeof(T).Name).AddComponent<T>();
                if (_instance.IsPersistent && Application.isPlaying)
                    DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }
}
/// ===========================================================================================
/// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
/// ===========================================================================================