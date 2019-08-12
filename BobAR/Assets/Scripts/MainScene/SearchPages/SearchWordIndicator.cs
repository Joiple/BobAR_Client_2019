using System.Linq;
using TMPro;
using UnityEngine;

namespace MainScene.SearchPages {

    public class SearchWordIndicator : MonoBehaviour {
        private RestaurantSearchPage manager;
        public TextMeshProUGUI log,time;
        public string logTag;

        public SearchWordIndicator Initialize(RestaurantSearchPage manager) {
            this.manager = manager;
            return Initialize(manager,SearchLog.dummy);
        }

        public SearchWordIndicator Initialize(RestaurantSearchPage manager,string word) {
            this.manager = manager;
            log.text = word;
            time.text = "";
            logTag = word;
            return this;
        }
        public SearchWordIndicator Initialize(RestaurantSearchPage manager,SearchLog input) {
            logTag = input.tag;
            log.text = logTag;
            time.text = input.time.ToString();//TODO 포맷잡아주기

            return this;
        }

        public void Click() {
            manager.searchText.text=logTag;
        }
    }

}
