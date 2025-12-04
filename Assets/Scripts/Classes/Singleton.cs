using UnityEngine;

namespace Assets.Scripts.Classes
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance is null)
                {
                    _instance = FindObjectOfType<T>();
                    if (_instance is null)
                    {
                        var singletonObject = new GameObject(typeof(T).Name);
                        _instance = singletonObject.AddComponent<T>();
                    }
                }
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance is not null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}