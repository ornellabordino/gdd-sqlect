﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FrbaHotel.Commons.Database;
using System.Windows.Forms;
using FrbaHotel.Generar_Modificar_Reserva;

namespace FrbaHotel.ABM_de_Cliente
{
    class AppModel_Alta_Cliente : AppModel_Base_Cliente
    {
        private Conexion sqlconexion = Conexion.Instance;
        AppModel_Reservas funcionesReservas = new AppModel_Reservas();

        public override void abmlCliente(string nombre, string apellido, string mail, string dom_Calle, string nro_Calle, string piso, string depto, string fecha_Nac, string nacionalidad, string documento_Nro, int idReserva)
        {
                Conexion conexion = Conexion.Instance;
                System.Data.SqlClient.SqlCommand comandoACliente = new System.Data.SqlClient.SqlCommand();
                comandoACliente.CommandType = CommandType.StoredProcedure;

                comandoACliente.Parameters.Add("@Nombre", SqlDbType.VarChar);
                comandoACliente.Parameters.Add("@Apellido", SqlDbType.VarChar);
                comandoACliente.Parameters.Add("@Mail", SqlDbType.VarChar);
                comandoACliente.Parameters.Add("@Dom_Calle", SqlDbType.VarChar);
                comandoACliente.Parameters.Add("@Nro_Calle", SqlDbType.Int);
                comandoACliente.Parameters.Add("@Piso", SqlDbType.TinyInt);
                comandoACliente.Parameters.Add("@Depto", SqlDbType.VarChar);
                comandoACliente.Parameters.Add("@Fecha_Nac", SqlDbType.DateTime);
                comandoACliente.Parameters.Add("@Nacionalidad", SqlDbType.VarChar);
                comandoACliente.Parameters.Add("@documento_Nro", SqlDbType.BigInt);
                comandoACliente.Parameters.Add("@idReserva", SqlDbType.Int);

                comandoACliente.Parameters[0].Value = nombre;
                comandoACliente.Parameters[1].Value = apellido;
                comandoACliente.Parameters[2].Value = mail;
                comandoACliente.Parameters[3].Value = dom_Calle;
                comandoACliente.Parameters[4].Value = nro_Calle;
                comandoACliente.Parameters[5].Value = piso;
                comandoACliente.Parameters[6].Value = depto;
                comandoACliente.Parameters[7].Value = DateTime.Parse(fecha_Nac);
                comandoACliente.Parameters[8].Value = nacionalidad;
                comandoACliente.Parameters[9].Value = documento_Nro;
                comandoACliente.Parameters[10].Value = idReserva;

                comandoACliente.CommandText = "SQLECT.altaCliente";
                conexion.ejecutarQueryConSP(comandoACliente);

                if (idReserva != 0)
                  MessageBox.Show(string.Format("Alta exitosa, guarde el siguiente código para posteriores modificaciones: {0}", funcionesReservas.obtenerCodigoReserva(idReserva))); 
                else           
                  MessageBox.Show("Alta exitosa", "Alta de Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public override void levantar(StringBuilder sentence)
        {
           
        }

    }
}