using System;

namespace Test
{
    /// <summary>
    /// Base class for test classes.
    /// Contains a <see cref="Random"/> field.
    /// </summary>
    public abstract class TestBase
    {
        protected Random Random;

        protected TestBase()
        {
            Random = new Random();
        }
    }
}