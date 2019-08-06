namespace Network {

    public enum KeyType {
        /// <summary>
        /// 위치/필터로 가게들 검색
        /// </summary>
        Location,
        /// <summary>
        /// 키로 유저 조회
        /// </summary>
        User,
        /// <summary>
        /// 키로 이미지 조회
        /// </summary>
        Image,
        /// <summary>
        /// 이미지 업로드 하고 키 회수
        /// </summary>
        WritingImage,
        /// <summary>
        /// 키로 리뷰 조회
        /// </summary>
        Review,
        /// <summary>
        /// 리뷰 업로드 하고 키 회수
        /// </summary>
        WritingReview,
        /// <summary>
        /// 키로 가게 조회
        /// </summary>
        Restaurant,
        /// <summary>
        /// 키로 팔로우 리스트 검색
        /// </summary>
        Followings,
        /// <summary>
        /// 예외처리
        /// </summary>
        None
    }

    
    public struct Key{
        public KeyType type;
        public int key;

        public double longitude,
                      latitude,
                      altitude;

        public byte[] data;

        public int[] siblingKeys;

        public string sequence;
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