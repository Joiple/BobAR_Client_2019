using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

namespace MainScene.SearchPages {

    public class RestaurantSearchPage : SearchPage {
        public List<SearchLog> logs = new List<SearchLog>();
        public List<string> recs = new List<string>();
        public Transform logList;
        public SearchWordIndicator wordPrefab;
        public const string Diff = " ";
        public const int MaxLogLength = 10;
        public static string logPath;
        public TMP_InputField searchText;
        public MainSceneManager context;
        public bool nowIsLog = true;
        public void OnEnable() {
            if (File.Exists(logPath)) {
                logPath = Path.Combine(Application.persistentDataPath, "Saves", "SearchLog.json");

                using (FileStream logFile = File.Open(logPath, FileMode.Open)) {
                    StreamReader reader = new StreamReader(logFile);
                    logs = JsonUtility.FromJson<List<SearchLog>>(reader.ReadToEnd());
                    reader.Close();
                }
            }
            //TODO recs에 추천 넣기
        }

        public void Search() {
            context.RefreshSearch();
            Close();
        }

        public void ChangeTab(bool isLog) {
            
            if (isLog) {
                if (nowIsLog) return;
                foreach(Transform t in logList)Destroy(t.gameObject);
                foreach (SearchLog log in logs) {
                    SearchWordIndicator temp = Instantiate(wordPrefab, logList).Initialize(this, log);
                }
            } else {
                if (!nowIsLog) return;

                foreach (string rec in recs) {
                    SearchWordIndicator temp = Instantiate(wordPrefab, logList).Initialize(this, rec);
                }
            }
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