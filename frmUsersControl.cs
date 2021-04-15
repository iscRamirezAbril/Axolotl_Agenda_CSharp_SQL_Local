using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using MySql.Data.MySqlClient; // Librería que nos permitirá conectarnos a las bases de datos de "MySQL".
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal
{
    public partial class frmUsersControl : Form{

        // Constructor.
        public frmUsersControl(){
            InitializeComponent();
            ReloadUsersTable(null); // Llamada al método que permitirá actaulizar la tabla de usuarios.
        }

        // <---------------------------------------> //
        // <---------- BOTONES / BUTTONS ----------> //
        // <---------------------------------------> //

        // <--- Botón "iconbtnSearch". ---> //
        /*
           Este botón permitirá buscar un usuario en la tabla de usuarios registrados.
        */
        private void iconbtnSearch_Click(object sender, EventArgs e){
            string dato = txtSearch.Text;
            ReloadUsersTable(dato);
        }

        // <--- Botón "iconSave". ---> //
        /*
           Este botón permitirá guardar y registrar un nuevo usuario.
        */
        private void iconbtnSave_Click(object sender, EventArgs e){
            registro_usuarios user = new registro_usuarios(); // Creación de un objeto de la clase "registro_usuarios".

            /* 
               Asiganción de los datos de los "textbox" del formulario de registro a las propiedades 
               del objeto "usuario".
            */
            user.Name1 = txtName.Text;
            user.Last_name1 = txtLastName.Text;
            user.Username1 = txtUsername.Text;
            user.Email1 = txtEmail.Text;
            user.Password1 = txtPass.Text;

            try{
                Control ctrl = new Control(); // Creación de un objeto de la clase "Control".
                string respuesta = ctrl.ctrlregisterAdmins(user); // Llamada al método "ctrlRegistro", enviandole como parámetro el objeto "usuario".
                                          // "MessageBox" que se mostrará al usuario para avisar de algun error.
                if (respuesta.Length > 0) MessageBox.Show(respuesta, "Aviso.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else{
                    // "MessageBox" que se mostrará al usuario para confirmar su registro.
                    MessageBox.Show("¡Usuario registrado con éxito!", "Datos registrados.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clean(); // Llamada al método para limpiar los textbox.
                    ReloadUsersTable(null); // Llamada al método que permite actualizar la tabla.
                }
            }
            // Catch que sólo se activará si el administrador presenta algún tipo de error al registrar un usuario.
            catch (Exception ex){
                MessageBox.Show(ex.Message); // Mensaje de error.
            }
        }

        // <--- Botón "btnModify". ---> //
        /*
           Botón que permitirá modificar los datos de un usuario.
        */
        private void btnModify_Click(object sender, EventArgs e) {
            Control objCtrl = new Control(); // Creación de una instancia de la clase "Control".

            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtLastName.Text) || string.IsNullOrEmpty(txtUsername.Text) ||
                string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPass.Text))
                // Mensaje de error.
                MessageBox.Show("Debe llenar todos los campos...", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else{
                // Declaración de variables, a las cuales se les asigna el dato correspondiente al textbox.
                int id = Convert.ToInt32(txtId.Text);
                string name = txtName.Text;
                string last_name = txtLastName.Text;
                string username = txtUsername.Text;
                string email = txtEmail.Text;
                string password = objCtrl.Encrypt(txtPass.Text);

                // Declaración de variable.
                /*
                   Esta variable selecciona de la tabla "registro_usuarios" los siguientes datos de los campos correspondientes:
                   1. name.       3. username.  5. id.
                   2. last_name.  4. email.     6. password.
                   Seleccina esos campos para tener acceso a ellos y poder actualizarlos.
                */
                string sql = "UPDATE registro_usuarios SET name='" + name + "', last_name='" + last_name + "', username='" + username + "', email='" + email + "', password='" + password + "' WHERE id='" + id + "'";
                
                // Referencia a la clase de nombre "SQLConnection".
                MySqlConnection connection = SQLConnection.getConnection();
                connection.Open();  // Esta función permite abrir la conexión.

                try{
                    // Se crea un objeto de la clase "MySqlCommand", enviandole como parámetros "sql" y "conexion".
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    command.ExecuteNonQuery(); // Devuelve el número de usuarios actualizados.

                    // MessageBox que se muestra cuando los datos del usuario se modifican con éxito.
                    MessageBox.Show("¡Datos de usuario modificados con éxito!", "Datos de usuario actualizados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clean(); // Llamada al método para limpiar los textbox.
                    ReloadUsersTable(null); // Llamada al método que permite actualizar la tabla.
                }

                // Catch correspondiente por si se presenta algún error al momento de actualizar los datos.
                catch (MySqlException ex){
                    MessageBox.Show("Error al modifcar: " + ex.Message); // Mensaje de error.
                }
            }
        }

        // <--- Botón "iconbtnDelete". ---> //
        /*
           Botón que permitirá eliminar un usuario.
        */
        private void iconbtnDelete_Click(object sender, EventArgs e){
            // Declaración de una variable que almacenará el id del usuario.
            int id = Convert.ToInt32(txtId.Text);

            // Condición que sólo se ejecutará si el administrador dá click al botón "Yes" del MessageBox.
            if(MessageBox.Show("¿Seguro que quiere eliminar a este usuario de la Base de Datos?" +
                "\n¡No podrá recuperar los datos!", "¡Aviso!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes){
                // Variable que almacenará el comando "DELETE" de SQL.
                string sql = "DELETE FROM registro_usuarios WHERE id='"+ id +"'";

                // Referencia a la clase de nombre "SQLConnection".
                MySqlConnection connection = SQLConnection.getConnection();
                connection.Open();  // Esta función permite abrir la conexión.

                try{
                    // Se crea un objeto de la clase "MySqlCommand", enviandole como parámetros "sql" y "conexion".
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    command.ExecuteNonQuery(); // Devuelve el número de usuarios actualizados.

                    // MessageBox que se muestra cuando los datos del usuario se eliminan con éxito.
                    MessageBox.Show("¡Usuario eliminado de la Base de Datos!", "Usuario eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clean(); // Llamada al método para limpiar los textbox.
                    ReloadUsersTable(null); // Llamada al método que permite actualizar la tabla.
                }

                // Catch correspondiente por si se presenta algún error al momento de eliminar un usuario.
                catch (MySqlException ex){
                    MessageBox.Show("Error al eliminar: " + ex.Message); // Mensaje de error.
                }
            }
        }

        // <--- Botón "iconbtnClean". ---> //
        /*
           Botón que reinicia los textboxes a su estado original.
        */
        private void iconbtnClean_Click(object sender, EventArgs e){
            Clean(); // Llamada al método que reinicia los textboxes.
        }

        // <---------------------------------------> //
        // <---------- MÉTODOS / METHODS ----------> //
        // <---------------------------------------> //

        // <--- Método #1: Cargar datos a "DataGridView". ---> //
        private void ReloadUsersTable(string dato){
            List < registro_usuarios > Lista = new List<registro_usuarios>();
            Model ctrlUsuarios = new Model();
            dataGridUsers.DataSource = ctrlUsuarios.userQuery(dato);
        }

        // <--- Método #2: Método que permitirá limpiar las "textboxes" del formulario. ---> //
        private void Clean(){
            txtId.Text = "ID";
            txtName.Text = "NAME";
            txtLastName.Text = "LAST NAME";
            txtUsername.Text = "USERNAME";
            txtEmail.Text = "EMAIL";
            txtPass.Text = "PASSWORD";
            txtIdTipo.Text = "ID_TIPO";
        }

        // <--------------------------------------> //
        // <---------- EVENTOS / EVENTS ----------> //
        // <--------------------------------------> //

        // <--- Evento #1: "Enter". ---> //
        /*
            Este evento sólo se "activará" si el cursor del Mouse se encuentra dentro de la 
            caja de texto.
        */
            private void txtSearch_Enter(object sender, EventArgs e){
                if (txtSearch.Text == "SEARCH"){
                    txtSearch.Text = ""; // Texto a mostrar.
                    txtSearch.ForeColor = Color.FromArgb(64, 64, 64); // Color de texto.
                }
            }

        // <--- Evento #2: "Leave". ---> //
        /*
           Este evento sólo se "activará" cuando el cursor del Mouse se encuentre fuera de
           la caja de texto.
        */
        private void txtSearch_Leave(object sender, EventArgs e){
            if (txtSearch.Text == ""){
                txtSearch.Text = "SEARCH"; // Texto a mostrar.
                txtSearch.ForeColor = Color.FromArgb(64, 64, 64); // Color de texto.
            }
        }

        // <--- Evento #3: "Enter". ---> //
        /*
            Este evento sólo se "activará" si el cursor del Mouse se encuentra dentro de la 
            caja de texto.
        */
        private void txtId_Enter(object sender, EventArgs e){
            if (txtId.Text == "ID"){
                txtId.Text = ""; // Texto a mostrar.
                txtId.ForeColor = Color.FromArgb(64, 64, 64); // Color de texto.
            }
        }

        // <--- Evento #4: "Leave". ---> //
        /*
           Este evento sólo se "activará" cuando el cursor del Mouse se encuentre fuera de
           la caja de texto.
        */
        private void txtId_Leave(object sender, EventArgs e){
            if (txtId.Text == ""){
                txtId.Text = "ID"; // Texto a mostrar.
                txtId.ForeColor = Color.FromArgb(64, 64, 64); // Color de texto.
            }
        }

        // <--- Evento #5: "Enter". ---> //
        /*
            Este evento sólo se "activará" si el cursor del Mouse se encuentra dentro de la 
            caja de texto.
        */
        private void txtName_Enter(object sender, EventArgs e){
            if (txtName.Text == "NAME"){
                txtName.Text = ""; // Texto a mostrar.
                txtName.ForeColor = Color.FromArgb(64, 64, 64); // Color de texto.
            }
        }

        // <--- Evento #6: "Leave". ---> //
        /*
           Este evento sólo se "activará" cuando el cursor del Mouse se encuentre fuera de
           la caja de texto.
        */
        private void txtName_Leave(object sender, EventArgs e){
            if (txtName.Text == ""){
                txtName.Text = "NAME"; // Texto a mostrar.
                txtName.ForeColor = Color.FromArgb(64, 64, 64); // Color de texto.
            }
        }

        // <--- Evento #7: "Enter". ---> //
        /*
            Este evento sólo se "activará" si el cursor del Mouse se encuentra dentro de la 
            caja de texto.
        */
        private void txtLastName_Enter(object sender, EventArgs e){
            if (txtLastName.Text == "LAST NAME"){
                txtLastName.Text = ""; // Texto a mostrar.
                txtLastName.ForeColor = Color.FromArgb(64, 64, 64); // Color de texto.
            }
        }

        // <--- Evento #8: "Leave". ---> //
        /*
           Este evento sólo se "activará" cuando el cursor del Mouse se encuentre fuera de
           la caja de texto.
        */
        private void txtLastName_Leave(object sender, EventArgs e){
            if (txtLastName.Text == ""){
                txtLastName.Text = "LAST NAME"; // Texto a mostrar.
                txtLastName.ForeColor = Color.FromArgb(64, 64, 64); // Color de texto.
            }
        }

        // <--- Evento #9: "Enter". ---> //
        /*
            Este evento sólo se "activará" si el cursor del Mouse se encuentra dentro de la 
            caja de texto.
        */
        private void txtUsername_Enter(object sender, EventArgs e){
            if (txtUsername.Text == "USERNAME"){
                txtUsername.Text = ""; // Texto a mostrar.
                txtUsername.ForeColor = Color.FromArgb(64, 64, 64); // Color de texto.
            }
        }

        // <--- Evento #10: "Leave". ---> //
        /*
           Este evento sólo se "activará" cuando el cursor del Mouse se encuentre fuera de
           la caja de texto.
        */
        private void txtUsername_Leave(object sender, EventArgs e){
            if (txtUsername.Text == ""){
                txtUsername.Text = "USERNAME"; // Texto a mostrar.
                txtUsername.ForeColor = Color.FromArgb(64, 64, 64); // Color de texto.
            }
        }

        // <--- Evento #11: "Enter". ---> //
        /*
            Este evento sólo se "activará" si el cursor del Mouse se encuentra dentro de la 
            caja de texto.
        */
        private void txtEmail_Enter(object sender, EventArgs e){
            if (txtEmail.Text == "EMAIL"){
                txtEmail.Text = ""; // Texto a mostrar.
                txtEmail.ForeColor = Color.FromArgb(64, 64, 64); // Color de texto.
            }
        }

        // <--- Evento #10: "Leave". ---> //
        /*
           Este evento sólo se "activará" cuando el cursor del Mouse se encuentre fuera de
           la caja de texto.
        */
        private void txtEmail_Leave(object sender, EventArgs e){
            if (txtEmail.Text == ""){
                txtEmail.Text = "EMAIL"; // Texto a mostrar.
                txtEmail.ForeColor = Color.FromArgb(64, 64, 64); // Color de texto.
            }
        }

        // <--- Evento #11: "Enter". ---> //
        /*
            Este evento sólo se "activará" si el cursor del Mouse se encuentra dentro de la 
            caja de texto.
        */
        private void txtIdTipo_Enter(object sender, EventArgs e){
            if (txtIdTipo.Text == "ID_TIPO"){
                txtIdTipo.Text = ""; // Texto a mostrar.
                txtIdTipo.ForeColor = Color.FromArgb(64, 64, 64); // Color de texto.
            }
        }

        // <--- Evento #12: "Leave". ---> //
        /*
           Este evento sólo se "activará" cuando el cursor del Mouse se encuentre fuera de
           la caja de texto.
        */
        private void txtIdTipo_Leave(object sender, EventArgs e){
            if (txtIdTipo.Text == ""){
                txtIdTipo.Text = "ID_TIPO"; // Texto a mostrar.
                txtIdTipo.ForeColor = Color.FromArgb(64, 64, 64); // Color de texto.
            }
        }

        // <--- Evento #13: "Enter". ---> //
        /*
            Este evento sólo se "activará" si el cursor del Mouse se encuentra dentro de la 
            caja de texto.
        */
        private void txtPass_Enter(object sender, EventArgs e){
            if (txtPass.Text == "PASSWORD"){
                txtPass.Text = ""; // Texto a mostrar.
                txtPass.ForeColor = Color.FromArgb(64, 64, 64); // Color de texto.
            }
        }

        // <--- Evento #12: "Leave". ---> //
        /*
           Este evento sólo se "activará" cuando el cursor del Mouse se encuentre fuera de
           la caja de texto.
        */
        private void txtPass_Leave(object sender, EventArgs e){
            if (txtPass.Text == ""){
                txtPass.Text = "PASSWORD"; // Texto a mostrar.
                txtPass.ForeColor = Color.FromArgb(64, 64, 64); // Color de texto.
            }
        }

        // <--- Evento #13: "CellClick". ---> //
        /*
           Este evento servirá para programar lo que sucederá con la información de la celda seleccionada
           del "DataGridview". En este caso, al momento de seleccionar algún dato de una fila correspondiente,
           los datos de dicha fila se mostrarán en los Textboxes y ComboBoxes correspondientes.
        */
        private void dataGridUsers_CellClick(object sender, DataGridViewCellEventArgs e){
            Control objCtrl = new Control();

            // Los valores que correspondan a cierta celda de la fila del DataGridView se asginarán a los textbox.
            txtId.Text = dataGridUsers.CurrentRow.Cells[0].Value.ToString();
            txtName.Text = dataGridUsers.CurrentRow.Cells[1].Value.ToString();
            txtLastName.Text = dataGridUsers.CurrentRow.Cells[2].Value.ToString();
            txtUsername.Text = dataGridUsers.CurrentRow.Cells[3].Value.ToString();
            txtEmail.Text = dataGridUsers.CurrentRow.Cells[4].Value.ToString();
            // Debido a que la contraseña está encriptada en la base de datos, se manda la contraseña desesncriptada al textbox.
            txtPass.Text = objCtrl.Desencrypt(dataGridUsers.CurrentRow.Cells[5].Value.ToString());
            txtIdTipo.Text = dataGridUsers.CurrentRow.Cells[6].Value.ToString();
        }
    }
}
