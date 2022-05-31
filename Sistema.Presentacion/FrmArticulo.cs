
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Windows.Forms;
using Sistema.Negocio;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Sistema.Presentacion
{
    public partial class FrmArticulo : Form
    {
        private string RutaOrigen;//Ruta absoluta de la imagen
        private string RutaDestino;//Directorio donde se carga
        private string Directorio = "C:\\Sistema\\";//directorio de las imagenes
        private string NombreAnt;

        public FrmArticulo()
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
                DgvListado.DataSource = NArticulo.Listar();
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
                DgvListado.DataSource = NArticulo.Buscar(TxtBuscar.Text);
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
            DgvListado.Columns[0].Visible = false;//Seleccionar CheckBox
            DgvListado.Columns[2].Visible = false;//idcategoria
            DgvListado.Columns[0].Width = 100;
            DgvListado.Columns[1].Width = 50;//ID
            DgvListado.Columns[3].Width = 100;
            DgvListado.Columns[3].HeaderText = "Categoría";
            DgvListado.Columns[4].Width = 100;
            DgvListado.Columns[4].HeaderText = "Código";
            DgvListado.Columns[5].Width = 150;
            DgvListado.Columns[6].Width = 100;
            DgvListado.Columns[6].HeaderText = "Precio Venta";
            DgvListado.Columns[7].Width = 60;//Stock
            DgvListado.Columns[8].Width = 200;
            DgvListado.Columns[8].HeaderText = "Descripción";
            DgvListado.Columns[9].Width = 100;
            DgvListado.Columns[10].Width = 100;
        }

        private void Limpiar()
        {
            TxtBuscar.Clear();
            BtnInsertar.Visible = true;
            BtnActualizar.Visible = false;//oculta el BtnActualizar
            ErrorIcono.Clear();
            //Controles del TabPage Mantenimiento
            TxtNombre.Clear();
            TxtId.Clear();
            TxtCodigo.Clear();
            TxtPrecioVenta.Clear();
            TxtStock.Clear();
            TxtImagen.Clear();
            PicImagen.Image = null;
            PanelCodigo.BackgroundImage = null;
            BtnGuardarCodigo.Enabled = true;
            TxtDescripcion.Clear();
            this.RutaDestino = "";
            this.RutaOrigen = "";

            DgvListado.Columns[0].Visible = false;//Columna Seleccionar
            BtnActivar.Visible = false;
            BtnDesactivar.Visible = false;
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

        private void CargarCategoria()
        {
            try
            {
                CboCategoria.DataSource = NCategoria.Seleccionar();
                CboCategoria.ValueMember = "idcategoria";
                CboCategoria.DisplayMember = "nombre";
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        //Métodos de los eventos de los controles
        private void FrmArticulo_Load(object sender, EventArgs e)
        {
            this.Listar();
            this.CargarCategoria();
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            this.Buscar();
        }

        private void BtnCargarImagen_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (file.ShowDialog() == DialogResult.OK)
            {
                PicImagen.Image = Image.FromFile(file.FileName);
                TxtImagen.Text = file.FileName.Substring(file.FileName.LastIndexOf("\\")+1);
                this.RutaOrigen = file.FileName;
            }
        }

        private void BtnGenerarCodigo_Click(object sender, EventArgs e)
        {
            BarcodeLib.Barcode Codigo = new BarcodeLib.Barcode();
            Codigo.IncludeLabel = true;//incluye un texto en número
            PanelCodigo.BackgroundImage = Codigo.Encode(BarcodeLib.TYPE.CODE128, TxtCodigo.Text, Color.Black,Color.White,230,58);
            BtnGuardarCodigo.Enabled = true;
        }

        private void BtnGuardarCodigo_Click(object sender, EventArgs e)
        {
            //Clonar el codigo de barras mostrado en el panel
            Image imgFinal = (Image)PanelCodigo.BackgroundImage.Clone();
            SaveFileDialog DialogoGuardar = new SaveFileDialog();
            DialogoGuardar.AddExtension = true;
            DialogoGuardar.Filter = "Image PNG (*.png)|*.png";
            DialogoGuardar.ShowDialog();
            if (!string.IsNullOrEmpty(DialogoGuardar.FileName))
            {
                //(ImageFormat) using System.Drawing.Imaging;
                imgFinal.Save(DialogoGuardar.FileName, ImageFormat.Png);
            }
            imgFinal.Dispose();
        }

        private void BtnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                string Rpta = "";
                if (CboCategoria.Text == string.Empty || TxtNombre.Text == string.Empty
                    || TxtPrecioVenta.Text == string.Empty || TxtStock.Text == string.Empty)
                {
                    this.MensajeError("Falta ingresar algunos datos, serán remarcados.");
                    ErrorIcono.SetError(CboCategoria, "Seleccione una categoría.");
                    ErrorIcono.SetError(TxtNombre, "Ingrese un nombre.");
                    ErrorIcono.SetError(TxtPrecioVenta, "Ingrese un precio.");
                    ErrorIcono.SetError(TxtStock, "Ingrese un stock inicial.");
                }
                else
                {
                    //Rpta (Respuesta) proviede de la capa Datos pasando por la capa Negocio
                    Rpta = NArticulo.Insertar(Convert.ToInt32(CboCategoria.SelectedValue),TxtCodigo.Text.Trim(),
                        TxtNombre.Text.Trim(), Convert.ToDecimal(TxtPrecioVenta.Text),Convert.ToInt32(TxtStock.Text),
                        TxtDescripcion.Text.Trim(), TxtImagen.Text.Trim());
                    if (Rpta.Equals("OK"))
                    {
                        this.MensajeOk("Se insertó de manera correcta el registro");
                        if (TxtImagen.Text != string.Empty)
                        {
                            //Entra si selecciono una imagen 
                            this.RutaDestino = this.Directorio + TxtImagen.Text;
                            File.Copy(this.RutaOrigen, this.RutaDestino);
                        }
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
                TxtId.Text = DgvListado.CurrentRow.Cells["ID"].Value.ToString();
                CboCategoria.SelectedValue = DgvListado.CurrentRow.Cells["idcategoria"].Value.ToString();
                TxtCodigo.Text = DgvListado.CurrentRow.Cells["Codigo"].Value.ToString();
                this.NombreAnt = DgvListado.CurrentRow.Cells["Nombre"].Value.ToString();
                TxtNombre.Text = DgvListado.CurrentRow.Cells["Nombre"].Value.ToString();
                TxtPrecioVenta.Text = DgvListado.CurrentRow.Cells["Precio_Venta"].Value.ToString();
                TxtStock.Text = DgvListado.CurrentRow.Cells["Stock"].Value.ToString();
                TxtDescripcion.Text = DgvListado.CurrentRow.Cells["Descripcion"].Value.ToString();
                string Imagen;
                Imagen = DgvListado.CurrentRow.Cells["Imagen"].Value.ToString();
                if (Imagen != string.Empty)
                {
                    PicImagen.Image = Image.FromFile(this.Directorio + Imagen);
                    TxtImagen.Text = Imagen;
                }
                else
                {
                    PicImagen.Image = null;
                    TxtImagen.Text = "";
                }
                TabGeneral.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Seleccione desde la celda nombre."+" | Error: "+ex.Message);
            }
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string Rpta = "";
                if (TxtId.Text == string.Empty || CboCategoria.Text == string.Empty || TxtNombre.Text == string.Empty
                    || TxtPrecioVenta.Text == string.Empty || TxtStock.Text == string.Empty)
                {
                    this.MensajeError("Falta ingresar algunos datos, serán remarcados.");
                    ErrorIcono.SetError(CboCategoria, "Seleccione una categoría.");
                    ErrorIcono.SetError(TxtNombre, "Ingrese un nombre.");
                    ErrorIcono.SetError(TxtPrecioVenta, "Ingrese un precio.");
                    ErrorIcono.SetError(TxtStock, "Ingrese un stock inicial.");
                }
                else
                {
                    //Rpta (Respuesta) proviede de la capa Datos pasando por la capa Negocio
                    Rpta = NArticulo.Actualizar(Convert.ToInt32(TxtId.Text),Convert.ToInt32(CboCategoria.SelectedValue), TxtCodigo.Text.Trim(),
                        this.NombreAnt, TxtNombre.Text.Trim(), Convert.ToDecimal(TxtPrecioVenta.Text), Convert.ToInt32(TxtStock.Text),
                        TxtDescripcion.Text.Trim(), TxtImagen.Text.Trim());
                    if (Rpta.Equals("OK"))
                    {
                        this.MensajeOk("Se actualizó de manera correcta el registro");
                        if (TxtImagen.Text != string.Empty && this.RutaOrigen != string.Empty)
                        {
                            //Entra si selecciono una imagen 
                            this.RutaDestino = this.Directorio + TxtImagen.Text;
                            File.Copy(this.RutaOrigen, this.RutaDestino);
                        }
                        this.Listar();
                        TabGeneral.SelectedIndex = 0;
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
                BtnActivar.Visible = true;
                BtnDesactivar.Visible = true;
                BtnEliminar.Visible = true;
            }
            else
            {
                DgvListado.Columns[0].Visible = false;//Columna Seleccionar
                BtnActivar.Visible = false;
                BtnDesactivar.Visible = false;
                BtnEliminar.Visible = false;
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
                    string Imagen = "";

                    foreach (DataGridViewRow row in DgvListado.Rows)
                    {
                        //si la celda marcada es true
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Codigo = Convert.ToInt32(row.Cells[1].Value);
                            Imagen = Convert.ToString(row.Cells[9].Value);
                            Rpta = NArticulo.Eliminar(Codigo);
                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se eliminó el registro" + row.Cells[5].Value.ToString());
                                File.Delete(this.Directorio+Imagen);
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

        private void BtnDesactivar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("¿Realmente deseas desactivar el(los) registro(s)?", "Sistema de Ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
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
                            Rpta = NArticulo.Desactivar(Codigo);
                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se desactivó el registro" + row.Cells[5].Value.ToString());
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

        private void BtnActivar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("¿Realmente deseas activar el(los) registro(s)?", "Sistema de Ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
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
                            Rpta = NArticulo.Activar(Codigo);
                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se activó el registro" + row.Cells[5].Value.ToString());
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

        private void BtnReporte_Click(object sender, EventArgs e)
        {
            Reportes.FrmReporteArticulos Reporte = new Reportes.FrmReporteArticulos();
            Reporte.ShowDialog();
        }

        //Fin
    }
}
