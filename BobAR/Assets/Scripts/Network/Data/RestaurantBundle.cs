using System.Collections.Generic;

namespace Network.Data {
    public class RestaurantBundle:ILoadable {
        public List<Key> keys=new List<Key>();
        public ILoadable Load(string input) {
            //TODO 검색결과 패킷분석
            return this;
        }
    }
}
