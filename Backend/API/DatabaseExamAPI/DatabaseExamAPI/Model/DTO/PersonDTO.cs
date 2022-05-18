namespace DatabaseExamAPI.Model.DTO
{
    public class PersonDTO
    {
        public string Name { get; set; }
        public int Born { get; set; }

        public PersonDTO(string name, int born)
        {
            Name = name;
            Born = born;
        }

        public PersonDTO()
        {
        }
    }
}
