using System.Collections.Generic;
using UnityEngine;

namespace Common.Dummies {

    public class DummyContainer : MonoBehaviour {
        public static DummyContainer instance;

        public void Initialize() {
            instance = this;
            imageDB = new Dictionary<string, DummyImage>();
            userDB = new Dictionary<string, DummyUser>();
            restaurantDB = new Dictionary<string, DummyRestaurant>();
            reviewDB = new Dictionary<string, DummyReview>();
            pathDB = new Dictionary<string, DummyPath>();
            imageDB.Clear();
            userDB.Clear();
            restaurantDB.Clear();
            reviewDB.Clear();
            foreach(DummyImage img in images)imageDB.Add(img.key,img);
            foreach(DummyUser user in users)userDB.Add(user.key,user);
            foreach(DummyRestaurant res in restaurants)restaurantDB.Add(res.key,res);
            foreach(DummyReview rev in reviews)reviewDB.Add(rev.key,rev);
            foreach(DummyPath path in paths)pathDB.Add(path.key,path);
        }

        public DummyImage[] images;
        public DummyUser[] users;
        public DummyRestaurant[] restaurants;
        public DummyReview[] reviews;
        public DummyPath[] paths;

        public Dictionary<string, DummyImage> imageDB;
        public Dictionary<string, DummyUser> userDB;
        public Dictionary<string, DummyRestaurant> restaurantDB;
        public Dictionary<string, DummyReview> reviewDB;
        public Dictionary<string, DummyPath> pathDB;


        public List<string> CountReviewOfUser(string userKey) {
            DummyUser user = userDB[userKey];
            List<string> ret = new List<string>();
            foreach (DummyReview rev in reviewDB.Values) {
                if (rev.writer.Equals(user)) ret.Add(rev.key);
            }

            return ret;
        }

        public string GetIdOfProfileImage(DummyRestaurant restaurant) {
            DummyReview tRev=null;
            foreach (DummyReview rev in reviewDB.Values) {
                if (rev.restaurant.key==restaurant.key) {
                    tRev = rev;

                    break;
                }
            }

            if (tRev == null)
                return "";

            return tRev.imageKeys[0].key;
        }
    }

}
