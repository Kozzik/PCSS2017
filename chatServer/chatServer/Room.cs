using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatServer
{
    class Room
    {
        private string name;
        private List<User> participants = new List<User>();
        private int ID;

        public Room(string n, int i)
        {
            name = n;
            ID = i;
        }
        public void joinUser(User u)
        {
            participants.Add(u);
        }
        public string getName()
        {
            return name;
        }
    }
}
