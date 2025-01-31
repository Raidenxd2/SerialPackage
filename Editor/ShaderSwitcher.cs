using UnityEngine;
using UnityEditor;
using System.IO;

namespace SerialPackage.Edito
{
    [InitializeOnLoad]
    public class ShaderSwitcher : UnityEditor.Editor
    {
        static ShaderSwitcher()
        {
            if (!File.Exists("Assets/build_target"))
            {
                Debug.Log("(SerialPackage) Creating Assets/build_target");

                File.WriteAllText("Assets/build_target", EditorUserBuildSettings.activeBuildTarget.ToString());
                return;
            }

            if (File.ReadAllText("Assets/build_target") != EditorUserBuildSettings.activeBuildTarget.ToString())
            {
                SwitchShadersCurrent();
            }
        }

        [MenuItem("Serial Package/Force Platform Shader Switch (current)")]
        public static void SwitchShadersCurrent()
        {
            SwitchShaders();
        }

        [MenuItem("Serial Package/Force Platform Shader Switch (Android)")]
        public static void SwitchShadersAndroid()
        {
            SwitchShaders(true);
        }

        [MenuItem("Serial Package/Force Platform Shader Switch (PC)")]
        public static void SwitchShadersPC()
        {
            SwitchShaders(false, true);
        }

        private static void SwitchShaders(bool forceAndroid = false, bool forcePC = false)
        {
            Debug.Log("(SerialPackage) build_target does not match the current build target!");

            if (forceAndroid && forcePC)
            {
                Debug.LogError("(SerialPackage) Cannot force Android and PC shaders!");
                return;
            }

            BuildTarget bt = EditorUserBuildSettings.activeBuildTarget;

            ShaderSwitchSO so = (ShaderSwitchSO)AssetDatabase.LoadAssetAtPath("Assets/ShaderSwitch.asset", typeof(ShaderSwitchSO));

            bool standalone = false;
            bool mobile = false;

            File.Delete("Assets/build_target");
            File.WriteAllText("Assets/build_target", bt.ToString());

            if (bt == BuildTarget.StandaloneWindows64 || bt == BuildTarget.StandaloneOSX || bt == BuildTarget.StandaloneLinux64)
            {
                standalone = true;
            }
            if (bt == BuildTarget.Android || forceAndroid)
            {
                standalone = false;
                mobile = true;
            }

            if (forcePC)
            {
                standalone = true;
                mobile = false;
            }

            foreach (var item in so.shaderSwitches)
            {
                if (standalone)
                {
                    item.material.shader = item.PCShader;
                }
                else if (mobile)
                {
                    item.material.shader = item.MobileShader;
                }
            }
        }
    }
}