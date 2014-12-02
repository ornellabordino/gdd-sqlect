﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrbaHotel.ABM_de_Hotel
{
    public partial class Alta_Hotel : Form
    {
        private HotelAppModel appModel;
        private DataGridView listaHoteles;

        public Alta_Hotel(DataGridView lsHoteles)
        {
            listaHoteles = lsHoteles;
            InitializeComponent();
            appModel = new AltaHotelAppModel();
            Text = "Alta de Hotel";
        }

        public Alta_Hotel(DataGridView lsHoteles, StringBuilder pais, StringBuilder ciudad, StringBuilder calle, Int32 nro_calle)
        {
            listaHoteles = lsHoteles;
            InitializeComponent();
            Text = "Modificacion de Hotel";
            StringBuilder sentence = new StringBuilder().AppendFormat("SELECT h.id_hotel,h.nombre,h.mail,h.fecha_creacion,h.pais,h.ciudad,h.calle,h.nro_calle,h.cant_estrellas,rh.fk_regimen FROM SQLECT.Hoteles h, SQLECT.Regimenes_Hoteles rh WHERE h.pais='{0}' AND h.ciudad='{1}' AND h.calle='{2}' AND h.nro_calle={3} AND h.id_hotel=rh.fk_hotel", pais.ToString(), ciudad.ToString(), calle.ToString(), nro_calle);
            appModel = new ModificacionAppModel(sentence);
            
            Nombre.Text = appModel.rowHotel.Rows[0][1].ToString();
            Email.Text = appModel.rowHotel.Rows[0][2].ToString();
            Fecha_creacion.Text = appModel.rowHotel.Rows[0][3].ToString();
            Pais.Text = appModel.rowHotel.Rows[0][4].ToString();
            Ciudad.Text = appModel.rowHotel.Rows[0][5].ToString();
            Calle.Text = appModel.rowHotel.Rows[0][6].ToString();
            Nro_calle.Text = appModel.rowHotel.Rows[0][7].ToString();
            Cantidad_Estrellas.Text = appModel.rowHotel.Rows[0][8].ToString();

            int i;
            for (i = 0; i<appModel.rowHotel.Rows.Count;i++) {
                int fk_regimen = Int32.Parse(appModel.rowHotel.Rows[i][9].ToString());
                if (fk_regimen==1) ckPensionCompleta.Checked = true;
                if (fk_regimen==2) ckMediaPension.Checked = true;
                if (fk_regimen==3) ckAllInclusive.Checked = true;
                if (fk_regimen==4) ckAllInclusiveModerado.Checked = true;
            }
        }

        private void btSeleccionarFecha_Click(object sender, EventArgs e)
        {
            monthCalendar1.Visible = true;
        }

        private void btVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Nombre.ResetText();
            Email.ResetText();
            Fecha_creacion.ResetText();
            Calle.ResetText();
            Nro_calle.ResetText();
            Pais.ResetText();
            Ciudad.ResetText();
            Cantidad_Estrellas.ResetText();
            ckAllInclusive.Checked = false;
            ckAllInclusiveModerado.Checked = false;
            ckMediaPension.Checked = false;
            ckPensionCompleta.Checked = false;
        }

        private void btAlta_Click(object sender, EventArgs e)
        {
            StringBuilder errores = new StringBuilder();
            bool retValue = this.appModel.actionHotel(Nombre, Email, Cantidad_Estrellas, Fecha_creacion,
                this.ckAllInclusive.Checked, this.ckAllInclusiveModerado.Checked, this.ckPensionCompleta.Checked,
                this.ckMediaPension.Checked, Pais, Ciudad, Calle, Nro_calle, errores);

            if (retValue)
            {
                this.listaHoteles.DataSource = ABM_de_Hotel.MainHotel.cargar_lista(ABM_de_Hotel.MainHotel.getAllInstances()).DefaultView;
                this.listaHoteles.AllowUserToAddRows = false;
                this.Close();
            }
            else
            {
                MessageBox.Show(errores.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errores = null;
            }
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            Fecha_creacion.Clear();
            Fecha_creacion.AppendText(monthCalendar1.SelectionStart.ToShortDateString());
            monthCalendar1.Visible = false;
        }
    }
}
