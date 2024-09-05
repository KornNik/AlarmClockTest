using Helpers;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "ResourcesBundle", menuName = "Data/Resources")]
    class GameObjectsResourcesBundle : ScriptableObject
    {
        [SerializeField] private string MAIN_PREFABS_FOLER_NAME = "Prefabs/";
        [SerializeField] private string GUI_PREFABS_FOLDER_NAME = "GUI/";
        [SerializeField] private string TIME_CONTROL_NAME = "TimeControl";

        [SerializeField] private ScreenAssetPath[] _screenPath;

        public string GetScreenPath(ScreenTypes screenType)
        {
            string screenName = default;
            for (int i = 0; i < _screenPath.Length; i++)
            {
                if (_screenPath[i].Type == screenType) screenName = _screenPath[i].Path;
            }
            var fullPath = Helpers.Extensions.StringBuilderExtender.CreateString
                (MAIN_PREFABS_FOLER_NAME, GUI_PREFABS_FOLDER_NAME, screenName);
            return fullPath;
        }
        public string GetTimeControlPath()
        {
            var fullPath = Helpers.Extensions.StringBuilderExtender.CreateString
                (MAIN_PREFABS_FOLER_NAME, TIME_CONTROL_NAME);
            return fullPath;
        }
    }
}
