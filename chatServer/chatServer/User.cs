using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace chatServer
{
    public class User
    {
        TcpClient client;
        string name;
        int ID;

        public User(string n, int i, TcpClient c)
        {
            name = n;
            ID = i;
            client = c;
        }
        
        public string getName()
        {
            return name;
        }

        public int getID()
        {
            return ID;
        }
        public TcpClient getClient()
        {
            return client;
        }
    }
}
