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
    public partial class Clientes : Form
    {
        public Clientes()
        {
            InitializeComponent();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            // TODO:  Debes cambiar esta variable connectionString para que pueda conectarse a tu base de datos.
            string connectionString = @"Data Source=JEREMY;Initial Catalog=PROYECTO3;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string queryClientes = "SELECT * FROM Clientes;";

                using (SqlCommand cmd = new SqlCommand(queryClientes, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dgClientes.DataSource = dt;
                    }
                }

                connection.Close();
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Validaciones para evitar insertar datos erroneos.
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Si el Codigo esta incorrecto o vacio.");
                return;
            }
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("El nombre está incorrecto o vacio.");
                return;
            }

            if (string.IsNullOrEmpty(txtCorreo.Text))
            {
                MessageBox.Show("El Correo está incorrecto o vacio.");
                return;
            }

            if (string.IsNullOrEmpty(txtDireccion.Text))
            {
                MessageBox.Show("La fecha la direccion está incorrecta o vacia.");
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

                string queryInsertarClientes = @"INSERT INTO Clientes (ClienteID, nombreCompleto, CorreoElectronico, Telefono, Direccion)
                                           VALUES ('" + txtId.Text + "', '" + txtNombre.Text + "','" + txtCorreo.Text + "'," +
                                                   "'" + txtDireccion.Text + "'," +
                                                   "'" + txtTelefono.Text + "')";

                using (SqlCommand cmd = new SqlCommand(queryInsertarClientes, connection))
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

            string connectionString = @"Data Source=JEREMY;Initial Catalog=PROYECTO3;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string queryEliminarClientes = @"DELETE FROM Clientes WHERE ClienteID = '" + txtEliminar.Text + "'";

                using (SqlCommand cmd = new SqlCommand(queryEliminarClientes, connection))
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Se ha eliminado al cliente en la base de datos.");
                    }
                }

                connection.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {


            if (string.IsNullOrEmpty(textIDActualizar.Text))
            {
                MessageBox.Show("Debe introducir un ID válido.");
                return;
            }

            if (string.IsNullOrEmpty(txtNombreActualizar.Text))
            {
                MessageBox.Show("El nombre está incorrecto o vacio.");
                return;
            }

            if (string.IsNullOrEmpty(txtCorreoActualiza.Text))
            {
                MessageBox.Show("El Correo está incorrecto o vacio.");
                return;
            }

            if (string.IsNullOrEmpty(txtTelefonoActualizar.Text))
            {
                MessageBox.Show("El Telefono está incorrecto o vacia.");
                return;
            }
            if (string.IsNullOrEmpty(txtCorreoActualiza.Text))
            {
                MessageBox.Show("La direccion esta incorrecta o vacia.");
                return;
            }

            // TODO:  Debes cambiar esta variable connectionString para que pueda conectarse a tu base de datos.
            string connectionString = @"Data Source=JEREMY;Initial Catalog=PROYECTO3;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string queryActualizarClientes = @"UPDATE Clientes 
                                                    SET 
                                                        ClienteID = '" + textIDActualizar.Text + "', " +
                                                        "NombreCompleto = '" + txtNombreActualizar.Text + "',  " +
                                                        "Direccion = '" + txtCorreoActualiza.Text + "', " +
                                                        "CorreoElectronico = '" + txtCorreoActualizar.Text + "', " +
                                                        "Telefono = '" + txtTelefonoActualizar.Text + "'" +
                                                    "WHERE ClienteID = '" + textIDActualizar.Text + "'";

                using (SqlCommand cmd = new SqlCommand(queryActualizarClientes, connection))
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
 
    
