using UnityEngine;

namespace Controllers
{
    sealed class SettingsController 
    {
        public SettingsController()
        {
            QualitySettings.SetQualityLevel(0);
            QualitySettings.vSyncCount = 1;
            Application.targetFrameRate = 30;

        }
    }
}
