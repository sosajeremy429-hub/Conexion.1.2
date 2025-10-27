using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROYECTOCONEXION
{
    public partial class Proveedores : Form
    {
        public Proveedores()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProveedorID.Text))
            {
                MessageBox.Show("Si el Codigo esta incorrecto o vacio.");
                return;
            }
            if (string.IsNullOrEmpty(txtNombreProv.Text))
            {
                MessageBox.Show("El nombre está incorrecto o vacio.");
                return;
            }

            if (string.IsNullOrEmpty(txtCorreoElectronico.Text))
            {
                MessageBox.Show("El Correo está incorrecto o vacio.");
                return;
            }

            if (string.IsNullOrEmpty(txtTelefono.Text))
            {
                MessageBox.Show("El telefono está incorrecta o vacia.");
                return;
            }


            // TODO:  Debes cambiar esta variable connectionString para que pueda conectarse a tu base de datos.
            string connectionString = @"Data Source=JEREMY;Initial Catalog=PROYECTO3;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string queryInsertarProveedores = @"INSERT INTO Proveedores (ProveedorID, NombreProveedor, Telefono, CorreoElectronico)
                                           VALUES ('" + txtProveedorID.Text + "', '" + txtNombreProv.Text + "','" + txtTelefono.Text + "'," +
                                                   "'" + txtCorreoElectronico.Text + "')";

                using (SqlCommand cmd = new SqlCommand(queryInsertarProveedores, connection))
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Se ha insertado al cliente en la base de datos.");
                    }
                }

                connection.Close();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEliminar.Text))
            {
                MessageBox.Show("Debe introducir un ID válido.");
                return;
            }

            // CONEXION A MI BASE DE DATOS
            string connectionString = @"Data Source=JEREMY;Initial Catalog=PROYECTO3;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string queryEliminarProveedores = @"DELETE FROM Proveedores WHERE ProveedorID = '" + txtEliminar.Text + "'";

                using (SqlCommand cmd = new SqlCommand(queryEliminarProveedores, connection))
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Se ha eliminado el proveedor en la base de datos.");
                    }
                }

                connection.Close();
            }
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=JEREMY;Initial Catalog=PROYECTO3;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string queryProveedores = "SELECT * FROM Proveedores;";

                using (SqlCommand cmd = new SqlCommand(queryProveedores, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dgProveedores.DataSource = dt;
                    }
                }

                connection.Close();
            }
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEliminar.Text))
            {
                MessageBox.Show("Debe introducir un ID válido.");
                return;
            }

            // TODO:  Debes cambiar esta variable connectionString para que pueda conectarse a tu base de datos.
            string connectionString = @"Data Source=JEREMY;Initial Catalog=PROYECTO3;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string queryEliminarProveedores = @"DELETE FROM Proveedores WHERE ProveedorID = '" + txtEliminar.Text + "'";

                using (SqlCommand cmd = new SqlCommand(queryEliminarProveedores, connection))
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Se ha eliminado el proveedor en la base de datos.");
                    }
                }

                connection.Close();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textProveedorIDActualizar.Text))
            {
                MessageBox.Show("Debe introducir un ID válido.");
                return;
            }

            if (string.IsNullOrEmpty(txtProveedorActualizar.Text))
            {
                MessageBox.Show("El nombre está incorrecto o vacio.");
                return;
            }

            if (string.IsNullOrEmpty(txtTelefonoActualizar.Text))
            {
                MessageBox.Show("El Correo está incorrecto o vacio.");
                return;
            }

            if (string.IsNullOrEmpty(txtCorreoElectronicoActualizar.Text))
            {
                MessageBox.Show("El Telefono está incorrecto o vacia.");
                return;
            }


            string connectionString = @"Data Source=JEREMY;Initial Catalog=PROYECTO3;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string queryActualizarProveedores = @"UPDATE Proveedores
                                                    SET 
                                                        ProveedorID = '" + textProveedorIDActualizar.Text + "', " +
                                                        "NombreProveedor = '" + txtProveedorActualizar.Text + "',  " +
                                                        "Telefono = '" + txtTelefonoActualizar.Text + "', " +
                                                        "CorreoElectronico = '" + txtCorreoElectronicoActualizar.Text + "'" +
                                                    "WHERE ProveedorID = '" + textProveedorIDActualizar.Text + "'";

                using (SqlCommand cmd = new SqlCommand(queryActualizarProveedores, connection))
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Se ha actualizado al cliente en la base de datos.");
                    }
                }

                connection.Close();

            }
        }
    }
}




