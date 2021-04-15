﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices; // Librería que permite utilizar librerías nativas del sistema operativo
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal
{
    public partial class RegistrerUsers : Form{
        public RegistrerUsers(){
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        // <--------------------------------------> //
        // <---------- EVENTOS / EVENTS ----------> //
        // <--------------------------------------> //

        // <--- Evento #1: "Enter" ---> //
        /*
           Este evento sólo se "activará" si el cursor del Mouse se encuentra dentro de la 
           caja de texto.
        */
        private void txtSignName_Enter(object sender, EventArgs e){
            if (txtSignName.Text == "NAME"){
                txtSignName.Text = ""; // Texto a mostrar.
                txtSignName.ForeColor = Color.FromArgb(64, 64, 64); // Color de texto.
            }
        }

        // <--- Evento #2: "Leave" ---> //
        /*
           Este evento sólo se "activará" cuando el cursor del Mouse se encuentre fuera de
           la caja de texto.
        */
        private void txtSignName_Leave(object sender, EventArgs e){
            if (txtSignName.Text == ""){
                txtSignName.Text = "NAME"; // Texto a mostrar.
                txtSignName.ForeColor = Color.FromArgb(64, 64, 64); // Color de texto.
            }
        }

        // <--- Evento #3: "Enter" ---> //
        /*
           Este evento sólo se "activará" si el cursor del Mouse se encuentra dentro de la 
           caja de texto.
        */
        private void txtSignLastName_Enter(object sender, EventArgs e){
            if (txtSignLastName.Text == "LAST NAME"){
                txtSignLastName.Text = ""; // Texto a mostrar.
                txtSignLastName.ForeColor = Color.FromArgb(64, 64, 64); // Color de texto.
            }
        }

        // <--- Evento #4: "Leave" ---> //
        /*
           Este evento sólo se "activará" cuando el cursor del Mouse se encuentre fuera de
           la caja de texto.
        */
        private void txtSignLastName_Leave(object sender, EventArgs e){
            if (txtSignLastName.Text == ""){
                txtSignLastName.Text = "LAST NAME"; // Texto a mostrar.
                txtSignLastName.ForeColor = Color.FromArgb(64, 64, 64); // Color de texto.
            }
        }

        // <--- Evento #5: "Enter" ---> //
        /*
           Este evento sólo se "activará" si el cursor del Mouse se encuentra dentro de la 
           caja de texto.
        */
        private void txtSignUsername_Enter(object sender, EventArgs e){
            if (txtSignUsername.Text == "USERNAME"){
                txtSignUsername.Text = ""; // Texto a mostrar.
                txtSignUsername.ForeColor = Color.FromArgb(64, 64, 64); // Color de texto.
            }
        }

        // <--- Evento #6: "Leave" ---> //
        /*
           Este evento sólo se "activará" cuando el cursor del Mouse se encuentre fuera de
           la caja de texto.
        */
        private void txtSignUsername_Leave(object sender, EventArgs e){
            if (txtSignUsername.Text == ""){
                txtSignUsername.Text = "USERNAME"; // Texto a mostrar.
                txtSignUsername.ForeColor = Color.FromArgb(64, 64, 64); // Color de texto.
            }
        }

        // <--- Evento #7: "Enter" ---> //
        /*
           Este evento sólo se "activará" si el cursor del Mouse se encuentra dentro de la 
           caja de texto.
        */
        private void txtSignEmail_Enter(object sender, EventArgs e){
            if (txtSignEmail.Text == "EMAIL ADDRESS"){
                txtSignEmail.Text = ""; // Texto a mostrar.
                txtSignEmail.ForeColor = Color.FromArgb(64, 64, 64); // Color de texto.
            }
        }

        // <--- Evento #8: "Leave" ---> //
        /*
           Este evento sólo se "activará" cuando el cursor del Mouse se encuentre fuera de
           la caja de texto.
        */
        private void txtSignEmail_Leave(object sender, EventArgs e){
            if (txtSignEmail.Text == ""){
                txtSignEmail.Text = "EMAIL ADDRESS"; // Texto a mostrar.
                txtSignEmail.ForeColor = Color.FromArgb(64, 64, 64); // Color de texto.
            }
        }

        // <--- Evento #9: "Enter" ---> //
        /*
           Este evento sólo se "activará" si el cursor del Mouse se encuentra dentro de la 
           caja de texto.
        */
        private void txtSignPassword_Enter(object sender, EventArgs e){
            if (txtSignPassword.Text == "PASSWORD"){
                txtSignPassword.Text = ""; // Texto a mostrar.
                txtSignPassword.ForeColor = Color.FromArgb(64, 64, 64); // Color de texto.
                txtSignPassword.UseSystemPasswordChar = true; // Esta condición permite que la contraseña no se visualice.
            }
        }

        // <--- Evento #10: "Leave" ---> //
        /*
           Este evento sólo se "activará" cuando el cursor del Mouse se encuentre fuera de
           la caja de texto.
        */
        private void txtSignPassword_Leave(object sender, EventArgs e){
            if (txtSignPassword.Text == ""){
                txtSignPassword.Text = "PASSWORD"; // Texto a mostrar.
                txtSignPassword.ForeColor = Color.FromArgb(64, 64, 64); // Color de texto.
                txtSignPassword.UseSystemPasswordChar = false; // Esta condición permite que la contraseña no se visualice.
            }
        }

        // <--- Evento #11: "Enter" ---> //
        /*
           Este evento sólo se "activará" si el cursor del Mouse se encuentra dentro de la 
           caja de texto.
        */
        private void txtConfirmPass_Enter(object sender, EventArgs e){
            if (txtConfirmPass.Text == "CONFIRM PASSWORD"){
                txtConfirmPass.Text = ""; // Texto a mostrar.
                txtConfirmPass.ForeColor = Color.FromArgb(64, 64, 64); // Color de texto.
                txtConfirmPass.UseSystemPasswordChar = true; // Esta condición permite que la contraseña no se visualice.
            }
        }

        // <--- Evento #12: "Leave" ---> //
        /*
           Este evento sólo se "activará" cuando el cursor del Mouse se encuentre fuera de
           la caja de texto.
        */
        private void txtConfirmPass_Leave(object sender, EventArgs e){
            if (txtConfirmPass.Text == ""){
                txtConfirmPass.Text = "CONFIRM PASSWORD"; // Texto a mostrar.
                txtConfirmPass.ForeColor = Color.FromArgb(64, 64, 64); // Color de texto.
                txtConfirmPass.UseSystemPasswordChar = false; // Esta condición permite que la contraseña no se visualice.
            }
        }

        // <---------------------------------------> //
        // <---------- BOTONES / BUTTONS ----------> //
        // <---------------------------------------> //

        // <--- Botón "btnRegister" ---> //
        private void btnRegister_Click(object sender, EventArgs e){
            registro_usuarios user = new registro_usuarios(); // Creación de un objeto de la clase "registro_usuarios".

            /* 
               Asiganción de los datos de los "textbox" del formulario de registro a las propiedades 
               del objeto "usuario".
            */
            user.Name1 = txtSignName.Text;
            user.Last_name1 = txtSignLastName.Text;
            user.Username1 = txtSignUsername.Text;
            user.Email1 = txtSignEmail.Text;
            user.Password1 = txtSignPassword.Text;
            user.ConPassword1 = txtConfirmPass.Text;

            try{
                Control control = new Control(); // Creación de un objeto de la clase "Control".
                string answer = control.ctrlRegister(user); // Llamada al método "ctrlRegistro", enviandole como parámetro el objeto "usuario".

                if (answer.Length > 0){
                    // "MessageBox" que se mostrará al usuario para avisar de algun error.
                    MessageBox.Show(answer, "Aviso.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else{
                    // "MessageBox" que se mostrará al usuario para confirmar su registro.
                    if(MessageBox.Show("¡Usuario registrado! \n¡Ya puede iniciar sesión y empezar a utilizar nuestra aplicación!", "Datos registrados.", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK){
                        Login LoginForm = new Login(); // Creación de un objeto de la clase "Login".
                        LoginForm.Show(); // ".Show()" permitirá mostrar el formulario de inicio de sesión.
                        this.Hide(); // ".Hide() ocultará el formulario actual (RegistrerUsers).
                    }           
                }
            } 
            catch(Exception ex){
                MessageBox.Show(ex.Message);
            }
        }

        // <--- Botón "btnClear". ---> //
        private void btnClear_Click(object sender, EventArgs e){
            // Al momento de presionar el botón, todos los textbox del formulario volverán a su estado inicial.
            txtSignName.Text = "NAME";
            txtSignLastName.Text = "LAST NAME";
            txtSignUsername.Text = "USERNAME";
            txtSignEmail.Text = "EMAIL ADDRESS";
            txtSignPassword.Text = "PASSWORD";
            txtSignPassword.UseSystemPasswordChar = false; // Esta condición permite que la contraseña se visualice.
            txtConfirmPass.Text = "CONFIRM PASSWORD";
            txtConfirmPass.UseSystemPasswordChar = false; // Esta condición permite que la contraseña se visualice.

            txtSignName.Focus(); // El cursos volverá a la primera caja de texto (En este caso, txtSignName).
        }

        // <--- Botón "btnClose". ---> //
        private void btnClose_Click(object sender, EventArgs e){
            /* 
               Condición que sólo funcionará sí y sólo sí el usuario presiona el botón de
               "YES" del MessageBox.
            */
            if(MessageBox.Show("¿Seguro que quiere volver a la pantalla principal? \n¡Todos sus datos no se registrarán y se perderán!", "¡Cuidado!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes){
                Login LoginForm = new Login(); // Creación de un objeto de la clase "Login".
                LoginForm.Show(); // ".Show()" permitirá mostrar el formulario de inicio de sesión.
                this.Hide(); // ".Hide() ocultará el formulario actual (RegistrerUsers).
            }
        }

        // <--- Botón "btnMinimize". ---> //
        private void btnMinimize_Click(object sender, EventArgs e){
            this.WindowState = FormWindowState.Minimized; // Permite minimizar la ventana.
        }
    }
}
