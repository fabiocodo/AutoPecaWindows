using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoPeca
{
    public partial class FrmVeiculo : Form
    {
        private VO.Veiculo vo;

        public FrmVeiculo()
        {
            InitializeComponent();
            vo = new VO.Veiculo();
        }

        private void interfaceToObject() {
            vo.ano = int.Parse(txtAno.Text);
            vo.codigo = int.Parse(txtAno.Text);
            vo.modelo = txtModelo.Text;
            vo.potencia = txtPotencia.Text ;
            vo.fabricante = cmbFabricante.SelectedItem.ToString();
        }
        private void Limpar()
        {
            txtAno.Text = "";
            txtCodigo.Text = "";
            txtModelo.Text = "";
            txtPotencia.Text = "";
            cmbFabricante.SelectedIndex = -1;
        }

        private void carregar()
        {

            lstVeiculos.Items.Add(vo);
            lstVeiculos.Refresh();

        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            Limpar();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            try {
                interfaceToObject();
                Limpar();
                carregar();
            }
            catch(Exception ex){
                MessageBox.Show(ex.Message,"Erro no Aplicativo");
            }
        }
    }
}
