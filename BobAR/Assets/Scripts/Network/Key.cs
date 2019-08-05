namespace Network {

    public enum KeyType {
        Location,
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

        public double longitude,
                      latitude,
                      altitude;
        public static string ToString(Key k) {
            return k.type.ToString() + NetworkConstants.Diff + k.key;

        }
    }

}