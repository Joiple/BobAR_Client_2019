using UnityEngine;
using System.Collections.Generic;
using Common.Dummies;

public class DummyContainer : MonoBehaviour {
    public static DummyContainer instance;

    public void Awake() {
        instance = this;
    }

    public void Start() {
        imageDB = new Dictionary<string, DummyImage>();
        userDB = new Dictionary<string, DummyUser>();
        restaurantDB = new Dictionary<string, DummyRestaurant>();
        reviewDB = new Dictionary<string, DummyReview>();
        foreach(DummyImage img in images)imageDB.Add(img.key,img);
        foreach(DummyUser user in users)userDB.Add(user.key,user);
        foreach(DummyRestaurant res in restaurants)restaurantDB.Add(res.key,res);
        foreach(DummyReview rev in reviews)reviewDB.Add(rev.key,rev);
    }

    public DummyImage[] images;
    public DummyUser[] users;
    public DummyRestaurant[] restaurants;
    public DummyReview[] reviews;

    public Dictionary<string, DummyImage> imageDB;
    public Dictionary<string, DummyUser> userDB;
    public Dictionary<string, DummyRestaurant> restaurantDB;
    public Dictionary<string, DummyReview> reviewDB;
}
