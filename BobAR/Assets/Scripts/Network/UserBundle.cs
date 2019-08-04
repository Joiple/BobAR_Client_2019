using System.Collections.Generic;

namespace Network {

    public class UserBundle : ILoadable {
        public List<Key> keys = new List<Key>();

        public ILoadable Load(string input) {
            //TODO 유저 검색결과 패킷분석
            return this;
        }
    }

}