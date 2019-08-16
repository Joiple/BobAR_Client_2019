using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Common.Dummies;
using TMPro;
using UnityEngine;

namespace MainScene.SearchPages {

    public class RestaurantSearchPage : SearchPage {
        public List<SearchLog> logs = new List<SearchLog>();
        public List<string> logTags = new List<string>();
        public List<string> recs = new List<string>();
        public Transform logList;
        public SearchWordIndicator wordPrefab;
        public const string Diff = " ";
        public const int MaxLogLength = 10;
        public static string logPath;
        public TMP_InputField searchText;
        public TextMeshProUGUI indicator;
        public GameObject placeHolder;
        public MainSceneManager context;
        public bool nowIsLog = true;

        public void OnEnable() {
            Load();
            recs.Clear();
            foreach (DummyReview t in DummyContainer.instance.reviewDB.Values) {
                foreach (string tt in t.TagText.Split('#')) {
                    if (tt.Length == 0) continue;
                    if (recs.Contains(tt)) continue;
                    recs.Add(tt);
                }
            }
        }

        public void Load() {
            logPath = Path.Combine(Application.persistentDataPath, "Saves", "SearchLog.json");
            if (File.Exists(logPath)) {
                logs.Clear();
                using (FileStream logFile = File.Open(logPath, FileMode.Open)) {
                    StreamReader reader = new StreamReader(logFile);

                    logs.AddRange(JsonHelper.FromJson<SearchLog>(reader.ReadToEnd()));
                    reader.Close();
                }
            }
        }

        public void Save() {
            logPath = Path.Combine(Application.persistentDataPath, "Saves");
            DirectoryInfo di = new DirectoryInfo(logPath);
            if (!di.Exists) di.Create();
            logPath = Path.Combine(logPath, "SearchLog.json");

            using (FileStream logFile = File.Open(logPath, FileMode.Create)) {
                StreamWriter writer = new StreamWriter(logFile);
                string temp = JsonHelper.ToJson(logs.ToArray(), prettyPrint: true);
                writer.Write(temp);
                writer.Close();
            }
        }

        public void SearchButton() {
            foreach (string t in searchText.text.Split(' ')) {
                if (t.Length == 0) continue;

                SearchLog temp = new SearchLog() {
                    tag = t,
                    time = DateTime.Now.ToString("MM") + "." + DateTime.Now.ToString("dd")
                };

                bool added = false;

                for (int i = 0; i < logs.Count; i++) {
                    SearchLog target = logs[i];

                    if (target.tag == temp.tag) {
                        added = true;
                        target.time = DateTime.Now.ToString("mm") + "." + DateTime.Now.ToString("dd");

                        break;
                    }
                }

                if (!added) logs.Add(temp);
            }

            indicator.text = searchText.text;
            placeHolder.SetActive(searchText.text.Length == 0);
            Save();
            Close();
        }

        public void Search() {
            context.RefreshSearch();
            Close();
        }

        public void ChangeTab(bool isLog) {
            if (isLog) {
                if (nowIsLog) return;
                foreach (Transform t in logList) Destroy(t.gameObject);

                foreach (SearchLog log in logs) {
                    SearchWordIndicator temp = Instantiate(wordPrefab, logList).Initialize(this, log);
                }

                nowIsLog = true;
            } else {
                if (!nowIsLog) return;
                foreach (Transform t in logList) Destroy(t.gameObject);

                foreach (string rec in recs) {
                    if (rec.Length == 0) continue;
                    SearchWordIndicator temp = Instantiate(wordPrefab, logList).Initialize(this, rec);
                }

                nowIsLog = false;
            }
        }

        public override IEnumerator CloseCoroutine() {
            yield return base.CloseCoroutine();
            indicator.text = searchText.text;
        }

        public override IEnumerator OpenCoroutine() {
            yield return base.OpenCoroutine();
            searchText.Select();
        }
    }

    [Serializable]
    public class SearchLog : IComparable {
        public string tag,
                      time;

        public static SearchLog dummy = new SearchLog() {
            tag = "태그_예제",
            time = "12.31"
        };

        public override string ToString() {
            return time + tag;
        }

        public override bool Equals(object obj) {
            if (obj is SearchLog log) return tag == log.tag;

            return base.Equals(obj);
        }

        public int CompareTo(object obj) => string.CompareOrdinal(ToString(), obj.ToString());
    }


    public static class JsonHelper {
        public static T[] FromJson<T>(string json) {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);

            return wrapper.items;
        }

        public static string ToJson<T>(T[] array) {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.items = array;

            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint) {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.items = array;

            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T> {
            public T[] items;
        }
    }

}