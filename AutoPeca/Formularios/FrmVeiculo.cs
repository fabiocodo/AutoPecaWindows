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
        private List<VO.Veiculo> lista;
        private BE.VeiculoBE be;
        public FrmVeiculo()
        {
            InitializeComponent();
            InicializarVeiculos();
            liberarEdicao(false);            
            carregar();
            carregarFabricante();
        }
        private void carregarFabricante()
        {
            cmbFabricante.DataSource = null;
            cmbFabricante.DataSource = DAO.DAO.listaFabricante;
            cmbFabricante.ValueMember = "codigo";
            cmbFabricante.DisplayMember = "nome";
            cmbFabricante.Refresh();
        }

        private void InicializarVeiculos()
        {
            vo = new VO.Veiculo();
            be = new BE.VeiculoBE();           
            lista = be.listar();
        }

        private void interfaceToObject() {
            vo.ano = int.Parse(txtAno.Text);
            vo.codigo = int.Parse(txtAno.Text);
            vo.modelo = txtModelo.Text;
            vo.potencia = txtPotencia.Text ;
            vo.fabricante = cmbFabricante.SelectedItem.ToString();
            
        }
        private void objecttoInterface()
        {
            txtAno.Text = vo.ano.ToString();
            txtCodigo.Text = vo.codigo.ToString();
            txtModelo.Text = vo.modelo.ToString();
            txtPotencia.Text = vo.potencia.ToString();
            cmbFabricante.SelectedItem = vo.fabricante.ToString();
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
            lstVeiculos.DataSource = null;
            lstVeiculos.DataSource = lista;
            lstVeiculos.SelectedIndex = -1;
            lstVeiculos.ValueMember = "codigo";
            lstVeiculos.DisplayMember = "modelo";
            lstVeiculos.Refresh();

        }
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            Limpar();
            liberarEdicao(false);
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            try {
                vo = new VO.Veiculo();
                interfaceToObject();
                lista.Add(vo);
                carregar();
                Limpar();
            }
            catch(Exception ex){
                MessageBox.Show(ex.Message,"Erro no Aplicativo");
            }
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            vo = ((VO.Veiculo)lstVeiculos.Items[lstVeiculos.SelectedIndex]);
            objecttoInterface();
            liberarEdicao(true);
        }
      
        private void liberarEdicao(bool habilita)
        {
            btnGravar.Enabled = !habilita;
            btnEditar.Enabled = habilita;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            interfaceToObject();
            carregar();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            lista.RemoveAt(lstVeiculos.SelectedIndex);
            carregar();
        }
    }
}
