namespace DittoBox.API.UserProfile.Domain.Models.Entities
{
	public class User
    {
        public int Id { get; set; }
		public string Username { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
	    public User()
        {
        }
        public User(string username, string password, string email)
        {
            Username = username;
            Password = password;
            Email = email;
        }

		public string GetUserDetails() {
			throw new NotImplementedException();
		}

		public void UpdateUsername(string username) {
			throw new NotImplementedException();
		}

		public void UpdateEmail(string email) {
			throw new NotImplementedException();
		}

		public void UpdatePassword(string password) {
			throw new NotImplementedException();
		}
	}
}