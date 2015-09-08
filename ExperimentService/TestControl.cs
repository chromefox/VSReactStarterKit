using System;
using System.Windows.Forms;

namespace ExperimentService
{
    public partial class TestControl : Form
    {
        private Service1 ms;

        public TestControl()
        {
            InitializeComponent();
        }


        public TestControl(Service1 ms)
            : this()
        {
            this.ms = ms;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TestControl_FormClosed(object sender, FormClosedEventArgs e)
        {
            ms?.Stop();
        }
    }
}
