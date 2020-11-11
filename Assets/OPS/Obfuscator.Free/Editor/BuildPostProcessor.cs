#if UNITY_EDITOR

// System
using System;

// Unity
using UnityEditor;
using UnityEditor.Callbacks;

#if UNITY_2018_1_OR_NEWER
using UnityEditor.Build.Reporting;
#endif

#if UNITY_5_6_OR_NEWER
using UnityEditor.Build;
#endif

namespace OPS.Obfuscator
{

#if UNITY_2018_2_OR_NEWER
	public class BuildPostProcessor : IPostBuildPlayerScriptDLLs
#elif UNITY_2018_1_OR_NEWER
	public class BuildPostProcessor : IPostprocessBuildWithReport
#elif UNITY_5_6_OR_NEWER
    public class BuildPostProcessor : IPostprocessBuild
#else
    public class BuildPostProcessor
#endif
    {
        //Defines if an Obfuscation Process took place.
        private static bool hasObfuscated = false;

        //The Main Obfuscation Program
        private static OPS.Obfuscator.Obfuscator obfuscator;

        //Check if extern assemblys got obfuscated.
        private static bool assemblysNeedReverting = false;
        
        //Revert Unity Assets and external Assemblies, if postprocess got not called or update got cleared!
        [InitializeOnLoad]
        public static class OnInitializeOnLoad
        {
            static OnInitializeOnLoad()
            {
                EditorApplication.update += RestoreAssemblies;
            }
        }

        //Obfuscate Assemblies after first scene build.
        [PostProcessScene(1)]
        public static void OnPostProcessScene()
        {
            if (!hasObfuscated)
            {
                if (BuildPipeline.isBuildingPlayer && !EditorApplication.isPlayingOrWillChangePlaymode)
                {
                    try
                    {
                        UnityEditor.EditorApplication.LockReloadAssemblies();

                        //Load Settings
                        var var_Settings = new Setting.Settings();
                        var_Settings.LoadSettings();

#if UNITY_2018_2_OR_NEWER
                    var_Settings.IsLaterUnity_2018_2 = true;
#endif

                        //Init
                        obfuscator = new Obfuscator(var_Settings);

                        //Obfuscate Assemblies
                        bool var_NoError = obfuscator.Obfuscate(UnityEditor.EditorUserBuildSettings.activeBuildTarget, new System.Collections.Generic.List<string>());
                        if (var_NoError)
                        {

                            //
                            EditorApplication.update += RestoreAssemblies;
                            assemblysNeedReverting = true;

                            //Save Assemblies
                            obfuscator.Save();
                        }
                        hasObfuscated = true;
                    }
                    catch(Exception e)
                    {
                        UnityEngine.Debug.LogError("[OPS] Error: " + e.ToString());
                    }
                    finally
                    {
                        UnityEditor.EditorApplication.UnlockReloadAssemblies();
                    }
                }
            }
        }

        public int callbackOrder
        {
            get
            {
                return 1;
            }
        }

        //Revert external Assemblies. 
#if UNITY_2018_2_OR_NEWER
		public void OnPostBuildPlayerScriptDLLs(BuildReport report)
#elif UNITY_2018_1_OR_NEWER
		public void OnPostprocessBuild(BuildReport report)
#elif UNITY_5_6_OR_NEWER
        public void OnPostprocessBuild(BuildTarget _Target, string _PathToBuiltProject)
#else
		[PostProcessBuildAttribute(1)]
		private static void OnPostprocessBuild(BuildTarget _Target, string _PathToBuiltProject)
#endif
        {
            if (hasObfuscated)
            {
                if (assemblysNeedReverting)
                {
                    RestoreAssemblies();
                }
            }

            RefreshAll();
        }


        private static void RestoreAssemblies()
        {
            if (BuildPipeline.isBuildingPlayer == false)
            {
                try
                {
                    assemblysNeedReverting = false;

                    if (obfuscator == null)
                    {
                        return;
                    }

                    obfuscator.RestoreTemporaryAssemblies();
                    EditorApplication.update -= RestoreAssemblies;
                }
                catch (Exception e)
                {
                    assemblysNeedReverting = true;
                    UnityEngine.Debug.LogWarning("[OPS.OBF] " + e.ToString());
                }
            }
        }

        public static void ManualRestore()
        {
            RestoreAssemblies();
        }

        private static void RefreshAll()
        {
            hasObfuscated = false;
        }
    }
}
#endif