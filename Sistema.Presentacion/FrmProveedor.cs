using System;
using System.Data;
using System.Windows.Forms;
using Sistema.Negocio;

namespace Sistema.Presentacion
{
    public partial class FrmProveedor : Form
    {
        private string NombreAnt;

        public FrmProveedor()
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
                this.Limpiar();//oculta el BtnActualizar
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

        private void Limpiar()
        {
            TxtBuscar.Clear();
            TxtNombre.Clear();
            TxtId.Clear();
            TxtNumDocumento.Clear();
            TxtDireccion.Clear();
            TxtEmail.Clear();
            TxtTelefono.Clear();
            BtnInsertar.Visible = true;
            BtnActualizar.Visible = false;//oculta el BtnActualizar
            ErrorIcono.Clear();

            DgvListado.Columns[0].Visible = false;//Columna Seleccionar
            BtnEliminar.Visible = false;
            ChkSeleccionar.Checked = false;//deseleccionado
        }

        private void MensajeError(string Mensaje)
        {
            MessageBox.Show(Mensaje, "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void MensajeOk(string Mensaje)
        {
            MessageBox.Show(Mensaje, "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        private void FrmProveedor_Load(object sender, EventArgs e)
        {
            this.Listar();
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            this.Buscar();
        }

        private void BtnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                string Rpta = "";
                if (TxtNombre.Text == string.Empty)
                {
                    this.MensajeError("Falta ingresar algunos datos, serán remarcados.");
                    ErrorIcono.SetError(TxtNombre, "Ingrese un nombre.");
                }
                else
                {
                    //Rpta (Respuesta) proviede de la capa Datos pasando por la capa Negocio
                    Rpta = NPersona.Insertar("Proveedor",TxtNombre.Text.Trim(),CboTipoDocumento.Text, 
                        TxtNumDocumento.Text.Trim(), TxtDireccion.Text.Trim(),
                        TxtTelefono.Text.Trim(), TxtEmail.Text.Trim());
                    if (Rpta.Equals("OK"))
                    {
                        this.MensajeOk("Se insertó de manera correcta el registro");
                        this.Listar();
                    }
                    else
                    {
                        this.MensajeError(Rpta);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void DgvListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Limpiar();
                BtnActualizar.Visible = true;
                BtnInsertar.Visible = false;
                TxtId.Text = Convert.ToString(DgvListado.CurrentRow.Cells["ID"].Value);
                TxtNombre.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
                this.NombreAnt = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
                CboTipoDocumento.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Tipo_Documento"].Value);
                TxtNumDocumento.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Num_Documento"].Value);
                TxtDireccion.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Direccion"].Value);
                TxtTelefono.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Telefono"].Value);
                TxtEmail.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Email"].Value);
                TabGeneral.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Seleccione desde la celda nombre | Error: "+ex.Message);
            }
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string Rpta = "";
                if (TxtId.Text == string.Empty || TxtNombre.Text == string.Empty)
                {
                    this.MensajeError("Falta ingresar algunos datos, serán remarcados.");
                    ErrorIcono.SetError(TxtNombre, "Ingrese un nombre.");
                }
                else
                {
                    //Rpta (Respuesta) proviede de la capa Datos pasando por la capa Negocio
                    Rpta = NPersona.Actualizar(Convert.ToInt32(TxtId.Text),"Proveedor",this.NombreAnt, TxtNombre.Text.Trim(), CboTipoDocumento.Text,
                        TxtNumDocumento.Text.Trim(), TxtDireccion.Text.Trim(),
                        TxtTelefono.Text.Trim(), TxtEmail.Text.Trim());
                    if (Rpta.Equals("OK"))
                    {
                        this.MensajeOk("Se actualizó de manera correcta el registro");
                        this.Listar();
                    }
                    else
                    {
                        this.MensajeError(Rpta);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Limpiar();
            TabGeneral.SelectedIndex = 0;
        }

        private void ChkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkSeleccionar.Checked)
            {
                DgvListado.Columns[0].Visible = true;//Columna Seleccionar
                BtnEliminar.Visible = true;
            }
            else
            {
                DgvListado.Columns[0].Visible = false;//Columna Seleccionar
                BtnEliminar.Visible = false;
            }
        }

        private void DgvListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Marca y desmarca el checkBox por la celda seleccionada
            if (e.ColumnIndex == DgvListado.Columns["Seleccionar"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)DgvListado.Rows[e.RowIndex].Cells["Seleccionar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("¿Realmente deseas eliminar el(los) registro(s)?", "Sistema de Ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Opcion == DialogResult.OK)
                {
                    int Codigo;
                    string Rpta = "";
                    foreach (DataGridViewRow row in DgvListado.Rows)
                    {
                        //si la celda marcada es true
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Codigo = Convert.ToInt32(row.Cells[1].Value);
                            Rpta = NPersona.Eliminar(Codigo);
                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se eliminó el registro " + row.Cells[3].Value.ToString());
                            }
                            else
                            {
                                this.MensajeError(Rpta);
                            }
                        }
                    }
                    this.Listar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        //Fin
    }
}
