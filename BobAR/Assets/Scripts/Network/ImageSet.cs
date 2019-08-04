using UnityEngine;

namespace Network{

    public class ImageSet : ILoadable {
        public Sprite sprite;
        public ILoadable Load(string input) {
            //TODO 이미지 패킷분석
            return this;
        }
    }

}