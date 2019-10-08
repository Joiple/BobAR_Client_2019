using System.Collections;
using Common.Dummies;
using UnityEngine;

namespace MainScene {

    public class BubbleText :MonoBehaviour {
        [SerializeField] private Transform listGameObject;
        [SerializeField] private BubbleTextComponent prefab;
        public void Start() => Initialize();
        public void Initialize() {
            StartCoroutine(RandomGeneration());
        }
        private IEnumerator RandomGeneration() {
            while (true) {
                Instantiate(prefab, listGameObject).Initialize((int) (Random.value*(DummyContainer.instance.reviews.Length-1)));
                yield return new WaitForSeconds(Random.value * 3 + .5f);
            }
        }
    }

}