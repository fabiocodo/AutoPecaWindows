using System;
using System.Collections;
//using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AutoPeca.Formularios
{
    public partial class Prova : Form
    {

        ArrayList arrray;
        public Prova()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           if (arrray == null)
            {
                arrray = new ArrayList();

            }
            if (lstProduto.Items.Count == 5)
            {
                MessageBox.Show("Lista Completa");
            }
            else
            {
                string texto = txtNome.Text + "-" + txtPreco.Text;
                arrray.Add(texto);

                lstProduto.DataSource = null;
                lstProduto.DataSource = arrray;
                lstProduto.Refresh();
            }


        }
    }
}
