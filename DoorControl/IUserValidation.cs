using System;
using System.Collections.Generic;
using System.Text;

namespace DoorControlProject
{
    public interface IUserValidation
    {
        bool ValidateEntryRequest(string id);
    }
}
