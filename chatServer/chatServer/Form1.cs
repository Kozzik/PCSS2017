using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chatServer
{
    public partial class Form1 : Form
    {
        String newUser;
        String uName;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void addListItem(object obj)
        {
            if (InvokeRequired)
            {
                ObjectDelegate method = new ObjectDelegate(addListItem);
                Invoke(method, obj);
                return;
            }

            newUser = (string)obj;
            listView1.Items.Add(new ListViewItem(new[] { newUser }));
        }

        private delegate void ObjectDelegate(object obj);

        public void addNewUser(string u)
        {
            uName = u;
            ObjectDelegate del = new ObjectDelegate(addListItem);

            del.Invoke(uName);

            Thread th = new Thread(new ParameterizedThreadStart(WorkThread));
            th.Start(del);
            th.Abort();
        }

        private void WorkThread(object obj)
        {
            // we would then unbox the obj into the delegate
            ObjectDelegate del = (ObjectDelegate)obj;
  
            // and invoke it like before
            del.Invoke(uName);
        }

}
}
