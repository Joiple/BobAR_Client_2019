using System;
using System.Collections.Generic;
using System.IO;
using ARComponents;
using Network;
using Network.Data;
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
            string[] tags = searchText.text.Split(new[] {
                Diff
            }, StringSplitOptions.None);

            foreach (string tTag in tags) {
                bool duplicated = false;

                SearchLog t = new SearchLog() {
                    tag = tTag,
                    time = DateTime.Now.ToString("m.dd")
                };

                for (int j = 0; j < logs.Count; j++)
                    if (logs[j].tag == tTag) {
                        logs[j] = t;

                        duplicated = true;

                        break;
                    }

                if (!duplicated) {
                    logs.Add(t);
                }
            }

            logs.Sort();
            if (logs.Count > MaxLogLength) logs.RemoveRange(MaxLogLength - 1, logs.Count - 1);

            using (FileStream logFile = File.Open(logPath, FileMode.Create)) {
                StreamWriter t = new StreamWriter(logFile);
                t.Write(JsonUtility.ToJson(logs));
                t.Close();
            }

            Key searchKey = new Key() {
                type = KeyType.Location,
                sequence = searchText.text,
                altitude = GpsManager.instance.initialAlt,
                longitude = GpsManager.instance.initialLon,
                latitude = GpsManager.instance.initialLat
            };

            Client<RestaurantBundle> searchClient = new Client<RestaurantBundle>(searchKey.ToString());
        }
    }


    public struct SearchLog : IComparable {
        public string tag,
                      time;

        public override string ToString() {
            return time + tag;
        }

        public int CompareTo(object obj) => string.CompareOrdinal(ToString(), obj.ToString());
    }

}