using System;
using System.Text;

namespace Network.Data {

    public class ImageUploadPacket :Loadable {
        public new const int Code = 10;

        public static string ParsePacket(byte[] file) {
            return Encoding.Default.GetString(file);
        }

        public bool status;

        public override Loadable Load(string input) {
            string[] data = input.Split(new[] {
                Diff
            }, StringSplitOptions.None);

            status= Convert.ToInt32(data[0])==1;
            return this;
        }
    }

}