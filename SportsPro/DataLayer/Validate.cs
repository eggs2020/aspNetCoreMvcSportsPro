using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SportsPro.Models;

namespace SportsPro.DataLayer
{
    // class to validate  customer emails, to check if an email is already in the database
    public class Validate
    {
        private const string EmailKey = "validEmail";
        
        private ITempDataDictionary tempData { get; set; }
        public Validate(ITempDataDictionary temp) => tempData = temp;

        public bool IsValid { get; private set; }
        public string ErrorMessage { get; private set; }

        //method that checks to see if a matching email already exists in the database,
        //duplication variable is used to distinguish between add operations and edit operations
        public void CheckEmail(string email, Repository<Customer> data, int duplication)
        {
            QueryOptions<Customer> emailCheck = new QueryOptions<Customer>
            {
                WhereClauses = new WhereClauses<Customer>
                {
                    {c => c.Email == email }
                }
            };
            Customer entity = data.Get(emailCheck);
            if (duplication == 0)
            {
                IsValid = (entity == null) ? true : false;
            }
            else
            {
                IsValid = true;
            }
            ErrorMessage = (IsValid) ? "" : $"Customer email : {email} is already in the database.";
        }

        //method that flags a valid email as having been checked
        public void MarkEmailChecked() => tempData[EmailKey] = true;

        //method to remove EmailKey from tempData in preparation to perform next check
        public void ClearEmail() => tempData.Remove(EmailKey);

        //method to ensure an email has been checked to see if it is valid
        public bool IsEmailChecked => tempData.Keys.Contains(EmailKey);



    }
}
