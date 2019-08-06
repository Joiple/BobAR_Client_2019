using System;
using System.Collections.Generic;
using CustomSceneManagement;
using DataManagement;
using NormalScene.Pages;
using UnityEngine;

namespace NormalScene {

    public enum PageType {
        UserPage,
        RestaurantPage,
        ReviewWritePage
    }

    public class NormalSceneManager : MonoBehaviour {
        public PrefabManager prefabs;

        public LinkedList<Page> pageStack = new LinkedList<Page>();
        public bool canBeExit = true;
        public Page NowPage => pageStack.Count > 0 ? pageStack.Last.Value : null;

        public void Start() {
            prefabs = GetComponent<PrefabManager>();
            PageType initialType = DataStorage.instance.GetItem<PageType>(DataStorage.InitialScene);
            AddPage(initialType).Initialize(this);
        }

        public void Update() {
            
        }
        public void Exit() {
            if (canBeExit && Input.GetKeyDown(KeyCode.Escape)) {
                if (pageStack.Count > 1) {
                    Page temp = pageStack.Last.Value;
                    pageStack.RemoveLast();
                    Destroy(temp);
                    temp = pageStack.Last.Value;
                    temp.gameObject.SetActive(true);
                } else {
                    CustomSceneManager.instance.LoadScene(0);
                }
            }
        }

        public Page AddPage(PageType type) {
            Page ret;

            switch (type) {
                case PageType.UserPage:
                    ret = Instantiate(prefabs.userPage);

                    break;

                case PageType.RestaurantPage:
                    ret = Instantiate(prefabs.restaurantPage);

                    break;

                case PageType.ReviewWritePage:
                    ret = Instantiate(prefabs.reviewWritePage);

                    break;

                default:
                    throw new Exception("잘못된 페이지타입");
            }

            Page temp = pageStack.Last.Value;
            pageStack.AddLast(ret);
            temp.gameObject.SetActive(false);

            return ret;
        }
    }

}