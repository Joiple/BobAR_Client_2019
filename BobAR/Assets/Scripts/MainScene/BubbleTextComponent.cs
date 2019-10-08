using System.Collections;
using Common.Dummies;
using CustomSceneManagement;
using DataManagement;
using NormalScene;
using TMPro;
using UnityEngine;

namespace MainScene {

    public class BubbleTextComponent : MonoBehaviour {
        private int index;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Animator animator;
        [SerializeField] private float maximumLifeTime=5f;
        public bool needToRemove = false;
        
        public void Initialize(int input) {
            index = input;
            text.text = DummyContainer.instance.reviewDB[DummyContainer.instance.reviews[input].key].content;
            StartCoroutine(LifeCycle());
        }
        private IEnumerator LifeCycle() {
            animator.SetTrigger("init");
            float t = maximumLifeTime;
            while (t > 0 || needToRemove) {
                t -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            animator.SetTrigger("end");
            yield return new WaitForSeconds(2f);
            Destroy(gameObject);
        }
        public void ClickMenu() {
            DataStorage.instance.AddItem(DataStorageKeyset.InitialScene, PageType.ReviewDetailPage);
            DataStorage.instance.AddItem(DataStorageKeyset.NextReview, index);
            CustomSceneManager.instance.LoadScene(1);
        }
    }

}