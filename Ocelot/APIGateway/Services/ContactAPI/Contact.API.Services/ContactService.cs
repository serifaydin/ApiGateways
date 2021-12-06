using Contact.API.Infrustructure;
using Contact.API.Models;

namespace Contact.API.Services
{
    public class ContactService : IContactService
    {
        public ContactDTO GetContactById(int Id)
        {
            return new ContactDTO
            {
                Id = 1,
                FirstName = "Şerif",
                LastName = "Aydın"
            };
        }
    }
}