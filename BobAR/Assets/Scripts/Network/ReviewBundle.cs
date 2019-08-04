using System.Collections.Generic;

namespace Network {

    public class ReviewBundle : ILoadable {
        public List<Key> keys = new List<Key>();

        public ILoadable Load(string input) {
            //TODO 리뷰 검색결과 패킷분석
            return this;
        }
    }

}