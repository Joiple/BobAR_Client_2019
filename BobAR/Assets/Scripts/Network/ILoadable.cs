using System;

namespace Network {

    public class Loadable {
        public const int Code = -1;
        public const string Diff = "%%";
        
        public virtual Loadable Load(string input) {
            throw new NotImplementedException();
        }

        public virtual Loadable LoadBinary(byte[] input) {
            throw new NotImplementedException();
        }
    }

}