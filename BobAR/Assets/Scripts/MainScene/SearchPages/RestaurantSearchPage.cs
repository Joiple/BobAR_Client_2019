using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

namespace MainScene.SearchPages {

    public class RestaurantSearchPage : SearchPage {
        public List<SearchLog> logs = new List<SearchLog>();
        public List<SearchLogIndicator> indicators = new List<SearchLogIndicator>();
        public Transform LogList;
        public SearchLogIndicator logPrefab;
        public const string Diff = " ";
        public const int MaxLogLength = 10;
        public static string logPath;
        public TMP_InputField searchText;
        public MainSceneManager context;

        public void OnEnable() {
            if (File.Exists(logPath)) {
                logPath = Path.Combine(Application.persistentDataPath, "Saves", "SearchLog.json");

                using (FileStream logFile = File.Open(logPath, FileMode.Open)) {
                    StreamReader reader = new StreamReader(logFile);
                    logs = JsonUtility.FromJson<List<SearchLog>>(reader.ReadToEnd());
                    reader.Close();

                    foreach (SearchLog log in logs) {
                        SearchLogIndicator temp = Instantiate(logPrefab, LogList).Initialize(log);
                        indicators.Add(temp);
                    }
                }
            }
        }

        public void Search() {
            context.RefreshSearch();
            Close();
        }

        public override IEnumerator OpenCoroutine() {
            yield return base.OpenCoroutine();
            searchText.Select();
        }
    }
    

    public struct SearchLog : IComparable {
        public string tag,
                      time;

        public static SearchLog dummy = new SearchLog() {
            tag = "태그_예제",
            time = "12.31"
        };
        public override string ToString() {
            return time + tag;
        }

        public int CompareTo(object obj) => string.CompareOrdinal(ToString(), obj.ToString());
    }

}