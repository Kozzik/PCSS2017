using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatServer
{
    public class User
    {
        string name;
        int ID;

        public User(string n, int i)
        {
        name = n;
        ID = i;

        }
        
        public string getName()
        {
            return name;
        }

        public int getID()
        {
            return ID;
        }

    }
}
