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
    public partial class Productos : Form
    {
        public Productos()
        {
            InitializeComponent();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            {

                string connectionString = @"Data Source=JEREMY;Initial Catalog=PROYECTO3;Integrated Security=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string queryProductos = "SELECT * FROM Productos;";

                    using (SqlCommand cmd = new SqlCommand(queryProductos, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            dgProductos.DataSource = dt;
                        }
                    }

                    connection.Close();
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtProductoID.Text.Trim(), out int productoID))
            {
                MessageBox.Show("El ID del producto es inválido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombre = txtNombreProducto.Text.Trim();
            string descripcion = txtDescripcion.Text.Trim();

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(descripcion))
            {
                MessageBox.Show("El nombre o la descripción están vacíos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrecio.Text.Trim(), out decimal precio))
            {
                MessageBox.Show("El precio es inválido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtStock.Text.Trim(), out int stock))
            {
                MessageBox.Show("El stock es inválido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtCategoriaID.Text.Trim(), out int categoriaID))
            {
                MessageBox.Show("El ID de la categoría es inválido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Conexion a mi base de datos
            string connectionString = @"Data Source=JEREMY;Initial Catalog=PROYECTO3;Integrated Security=True";

           
            string queryInsertar = "INSERT INTO Productos (ProductoID, NombreProducto, Descripcion, Precio, Stock, CategoriaID) " +
                                   "VALUES (@id, @nombre, @descripcion, @precio, @stock, @categoriaID)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(queryInsertar, connection))
            {
                // Parámetros
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = productoID;
                cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 100).Value = nombre;
                cmd.Parameters.Add("@descripcion", SqlDbType.VarChar, 200).Value = descripcion;
                cmd.Parameters.Add("@precio", SqlDbType.Decimal).Value = precio;
                cmd.Parameters.Add("@stock", SqlDbType.Int).Value = stock;
                cmd.Parameters.Add("@categoriaID", SqlDbType.Int).Value = categoriaID;

                try
                {
                    connection.Open();
                    int filasInsertadas = cmd.ExecuteNonQuery();

                    if (filasInsertadas > 0)
                    {
                        MessageBox.Show("Producto guardado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo guardar el producto.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error al guardar el producto: " + ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
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

                string queryEliminarProductos = @"DELETE FROM Productos WHERE ProductoID = '" + txtEliminar.Text + "'";

                using (SqlCommand cmd = new SqlCommand(queryEliminarProductos, connection))
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Se ha eliminado el producto en la base de datos.");
                    }
                }

                connection.Close();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtProductoIDActualizar.Text.Trim(), out int productoID))
            {
                MessageBox.Show("El ID del producto es inválido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nuevoNombre = txtNombreProductoActualizar.Text.Trim();
            string nuevaDescripcion = txtDescripcionActualizar.Text.Trim();

            if (string.IsNullOrEmpty(nuevoNombre))
            {
                MessageBox.Show("El nuevo nombre está vacío.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrecioActualizar.Text.Trim(), out decimal nuevoPrecio))
            {
                MessageBox.Show("El nuevo precio es inválido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtStockActualizar.Text.Trim(), out int nuevoStock))
            {
                MessageBox.Show("El nuevo stock es inválido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtCategoriaIDActualizar.Text.Trim(), out int nuevaCategoriaID))
            {
                MessageBox.Show("El ID de la nueva categoría es inválido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            string connectionString = @"Data Source=JEREMY;Initial Catalog=PROYECTO3;Integrated Security=True;";

            // Consulta SQL parametrizada para actualizar el producto
            string queryActualizar = @"UPDATE Productos 
                               SET NombreProducto = @nombre, 
                                   Descripcion = @descripcion, 
                                   Precio = @precio, 
                                   Stock = @stock, 
                                   CategoriaID = @categoriaID 
                               WHERE ProductoID = @id";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand comando = new SqlCommand(queryActualizar, con))
            {
                comando.Parameters.Add("@id", SqlDbType.Int).Value = productoID;
                comando.Parameters.Add("@nombre", SqlDbType.VarChar, 100).Value = nuevoNombre;
                comando.Parameters.Add("@descripcion", SqlDbType.VarChar, 255).Value = nuevaDescripcion;
                comando.Parameters.Add("@precio", SqlDbType.Decimal).Value = nuevoPrecio;
                comando.Parameters.Add("@stock", SqlDbType.Int).Value = nuevoStock;
                comando.Parameters.Add("@categoriaID", SqlDbType.Int).Value = nuevaCategoriaID;

                try
                {
                    con.Open();
                    int filasAfectadas = comando.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Producto actualizado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No se encontró un producto con ese ID.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error al actualizar el producto: " + ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            //Para limpiar los campos después de actualizar
            txtProductoIDActualizar.Clear();
            txtNombreProductoActualizar.Clear();
            txtDescripcionActualizar.Clear();
            txtPrecioActualizar.Clear();
            txtStockActualizar.Clear();
            txtCategoriaIDActualizar.Clear();


        }
    }
}