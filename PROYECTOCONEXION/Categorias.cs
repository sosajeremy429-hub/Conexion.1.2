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
    public partial class Categorias : Form
    {
        public Categorias()
        {
            InitializeComponent();
        }

        private void bntAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCategoriaID.Text))
            {
                MessageBox.Show("Si el Codigo esta incorrecto o vacio.");
                return;
            }
            if (string.IsNullOrEmpty(txtNombreCategoria.Text))
            {
                MessageBox.Show("El nombre está incorrecto o vacio.");
                return;
            }



            // TODO:  Debes cambiar esta variable connectionString para que pueda conectarse a tu base de datos.
            string connectionString = @"Data Source=JEREMY;Initial Catalog=PROYECTO3;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string queryInsertarCategorias = @"INSERT INTO Categorias (CategoriaID, nombreCategoria)
                                           VALUES ('" + txtCategoriaID.Text + "', '" + txtNombreCategoria.Text + "')";

                using (SqlCommand cmd = new SqlCommand(queryInsertarCategorias, connection))
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Se ha insertado la categoria en la base de datos.");
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

                string queryClientes = "SELECT * FROM Categorias;";

                using (SqlCommand cmd = new SqlCommand(queryClientes, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dgCategorias.DataSource = dt;
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

                string queryEliminarCategorias = @"DELETE FROM Categorias WHERE CategoriaID = '" + txtEliminar.Text + "'";

                using (SqlCommand cmd = new SqlCommand(queryEliminarCategorias, connection))
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Se ha eliminado la categoria en la base de datos.");
                    }
                }

                connection.Close();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtCategoriaIDActualizar.Text.Trim(), out int categoriaID))
            {
                MessageBox.Show("El ID de la categoría es inválido o está vacío.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validación del nombre
            string nombreCategoria = txtNombreCategoriaActualizar.Text.Trim();
            if (string.IsNullOrEmpty(nombreCategoria))
            {
                MessageBox.Show("El nombre de la categoría está vacío.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Cadena de conexión a la base de datos Costal
            string connectionString = @"Data Source=JEREMY;Initial Catalog=PROYECTO3;Integrated Security=True";

            // Consulta SQL parametrizada
            string queryActualizar = "UPDATE Categorias SET NombreCategoria = @nombre WHERE CategoriaID = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(queryActualizar, connection))
            {
                // Parámetros seguros
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = categoriaID;
                cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = nombreCategoria;

                try
                {
                    connection.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Categoría actualizada correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No se encontró una categoría con ese ID.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error al actualizar la categoría: " + ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

         
            txtCategoriaIDActualizar.Clear();
            txtNombreCategoriaActualizar.Clear();
        }

    }
}


