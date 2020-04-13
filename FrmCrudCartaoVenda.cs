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
    public partial class FrmCrudCartaoVenda : Form
    {
        public FrmCrudCartaoVenda()
        {
            InitializeComponent();
        }
        public void CarregaDgvCartaoVenda()
        {
            SqlConnection con = Conexao.obterConexao();
            String query = "select * from cartaovenda";
            SqlCommand cmd = new SqlCommand(query, con);
            Conexao.obterConexao();
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable CartaoVenda = new DataTable();
            da.Fill(CartaoVenda);
            DgvCartaoVenda.DataSource = CartaoVenda;
            Conexao.fecharConexao();
        }

        private void lblId_Click(object sender, EventArgs e)
        {

        }

        private void FrmCrudCartaoVenda_Load(object sender, EventArgs e)
        {
            CarregaDgvCartaoVenda();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCadastro_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = Conexao.obterConexao();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Inserir_CartaoVenda";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@numero", txtNumero.Text);
                cmd.Parameters.AddWithValue("@usuario", txtUsuario.Text);
                Conexao.obterConexao();
                cmd.ExecuteNonQuery();
                CarregaDgvCartaoVenda();
                FrmPrincipal obj = (FrmPrincipal)Application.OpenForms["FrmPricipal"];
                obj.CarragadgvPripedido();
                MessageBox.Show("Registro inserido com sucesso!", "Cadastro", MessageBoxButtons.OK);
                Conexao.fecharConexao();
                txtId.Text = "";
                txtNumero.Text = "";
                txtUsuario.Text = "";
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = Conexao.obterConexao();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "AtualizarCartaoVenda";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", this.txtId.Text);
                cmd.Parameters.AddWithValue("@numero", this.txtNumero.Text);
                cmd.Parameters.AddWithValue("@usuario", this.txtUsuario);
                Conexao.obterConexao();
                cmd.ExecuteNonQuery();
                CarregaDgvCartaoVenda();
                FrmPrincipal obj = (FrmPrincipal)Application.OpenForms["FrmPricipal"];
                obj.CarragadgvPripedido();
                MessageBox.Show("Registro atualizado com sucesso!", "Atualizar Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Conexao.fecharConexao();
                txtId.Text = "";
                txtNumero.Text = "";
                txtUsuario.Text = "";
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = Conexao.obterConexao();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Excluir_CartaoVenda";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", this.txtId.Text);
                Conexao.obterConexao();
                cmd.ExecuteNonQuery();
                CarregaDgvCartaoVenda();
                FrmPrincipal obj = (FrmPrincipal)Application.OpenForms["FrmPricipal"];
                obj.CarragadgvPripedido();
                MessageBox.Show("Registro apagado com sucesso!", "Excluir Registro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Conexao.fecharConexao();
                txtId.Text = "";
                txtNumero.Text = "";
                txtUsuario.Text = "";
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = Conexao.obterConexao();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Localizar_CartaoVenda";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", this.txtId.Text);
                Conexao.obterConexao();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    txtId.Text = rd["Id"].ToString();
                    txtNumero.Text = rd["numero"].ToString();
                    txtUsuario.Text = rd["usuario"].ToString();
                    Conexao.fecharConexao();
                }
                else
                {
                    MessageBox.Show("Nenhum registro encontrado!", "Sem registro!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Conexao.fecharConexao();
                }
            }
            finally
            {
            }
        }

        private void DgvCartaoVenda_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.DgvCartaoVenda.Rows[e.RowIndex];
                txtId.Text = row.Cells[0].Value.ToString();
                txtNumero.Text = row.Cells[1].Value.ToString();
                txtUsuario.Text = row.Cells[2].Value.ToString();
            }
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {
          
        }
    }
}
