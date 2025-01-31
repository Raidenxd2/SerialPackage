using UnityEngine;

namespace SerialPackage.Runtime
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