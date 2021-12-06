using Contact.API.Models;

namespace Contact.API.Infrustructure
{
    public interface IContactService
    {
        public ContactDTO GetContactById(int Id);
    }
}