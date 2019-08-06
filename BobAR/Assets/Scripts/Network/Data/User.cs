using System.Collections.Generic;

namespace Network.Data
{
    public class User:ILoadable
    {
        public string name;
        public string address;
        public string email;
        public string inputId;
        public string password;
        public int id;
        public Key imageKey;
        public List<Key> reviewIds;
        public List<Key> followings;
        public List<Key> followers;

        public ILoadable Load(string input) {
            //TODO 유저 패킷 분석
            return this;

        }
    }
}
