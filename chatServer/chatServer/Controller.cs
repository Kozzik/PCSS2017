using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chatServer
{
    public class Controller
    {
        private TcpListener server;
        private Boolean isRunning;
        private Form1 mainWindow;
        private int UID = 0;
        private int RID = 0;
        private List<User> users = new List<User>();
        private List<Room> rooms = new List<Room>();



        

        public Controller()
        {

            var GUImain = new Thread(initGUI);
            GUImain.Start();

        }

        private void initGUI()
        {
            mainWindow = new Form1();
            Application.Run(mainWindow);
        }

        public void serverSetup(int port)
        {
            server = new TcpListener(IPAddress.Any, port);
            server.Start();

            isRunning = true;

            LoopClients();
        }

        public void LoopClients()
        {
            while (isRunning)
            {
                // wait for client connection
                TcpClient newClient = server.AcceptTcpClient();

                // client found.
                // create a thread to handle communication
                Thread t = new Thread(new ParameterizedThreadStart(HandleClient));
                t.Start(newClient);
            }
        }

        public void HandleClient(object obj)
        {
            // retrieve client from parameter passed to thread
            TcpClient client = (TcpClient)obj;

            // sets two streams
            StreamWriter sWriter = new StreamWriter(client.GetStream(), Encoding.ASCII);
            StreamReader sReader = new StreamReader(client.GetStream(), Encoding.ASCII);
            // you could use the NetworkStream to read and write, 
            // but there is no forcing flush, even when requested

            Boolean bClientConnected = true;
            String sData = null;

            if (bClientConnected)
            {
                // reads from stream
                sData = sReader.ReadLine();

                // shows content on the console.
                addUser(sData, UID);
               

                //Console.WriteLine(sData);
                // to write something back.
                // sWriter.WriteLine("Meaningfull things here");
                // sWriter.Flush();
            }

        }

        public void addUser(string n, int i)
        {
            User u = new User(n, i);
            users.Add(u);
            UID++;
            System.Console.WriteLine(u.getName());
        }

        public void createRoom(string n, int i, User u)
        {
            Room r = new Room(n, i);
            rooms.Add(r);
            RID++;
            r.joinUser(u);
        }

    }
}
