using System;

namespace Network.Data {

    public class ImageUploadHeaderPacket :Loadable {
        public new const int Code = 7;
        public string fileName;
        public static string ParsePacket(string userid, string restaurantId) {
            return Code + Diff + userid + Diff + restaurantId + Diff;
        }

        public override Loadable Load(string input) {
            string[] data = input.Split(new[] {
                Diff
            }, StringSplitOptions.None);

            fileName = data[0];
            return this;
        }
    }

}