using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NormalScene.Pages.ReviewDetailView {

    public class ReviewDetailPageManager : Page {
        public Image thumbnailImage,
                      profileImage;

        public TextMeshProUGUI followingNumber,
                               userName,
                               content,
                               totalAvgScore,
                               tasteScore,
                               clearanceScore,
                               kindnessScore,
                               atmosphereScore,
                               efficiencyScore,
                               date,
                               reviewTag;
        public ReviewDetailPageManager Initialize(string id = "") {
            StartCoroutine(InitializeInternal());
            return this;
        }

        private IEnumerator InitializeInternal() {
            //TODO 리뷰 정보 수신
            thumbnailImage.sprite = null;
            profileImage.sprite = null;
            followingNumber.text = ""+123;
            userName.text = "모먹지";
            content.text = "여하튼 맛있음";
            totalAvgScore.text = "3.6";
            tasteScore.text = "4점";
            clearanceScore.text = "4점";
            kindnessScore.text = "3점";
            atmosphereScore.text = "4점";
            efficiencyScore.text = "3점";
            reviewTag.text = "#모티집 #서면 #밀푀유나베 #서면맛짐";
            date.text = "19.07.08";
            yield return null;
        }

        public void Follow() {

        }
    }

}
