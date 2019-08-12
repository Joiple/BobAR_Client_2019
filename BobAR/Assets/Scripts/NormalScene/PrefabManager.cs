using NormalScene.Pages;
using UnityEngine;

namespace NormalScene {

    [RequireComponent(typeof(NormalSceneManager))]
    public class PrefabManager : MonoBehaviour {
        public Page restaurantPage,
                    userPage,
                    reviewWritePage,
                    reviewDetailPage,
                    followerPage;
    }

}