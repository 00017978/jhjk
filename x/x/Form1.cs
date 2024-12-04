using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace x
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void tb_ModuleBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            { // TODO: This line of code loads data into the 'universityDataSet.tb_ModuleType' table. You can move, or remove it, as needed.
                this.tb_ModuleTypeTableAdapter.Fill(this.universityDataSet.tb_ModuleType);
                // TODO: This line of code loads data into the 'universityDataSet.tb_Module' table. You can move, or remove it, as needed.
                this.tb_ModuleTableAdapter.Fill(this.universityDataSet.tb_Module);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            tb_ModuleBindingSource.MoveFirst();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            tb_ModuleBindingSource.MovePrevious();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tb_ModuleBindingSource.MoveNext();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            tb_ModuleBindingSource.MoveLast();
        }

        private void EnableButtons()
        {
            if (tb_ModuleBindingSource.Position == 0)
            {
                btnFirst.Enabled = false;
                btnPrevious.Enabled = false;
            }
            else
            {
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;
            }

            if (tb_ModuleBindingSource.Position == tb_ModuleBindingSource.Count - 1)
            {
                btnNext.Enabled = false;
                btnLast.Enabled = false;
            }
            else
            {
                btnNext.Enabled = true;
                btnLast.Enabled = true;
            }
        }

        private void tb_ModuleBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            EnableButtons();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (tb_ModuleBindingSource.Count == 0)
            {
                MessageBox.Show("X");
            }
            else
            {
                var a = MessageBox.Show("U sure?", "delete", MessageBoxButtons.YesNo);
                if (a == DialogResult.Yes)
                { tb_ModuleBindingSource.RemoveCurrent(); }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void SaveData()
        {
            if (this.Validate())
            {
                try
                {
                    this.tb_ModuleBindingSource.EndEdit();
                    this.tableAdapterManager.UpdateAll(this.universityDataSet);
                    MessageBox.Show("Successfully saved!");
                }
                catch (Exception e) { MessageBox.Show(e.Message); }
            }


        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Validate())
            {
                if (universityDataSet.HasChanges())
                {
                    var a = MessageBox.Show("U sure u dn wanna save?", "Exit", MessageBoxButtons.YesNo);
                    if (a == DialogResult.Yes)
                    {
                        SaveData();
                    }
                }
            }
            else
            {
                MessageBox.Show("There are empty fields!");
                e.Cancel = true;
            }
        }

        private void tbxfilter_TextChanged(object sender, EventArgs e)
        {
            tb_ModuleBindingSource.Filter = $"ModuleName LIKE '{tbxfilter.Text}%'";
        }
    }
}
