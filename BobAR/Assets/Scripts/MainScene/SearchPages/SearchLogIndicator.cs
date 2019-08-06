using TMPro;
using UnityEngine;

namespace MainScene.SearchPages {

    public class SearchLogIndicator : MonoBehaviour {
        public TextMeshProUGUI log,time;
        public string tag;

        public SearchLogIndicator Initialize(SearchLog input) {
            tag = input.tag;
            log.text = tag;
            time.text = input.time.ToString();//TODO 포맷잡아주기

            return this;
        }
    }

}
