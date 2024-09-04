using UnityEngine;
using UnityEngine.SceneManagement;

namespace Helpers
{
    class PersistanceSingleton<T> : MonoBehaviour where T : PersistanceSingleton<T>
    {
        private bool _alive = true;
        private static T _instance;

        public static bool IsAlive
        {
            get
            {
                if (_instance == null)
                    return false;
                return _instance._alive;
            }
        }

        void OnDestroy() { _alive = false; }
        void OnApplicationQuit() { _alive = false; }
        private void OnEnable()
        {
            if (!_instance)
            {
                T[] managers = FindObjectsOfType(typeof(T)) as T[];

                if (managers != null)
                {
                    if (managers.Length == 1)
                    {
                        _instance = managers[0];
                        DontDestroyOnLoad(_instance.gameObject);
                        return;
                    }
                    else if (managers.Length > 1)
                    {
                        DeleteExtraManagers(managers);
                        _instance = managers[0];
                        DontDestroyOnLoad(_instance.gameObject);
                    }
                }
            }
            if (_instance)
            {
                SceneManager.activeSceneChanged += CheckIsObjectSingle;
            }
        }
        private void OnDisable()
        {
            if (_instance)
            {
                SceneManager.activeSceneChanged -= CheckIsObjectSingle;
            }
        }
        protected static PersistanceSingleton<T> _instanceSingleton
        {
            get
            {
                if (!_instance)
                {
                    T[] managers = FindObjectsOfType(typeof(T)) as T[];

                    if (managers != null)
                    {
                        if (managers.Length == 1)
                        {
                            _instance = managers[0];
                            return _instance;
                        }
                        else if (managers.Length > 1)
                        {
                            for (int i = 0; i < managers.Length; ++i)
                            {
                                T manager = managers[i];
                                Destroy(manager.gameObject);
                            }
                        }
                    }

                    GameObject gameObject = new GameObject(typeof(T).Name, typeof(T));
                    _instance = gameObject.GetComponent<T>();
                    DontDestroyOnLoad(_instance.gameObject);
                }
                return _instance;
            }
            set { _instance = value as T; }
        }
        private void CheckIsObjectSingle(Scene originScene, Scene targetScene)
        {
            if (targetScene.isLoaded)
            {
                T[] managers = FindObjectsOfType(typeof(T)) as T[];

                if (managers != null)
                {
                    if (managers.Length > 1)
                    {
                        DeleteExtraManagers(managers);
                    }
                }
            }
        }
        private void DeleteExtraManagers(T[] managers)
        {
            for (int i = 1; i < managers.Length; ++i)
            {
                if (managers[i] != _instance)
                {
                    T manager = managers[i];
                    Destroy(manager.gameObject);
                }
            }
        }
        public static T GetInstance()
        {
            var currentInstance = _instanceSingleton as T;
            return currentInstance;
        }
    }
}
