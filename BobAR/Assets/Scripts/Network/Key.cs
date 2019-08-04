namespace Network {

    public enum KeyType {
        User,
        Image,
        Review,
        Restaurant,
        Followings,
        None
    }
    public struct Key {
        public KeyType type;
        public int key;

        public static string ToString(Key k) {
            return k.type.ToString() + NetworkConstants.diff + k.key;

        }
    }

}