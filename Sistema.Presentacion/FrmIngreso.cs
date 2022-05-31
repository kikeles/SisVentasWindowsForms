using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Data;
using System.Windows.Forms;
using Sistema.Negocio;

namespace Sistema.Presentacion
{
    public partial class FrmIngreso : Form
    {
        private DataTable DtDetalle = new DataTable();

        public FrmIngreso()
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
                DgvListado.DataSource = NIngreso.Listar();
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
                DgvListado.DataSource = NIngreso.Buscar(TxtBuscar.Text);
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
            DgvListado.Columns[1].Visible = false;//ID idingreso
            DgvListado.Columns[2].Visible = false;//idusuario
            DgvListado.Columns[0].Width = 100;
            DgvListado.Columns[3].Width = 150;//Usuario
            DgvListado.Columns[4].Width = 150;//Proveedor
            DgvListado.Columns[5].Width = 100;//Tipo_Documento
            DgvListado.Columns[5].HeaderText = "Documento";
            DgvListado.Columns[6].Width = 70;
            DgvListado.Columns[6].HeaderText = "Serie";
            DgvListado.Columns[7].Width = 70;
            DgvListado.Columns[7].HeaderText = "Número";
            DgvListado.Columns[8].Width = 60;
            DgvListado.Columns[9].Width = 100;
            DgvListado.Columns[10].Width = 100;
            DgvListado.Columns[11].Width = 100;
        }

        private void Limpiar()
        {
            TxtBuscar.Clear();
            TxtId.Clear();
            TxtCodigo.Clear();
            TxtIdProveedor.Clear();
            TxtNombreProveedor.Clear();
            TxtSerieComprobante.Clear();
            TxtNumComprobante.Clear();
            DtDetalle.Clear();
            TxtSubTotal.Text = "0.00";
            TxtTotalImpuesto.Text = "0.00";
            TxtTotal.Text = "0.00";

            DgvListado.Columns[0].Visible = false;//Columna Seleccionar
            BtnAnular.Visible = false;
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

        private void CrearTabla()
        {
            this.DtDetalle.Columns.Add("idarticulo",System.Type.GetType("System.Int32"));
            this.DtDetalle.Columns.Add("codigo", System.Type.GetType("System.String"));
            this.DtDetalle.Columns.Add("articulo", System.Type.GetType("System.String"));
            this.DtDetalle.Columns.Add("cantidad", System.Type.GetType("System.Int32"));
            this.DtDetalle.Columns.Add("precio", System.Type.GetType("System.Decimal"));
            this.DtDetalle.Columns.Add("importe", System.Type.GetType("System.Decimal"));

            DgvDetalle.DataSource = this.DtDetalle;
            //Nombramiento de las columnas y su ancho
            DgvDetalle.Columns[0].Visible = false;
            DgvDetalle.Columns[1].HeaderText = "CÓDIGO";
            DgvDetalle.Columns[1].Width = 100;
            DgvDetalle.Columns[2].HeaderText = "ARTICULO";
            DgvDetalle.Columns[2].Width = 200;
            DgvDetalle.Columns[3].HeaderText = "CANTIDAD";
            DgvDetalle.Columns[3].Width = 70;
            DgvDetalle.Columns[4].HeaderText = "PRECIO";
            DgvDetalle.Columns[4].Width = 70;
            DgvDetalle.Columns[5].HeaderText = "IMPORTE";
            DgvDetalle.Columns[5].Width = 80;
            //Columnas de solo lectura
            DgvDetalle.Columns[1].ReadOnly = true;
            DgvDetalle.Columns[2].ReadOnly = true;
            DgvDetalle.Columns[5].ReadOnly = true;
        }

        private void FormatoArticulos()
        {
            DgvArticulos.Columns[1].Visible = false;
            DgvArticulos.Columns[1].Width = 50;
            DgvArticulos.Columns[2].Width = 100;
            DgvArticulos.Columns[2].HeaderText = "Categoría";
            DgvArticulos.Columns[3].Width = 100;
            DgvArticulos.Columns[3].HeaderText = "Código";
            DgvArticulos.Columns[4].Width = 150;
            DgvArticulos.Columns[5].Width = 100;
            DgvArticulos.Columns[5].HeaderText = "Precio Venta";
            DgvArticulos.Columns[6].Width = 60;
            DgvArticulos.Columns[7].Width = 200;
            DgvArticulos.Columns[7].HeaderText = "Descripción";
            DgvArticulos.Columns[8].Width = 100;
        }

        private void FrmIngreso_Load(object sender, EventArgs e)
        {
            this.Listar();
            this.CrearTabla();
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            this.Buscar();
        }

        private void BtnBuscarProveedor_Click(object sender, EventArgs e)
        {
            FrmVista_ProveedorIngreso vista = new FrmVista_ProveedorIngreso();
            vista.ShowDialog();
            TxtIdProveedor.Text = Variables.IdProveedor.ToString();
            TxtNombreProveedor.Text = Variables.NombreProveedor;
        }

        private void TxtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            //entra cada vez que se presiona la tecla Enter
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    DataTable Tabla = new DataTable();
                    Tabla = NArticulo.BuscarCodigo(TxtCodigo.Text.Trim());
                    //consultar si existe o no el articulo 
                    if (Tabla.Rows.Count <= 0)
                    {
                        this.MensajeError("No existe artículo con ese código de barras.");
                    }
                    else
                    {
                        //Agregar al detalle
                        this.AgregarDetalle(Convert.ToInt32(Tabla.Rows[0][0]), Convert.ToString(Tabla.Rows[0][1]),
                            Convert.ToString(Tabla.Rows[0][2]), Convert.ToDecimal(Tabla.Rows[0][3]));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AgregarDetalle(int IdArticulo, string Codigo, string Nombre, decimal Precio)
        {
            //No permitir agregar dos veces un articulo
            bool Agregar = true;
            foreach (DataRow FilaTem in DtDetalle.Rows)
            {
                if(Convert.ToInt32(FilaTem["idarticulo"]) == IdArticulo)
                {
                    Agregar = false;
                    this.MensajeError("El artículo ya ha sido agregado.");
                }
            }

            if (Agregar)
            {
                DataRow Fila = DtDetalle.NewRow();
                //columnas de DataTable (DtDetalle) 
                Fila["idarticulo"] = IdArticulo;
                Fila["codigo"] = Codigo;
                Fila["articulo"] = Nombre;
                Fila["cantidad"] = 1;
                Fila["precio"] = Precio;
                Fila["importe"] = Precio;
                this.DtDetalle.Rows.Add(Fila);
                this.CalcularTotales();
            }
        }

        private void CalcularTotales()
        {
            decimal Total = 0;
            decimal SubTotal = 0;
            if (DgvDetalle.Rows.Count == 0)
            {
                Total = 0;
            }
            else
            {
                foreach (DataRow FilaTemp in DtDetalle.Rows)
                {
                    Total = Total + Convert.ToDecimal(FilaTemp["importe"]);
                }
            }
            SubTotal = Total / (1 + Convert.ToDecimal(TxtImpuesto.Text));
            TxtTotal.Text = Total.ToString("#0.00#");
            TxtSubTotal.Text = SubTotal.ToString("#0.00#");
            TxtTotalImpuesto.Text = (Total - SubTotal).ToString("#0.00#");
        }

        private void BtnVerArticulo_Click(object sender, EventArgs e)
        {
            PanelArticulos.Visible = true;
        }

        private void BtnCerrarArticulo_Click(object sender, EventArgs e)
        {
            PanelArticulos.Visible = false;
        }

        private void btnFiltrarArticulos_Click(object sender, EventArgs e)
        {
            try
            {
                DgvArticulos.DataSource = NArticulo.Buscar(TxtBuscarArticulos.Text);
                this.FormatoArticulos();
                LblTotalArticulos.Text = "Total registros: " + Convert.ToString(DgvArticulos.Rows.Count);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DgvArticulos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int IdArticulo;
            string Codigo, Nombre;
            decimal Precio;
            IdArticulo = Convert.ToInt32(DgvArticulos.CurrentRow.Cells["ID"].Value);
            Codigo = Convert.ToString(DgvArticulos.CurrentRow.Cells["Codigo"].Value);
            Nombre = Convert.ToString(DgvArticulos.CurrentRow.Cells["Nombre"].Value);
            Precio = Convert.ToDecimal(DgvArticulos.CurrentRow.Cells["Precio_Venta"].Value);
            this.AgregarDetalle(IdArticulo,Codigo,Nombre,Precio);
        }

        private void DgvDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //Calcular el importe al cambiar la cantidad en la celda cantidad
            DataRow Fila = (DataRow)DtDetalle.Rows[e.RowIndex];
            decimal Precio = Convert.ToDecimal(Fila["precio"]);
            int Cantidad = Convert.ToInt32(Fila["cantidad"]);
            Fila["importe"] = Precio * Cantidad;
            this.CalcularTotales();
        }

        private void DgvDetalle_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            this.CalcularTotales();
        }

        private void BtnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                string Rpta = "";
                if (TxtIdProveedor.Text == string.Empty || TxtImpuesto.Text == string.Empty 
                    || TxtNumComprobante.Text == string.Empty || DtDetalle.Rows.Count == 0)
                {
                    this.MensajeError("Falta ingresar algunos datos, serán remarcados.");
                    ErrorIcono.SetError(TxtIdProveedor, "Seleccione un proveedor.");
                    ErrorIcono.SetError(TxtImpuesto, "Ingrese un impuesto.");
                    ErrorIcono.SetError(TxtNumComprobante, "Ingrese el número de comprobante.");
                    ErrorIcono.SetError(DgvDetalle, "Debe tener al menos un detalle.");
                }
                else
                {
                    //Rpta (Respuesta) proviede de la capa Datos pasando por la capa Negocio
                    Rpta = NIngreso.Insertar(Convert.ToInt32(TxtIdProveedor.Text),Variables.IdUsuario,CboComprobante.Text,TxtSerieComprobante.Text.Trim(),
                        TxtNumComprobante.Text.Trim(),Convert.ToDecimal(TxtImpuesto.Text), Convert.ToDecimal(TxtTotal.Text),DtDetalle);
                    if (Rpta.Equals("OK"))
                    {
                        this.MensajeOk("Se insertó de manera correcta el registro");
                        this.Limpiar();
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
                DgvMostrarDetalle.DataSource = NIngreso.ListarDetalle(Convert.ToInt32(DgvListado.CurrentRow.Cells["ID"].Value));
                decimal Total, SubTotal;
                decimal Impuesto = Convert.ToDecimal(DgvListado.CurrentRow.Cells["Impuesto"].Value);
                Total = Convert.ToDecimal(DgvListado.CurrentRow.Cells["Total"].Value);
                SubTotal = Total / (1 + Impuesto);
                TxtSubTotalD.Text = SubTotal.ToString("#0.00#");
                TxtTotalImpuestoD.Text = (Total - SubTotal).ToString("#0.00#");
                TxtTotalD.Text = Total.ToString("#0.00#");
                PanelMostrar.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnCerrarDetalle_Click(object sender, EventArgs e)
        {
            PanelMostrar.Visible = false;
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Limpiar();
            TabGeneral.SelectedIndex = 0;
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

        private void ChkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkSeleccionar.Checked)
            {
                DgvListado.Columns[0].Visible = true;//Columna Seleccionar
                BtnAnular.Visible = true;
            }
            else
            {
                DgvListado.Columns[0].Visible = false;//Columna Seleccionar
                BtnAnular.Visible = false;
            }
        }

        private void BtnAnular_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("¿Realmente deseas anular el(los) registro(s)?", "Sistema de Ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
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
                            Rpta = NIngreso.Anular(Codigo);
                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se anuló el registro " + row.Cells[6].Value.ToString()+"-"+
                                    row.Cells[7].Value.ToString());
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
