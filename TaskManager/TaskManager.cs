using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskManager
{
    public partial class TaskManager : Form
    {
        public TaskManager()
        {
            InitializeComponent();

        }

        private void TaskManager_Load(object sender, EventArgs e)
        {
            refresh();
        }


        private void refresh()
        {
            listView1.Clear();
            listView2.Clear();
            foreach (Process p in Process.GetProcesses("."))
            {
                if (p.MainWindowTitle.Length > 0)//görev çubuğunda gösterilen uygulamalardır.
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = p.ProcessName.ToString();
                    item.SubItems.Add(p.MainWindowTitle.ToString());
                    listView1.Items.Add(item);
                }
                else //Arka planda Çalısan uygulamaları getirir.
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = p.ProcessName.ToString();
                    item.SubItems.Add(p.SessionId.ToString());
                    item.SubItems.Add(p.Id.ToString());
                    listView2.Items.Add(item);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void btnKill_Click(object sender, EventArgs e)
        {
            kill(listView1.SelectedItems[0].Text);
            refresh();
        }

        private void kill(string process)
        {
            try
            {
                Process[] proc = Process.GetProcessesByName(process);
                proc[0].Kill();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
