using UnityEngine;

namespace KillItMyself.Android.Runtime
{
    public class AndroidFPS : MonoBehaviour
    {
        private void Start()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = (int)(Screen.currentResolution.refreshRateRatio.numerator + 1);
        }
    }
}