namespace Network {

    public enum KeyType {
        Location,
        User,
        Image,
        WritingImage,
        Review,
        WritingReview,
        Restaurant,
        Followings,
        None
    }
    public struct Key {
        public KeyType type;
        public int key;

        public double longitude,
                      latitude,
                      altitude;

        public byte[] data;
        public static string ToString(Key k) {
            string ret = "";

            switch (k.type) {//TODO 송신 패킷 직렬화
                default:
                    ret+=k.type.ToString() + NetworkConstants.Diff + k.key;
                    
                    break;
            }
            return ret;

        }
    }

}