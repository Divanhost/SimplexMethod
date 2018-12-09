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
    public partial class Form1 : BaseForm
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SimplexInputCreator.rowsCount = Int32.Parse(numericUpDown1.Value.ToString());
            SimplexInputCreator.formsNeeded = SimplexInputCreator.rowsCount;
            SimplexInputCreator.variablesСount = Int32.Parse(numericUpDown2.Value.ToString());
            SimplexInputCreator.AddSpace();

            RowOne ro = new RowOne();
            ro.text = "F(x):";
            ro.b = true;
            ShowNextForm(ro);
        }
    }
}
