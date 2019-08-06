

namespace Network.Data {

    public class ImageUpload :ILoadable {
        public bool result;
        public int key;
        public ILoadable Load(string input) {
            //TODO 이미지 업로드 결과 패킷 분석
            return this;
        }
    }

}