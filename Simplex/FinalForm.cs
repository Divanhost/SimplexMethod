using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simplex
{
    public partial class FinalForm : BaseForm
    {
        public FinalForm()
        {
            InitializeComponent();
        }

        private void FinalForm_Load(object sender, EventArgs e)
        {
            foreach(string item in SimplexInputCreator.CreateSolution())
            {
                listBox1.Items.Add(item);
                
            }
            for (int i = 0; i < SimplexInputCreator.result.Length; i++)
            {
                listBox2.Items.Add(string.Format("X[{0}]={1}", i + 1, SimplexInputCreator.result[i]));
            }
        }
    }
}
