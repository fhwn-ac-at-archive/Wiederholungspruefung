using System;

namespace TestLib
{
    /// <summary>
    /// A test class for reflection testing.
    /// </summary>
    public class TestClass : ITestInterface

    {
        /// <summary>
        /// A string.
        /// </summary>
        public string testString;

        /// <summary>
        /// A constructor.
        /// </summary>
        /// <param name="ts">test string.</param>
        public TestClass(string ts)
        {
            this.testString = ts;
        }
        /// <summary>
        /// An empty constructor.
        /// </summary>
        public TestClass()
        {
            ;
        }

        /// <summary>
        /// A useless method.
        /// </summary>
        /// <returns></returns>
        public string DoSomething()
        {
            return testString;
        }

        /// <summary>
        /// A useless function from the ITestInterface
        /// </summary>
        /// <param name="s">a string.</param>
        public void Foo(string s)
        {
            Console.WriteLine("foo: " + s);
        }

        /// <summary>
        /// This is a test property.
        /// </summary>
        public string TestString
        {
            get
            {
                return this.testString;
            }
            set
            {
                this.testString = value;
            }
        }
    }
}
