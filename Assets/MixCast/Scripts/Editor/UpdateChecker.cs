/**********************************************************************************
* Blueprint Reality Inc. CONFIDENTIAL
* 2023 Blueprint Reality Inc.
* All Rights Reserved.
*
* NOTICE:  All information contained herein is, and remains, the property of
* Blueprint Reality Inc. and its suppliers, if any.  The intellectual and
* technical concepts contained herein are proprietary to Blueprint Reality Inc.
* and its suppliers and may be covered by Patents, pending patents, and are
* protected by trade secret or copyright law.
*
* Dissemination of this information or reproduction of this material is strictly
* forbidden unless prior written permission is obtained from Blueprint Reality Inc.
***********************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BlueprintReality.MixCast
{
    [InitializeOnLoad]
    public class UpdateChecker
    {
        const string VERSION_URL = "https://mixcast.me/downloads/latest_sdk_version.txt";
        const string PACKAGE_URL = "https://mixcast.me/sdk";

        const string IGNORE_TIME_PREF = "MixCast_UpdateIgnoreTime";

        const int LATER_COOLDOWN_HOURS = 72;
        const int UNCHANGED_COOLDOWN_MINUTES = 60;

        static DateTime NextCheckTime
        {
            get
            {
                if (EditorPrefs.HasKey(IGNORE_TIME_PREF))
                {
                    string valStr = EditorPrefs.GetString(IGNORE_TIME_PREF);
                    long val;
                    if (long.TryParse(valStr, out val))
                        return new DateTime(val);
                    else
                        return new DateTime();
                }
                else
                    return new DateTime();
            }
            set
            {
                EditorPrefs.SetString(IGNORE_TIME_PREF, value.Ticks.ToString());
            }
        }

        static UpdateChecker()
        {
            EditorApplication.update += Update;

            if (NextCheckTime < DateTime.UtcNow)
                coroutines.Add(CheckForUpdates(true));
        }

        static List<IEnumerator> coroutines = new List<IEnumerator>();

        private static void Update()
        {
            for (int i = coroutines.Count - 1; i >= 0; i--)
                if (!coroutines[i].MoveNext())
                    coroutines.RemoveAt(i);
        }

        [MenuItem("MixCast/Check for Updates", priority = 2)]
        public static void RunCheck()
        {
            coroutines.Add(CheckForUpdates(false));
        }

        static IEnumerator CheckForUpdates(bool headless)
        {
            string response = "";

#if UNITY_2018_3_OR_NEWER
            UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Get(VERSION_URL);
            UnityEngine.Networking.UnityWebRequestAsyncOperation get = www.SendWebRequest();

            while (!get.isDone)
                yield return null;

            response = www.downloadHandler.text;
#else
            WWW www = new WWW(VERSION_URL);

            while (!www.isDone)
                yield return null;

            response = www.text;
#endif

            if (!string.IsNullOrEmpty(www.error) || string.IsNullOrEmpty(response))
            {
                if (!headless)
                    EditorUtility.DisplayDialog("Oops!", "Couldn't contact the update server, try again later", "OK");
                yield break;
            }

            if (MixCastSdk.IsVersionBLaterThanVersionA(MixCastSdk.VERSION_STRING, response))
            {
                int result = EditorUtility.DisplayDialogComplex("Update Available", string.Format("MixCast has a new version available ({0})!", response), "Get It Now", "Get It Later", "Ignore MixCast updates");
                switch (result)
                {
                    case 0:
                        Application.OpenURL(PACKAGE_URL);
                        NextCheckTime = DateTime.UtcNow + new TimeSpan(0, UNCHANGED_COOLDOWN_MINUTES, 0);
                        yield break;
                    case 1:
                        NextCheckTime = DateTime.UtcNow + new TimeSpan(LATER_COOLDOWN_HOURS, 0, 0);
                        yield break;
                    case 2:
                        NextCheckTime = DateTime.UtcNow + new TimeSpan(999, 0, 0);
                        yield break;
                }
            }
            else if (MixCastSdk.IsVersionBLaterThanVersionA(response, MixCastSdk.VERSION_STRING))
            {
                NextCheckTime = DateTime.UtcNow + new TimeSpan(0, UNCHANGED_COOLDOWN_MINUTES, 0);

                if (!headless)
                    EditorUtility.DisplayDialog("Wow!", string.Format("Your MixCast version is ahead of the public release ({0})!", response), "OK");
            }
            else
            {
                NextCheckTime = DateTime.UtcNow + new TimeSpan(0, UNCHANGED_COOLDOWN_MINUTES, 0);

                if (!headless)
                    EditorUtility.DisplayDialog("Up To Date", string.Format("MixCast is up to date at version {0}!", MixCastSdk.VERSION_STRING), "OK");
            }
        }
    }
}
