using System;
using System.Windows.Forms;
using Sistema.Negocio;

namespace Sistema.Presentacion
{
    public partial class FrmVista_ProveedorIngreso : Form
    {
        public FrmVista_ProveedorIngreso()
        {
            InitializeComponent();
        }

        //Mótodos de uso para los eventos de los controles
        private void Listar()
        {
            try
            {
                //using Sistema.Negocio; Listar() es metodo estatico
                //no se necesita instanciar mediante un objeto
                DgvListado.DataSource = NPersona.ListarProveedores();
                this.Formato();
                LblTotal.Text = "Total registros: " + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {
                //se muestra en caso de haber un error
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void Buscar()
        {
            try
            {
                //using Sistema.Negocio;
                DgvListado.DataSource = NPersona.BuscarProveedores(TxtBuscar.Text);
                this.Formato();
                LblTotal.Text = "Total registros: " + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void Formato()
        {
            DgvListado.Columns[0].Visible = false;//Seleccionar
            DgvListado.Columns[1].Width = 50;//ID
            DgvListado.Columns[2].Width = 100;
            DgvListado.Columns[2].HeaderText = "Tipo Persona";
            DgvListado.Columns[3].Width = 170;
            DgvListado.Columns[4].Width = 100;
            DgvListado.Columns[4].HeaderText = "Documento";
            DgvListado.Columns[5].Width = 100;
            DgvListado.Columns[5].HeaderText = "Número Doc.";
            DgvListado.Columns[6].Width = 120;
            DgvListado.Columns[6].HeaderText = "Dirección";
            DgvListado.Columns[7].Width = 100;
            DgvListado.Columns[7].HeaderText = "Teléfono";
            DgvListado.Columns[8].Width = 120;
        }
        private void FrmVista_ProveedorIngreso_Load(object sender, EventArgs e)
        {
            /*
             * No es recomendable cargar una lista de proveedores si esta es muy extensa
             * ya que se puede sobrecargar la memoria, por lo que es recomendable
             * solamente hacer la busqueda y de hay cargar el proveedor con el doble clic
             */
            //this.Listar();
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            this.Buscar();
        }

        private void DgvListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Variables.IdProveedor = Convert.ToInt32(DgvListado.CurrentRow.Cells["ID"].Value);
            Variables.NombreProveedor = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
            this.Close();//cierra este mismo formulario
        }
    }
}
