using System;

namespace AppCore
{
    public class ObjectTest : IObjectTest
    {
        public bool IsSaved(int input)
        {
            if (input == 1)
                return true;
            else
                return false;
        }
    }

    public interface IObjectTest
    {
        bool IsSaved(int input);

    }
}
