using System;

namespace Kztek.Object
{
    public class RegisterMode
    {
        [Flags]
        public enum EmRegisterMode
        {
            Card = 1,
            Finger = 2,
            Face = 4
        }
    }
}
