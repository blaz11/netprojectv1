﻿using Pizza.Net.Domain;

namespace Pizza.Net.Screens.DesignTimeData
{
    class EmployeeEntity
    {
        public Account Account
        {
            get
            {
                return new Account()
                {
                    Username = "kowalskij"
                };
            }
        }

        public string FirstName
        {
            get
            {
                return "Jacek";
            }
        }

        public string LastName
        {
            get
            {
                return "Kowalski";
            }
        }

        public Position Position
        {
            get
            {
                return new Position()
                {
                    Name = "CEO"
                };
            }
        }
    }
}