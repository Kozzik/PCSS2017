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
        private Boolean bClientConnected;
        private String sData;
        private StreamWriter sWriter;
        private StreamReader sReader;
        private Commands commands;

        public Controller()
        {

            var GUImain = new Thread(initGUI);
            GUImain.Start();

        }
        
        [Serializable]private enum Commands
        {
            createRoom, joinRoom
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
            sWriter = new StreamWriter(client.GetStream(), Encoding.ASCII);
            string inputString = "";
            sReader = new StreamReader(client.GetStream(), Encoding.ASCII);
            // you could use the NetworkStream to read and write, 
            // but there is no forcing flush, even when requested

            bClientConnected = true;
            sData = null;

            if (bClientConnected)
            {
                addUser();

                inputString = sReader.ReadLine();

                if (Enum.TryParse(inputString, out commands))
                {
                    switch (commands)
                    {
                        case Commands.createRoom:
                            createRoom(sReader.ReadLine(), RID);
                            break;

                        case Commands.joinRoom:
                            //joinRoom();
                            break;
                    }
                }

                

                //Console.WriteLine(sData);
                // to write something back.
                // sWriter.WriteLine("Meaningfull things here");
                // sWriter.Flush();
            }

        }

        public void addUser()
        {
            sData = sReader.ReadLine();
            User u = new User(sData, UID);
            users.Add(u);
            UID++;
            Console.WriteLine(u.getName());
            sWriter.WriteLine(u.getID());
            sWriter.Flush();
        }

        public void createRoom(string n, int i)
        {
            Room r = new Room(n, i);
            rooms.Add(r);
            RID++;
            users.ForEach(x => this.sWriter.WriteLine(n));
            sWriter.Flush();
            Console.WriteLine(r.getName());
        }

        public void joinRoom(User u, Room r)
        {
            
        }

    }
}
