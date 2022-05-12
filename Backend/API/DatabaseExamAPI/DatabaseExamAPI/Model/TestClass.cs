namespace DatabaseExamAPI.Model
{
    [Serializable]
    public class TestClass
    {
        public string Test { get; set; }
        public int UserId { get; private set; }

        public TestClass(string test)
        {
            Test = test;
            UserId = 1;
        }
    }
}
