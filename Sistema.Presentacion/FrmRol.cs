using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Windows.Forms;
using Sistema.Negocio;

namespace Sistema.Presentacion
{
    public partial class FrmRol : Form
    {
        public FrmRol()
        {
            InitializeComponent();
        }

        private void Listar()
        {
            try
            {
                //using Sistema.Negocio; Listar() es metodo estatico
                //no se necesita instanciar mediante un objeto
                DgvListado.DataSource = NRol.Listar();
                this.Formato();
                LblTotal.Text = "Total registros: " + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {
                //se muestra en caso de haber un error
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void Formato()
        {
            DgvListado.Columns[0].Width = 100;
            DgvListado.Columns[0].HeaderText = "ID";
            DgvListado.Columns[1].Width = 200;
            DgvListado.Columns[1].HeaderText = "Nombre";
        }
        private void FrmRol_Load(object sender, EventArgs e)
        {
            this.Listar();
        }
    }
}
