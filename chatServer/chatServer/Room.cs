using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatServer
{
    public class Room
    {
        Controller controller;
        private string name;
        private List<User> participants = new List<User>();
        private int ID;

        public Room(string n, int i, Controller con)
        {
            name = n;
            ID = i;
            controller = con;
        }
        public void joinUser(User u)
        {
            participants.Add(u);
        }
        public string getName()
        {
            return name;
        }

        public int getID()
        {
            return ID;
        }

        public void broadcastMessage()
        {
            foreach(User u in participants)
            {
                Random ran = new Random();
                int r = ran.Next(controller.getBlasted().Count);
                StreamWriter writer = new StreamWriter(u.getClient().GetStream(), Encoding.ASCII);
                writer.WriteLine(controller.randomBlast(r));
                writer.Flush();
            }
            
        }
    }
}
