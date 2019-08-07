using TMPro;
using UnityEngine;

namespace MainScene.SearchPages {

    public class SearchLogIndicator : MonoBehaviour {
        public TextMeshProUGUI log,time;
        public string logTag;

        public SearchLogIndicator Initialize() {
            return Initialize(SearchLog.dummy);
        }
        public SearchLogIndicator Initialize(SearchLog input) {
            logTag = input.tag;
            log.text = logTag;
            time.text = input.time.ToString();//TODO 포맷잡아주기

            return this;
        }
    }

}
