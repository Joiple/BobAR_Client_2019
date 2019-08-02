using System;
using System.Collections.Generic;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

namespace Common
{
    public class User:ILoadable
    {
        public string name;
        public string address;
        public string email;
        public string inputId;
        public string password;
        public int id;
        public Image profile;
        public List<int> reviewIds;
        public List<Review> reviews;
        public List<int> followings;
        public List<int> followers;
    }
}
