namespace DatabaseExamAPI.Model.DTO
{
    public class RegisterDTO
    {

        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }


        public RegisterDTO(string username, string password, string email)
        {
            Username = username;
            Password = password;
            Email = email;
        }

    }
}
