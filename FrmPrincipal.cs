using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LojaCL
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }
        public void CarragadgvPripedido()
        {
            SqlConnection con = Conexao.obterConexao();
            String query = "select * from cartaovenda";
            SqlCommand cmd = new SqlCommand(query, con);
            Conexao.obterConexao();
            cmd.CommandType = CommandType.Text;
            //SqlDataAdapter, usado para prencher o dataTable
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //adicionar um dataTable carregado em memoria
            DataTable cartao = new DataTable();
            da.Fill(cartao);
            //fonte de dados, da onde vai sair os dados
            dgvPripedido.DataSource = cartao;
            //quando for criar um cotrole em tempo de execucão é importate atribuir um nome para ele, definir as principais propriedades do controle
            DataGridViewButtonColumn fechar = new DataGridViewButtonColumn();
            fechar.Name = "FecharConta";
            fechar.HeaderText = "Fechar Conta";
            fechar.Text = "Fechar Conta";
            fechar.UseColumnTextForButtonValue = true;
            //indicar a onde vai aparecer o buton fechar, qual coluna no DataGrid
            int columIndex = 4;
            dgvPripedido.Columns.Insert(columIndex, fechar);
            Conexao.fecharConexao();
            dgvPripedido.CellClick += dgvPripedido_CellClick;
            int colunas = dgvPripedido.Columns.Count;
            if (colunas > 5)
            {
                dgvPripedido.Columns.Remove("FecharConta");
            }
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCrudCliente cli = new FrmCrudCliente();
            cli.Show();
        }

        private void testarBancoDeDadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = Conexao.obterConexao();
                String query = "select * from cliente";
                SqlCommand cmd = new SqlCommand(query, con);
                Conexao.obterConexao();
                DataSet ds = new DataSet();
                MessageBox.Show("Conectado ao Banco de Dados com Sucesso!", "Teste de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Information) ;
                Conexao.fecharConexao();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void produtosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCrudProduto pro = new FrmCrudProduto();
            pro.Show();
        }

        private void vendasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmVenda ven = new FrmVenda();
            ven.Show();
        }

        private void usuáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCrudUsuario usu = new FrmCrudUsuario();
            usu.Show();
        }

        private void cartãoDeVendaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCrudCartaoVenda usu = new FrmCrudCartaoVenda();
            usu.Show();
        }

        private void dgvPripedido_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dgvPripedido.Columns["FecharConta"].Index)
                {
                    if (Application.OpenForms["FrmVenda"] == null)
                    {
                        FrmVenda usu = new FrmVenda();
                        usu.Show();
                    }
                }
            }
            catch
            {

            }
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            CarragadgvPripedido();
        }
    }
}
