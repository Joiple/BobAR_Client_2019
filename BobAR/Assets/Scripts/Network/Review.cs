using System.Collections.Generic;

namespace Network
{
    public class Review:ILoadable
    {
        public List<Key> pictures=new List<Key>();
        public Key author;
        public string content;
        public Key restaurant;
        public List<Key> followers;


        public ILoadable Load(string input) {
            //TODO 리뷰 패킷 분석
            return this;
        }
    }
}