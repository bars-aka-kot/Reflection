namespace Reflection
{
    internal class TestClass
    {
        [DontSave]
        public int I { get; set; }
        public char[]? C { get; set; }
        public string? S { get; set; }
        public decimal D { get; set; }
        [CustomName("CustomFieldName")]
        public string F = "Hello!";

        public TestClass(int i)
        {
            I = i;
        }
        public TestClass()
        {
        }
        public TestClass(int i, char[]? c, string? s, decimal d, string f) : this(i)
        {
            C = c;
            S = s;
            D = d;
            F = f;
        }
    }
}
