﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices; // Librería que permite utilizar librerías nativas del sistema operativo.
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal
{
    public partial class Login : Form{
        public Login(){
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
        private void txtUser_Enter(object sender, EventArgs e){
            if(txtUser.Text == "USERNAME"){
                txtUser.Text = ""; // Texto a mostrar.
                txtUser.ForeColor = Color.FromArgb(64, 64, 64); // Color de texto.
            }
        }

        // <--- Evento #2: "Leave" ---> //
        /*
           Este evento sólo se "activará" cuando el cursor del Mouse se encuentre fuera de
           la caja de texto.
        */
        private void txtUser_Leave(object sender, EventArgs e){
            if(txtUser.Text == ""){
                txtUser.Text = "USERNAME"; // Texto a mostrar.
                txtUser.ForeColor = Color.FromArgb(64, 64, 64); // Color de texto.
            }
        }

        // <--- Evento #3: "Enter" ---> //
        /*
           Este evento sólo se "activará" si el cursor del Mouse se encuentra dentro de la 
           caja de texto.
        */
        private void txtPass_Enter(object sender, EventArgs e){
            if (txtPass.Text == "PASSWORD"){
                txtPass.Text = ""; // Texto a mostrar.
                txtPass.ForeColor = Color.FromArgb(64, 64, 64); // Color de texto.
                txtPass.UseSystemPasswordChar = true; // Esta condición permite que la contraseña no se visualice.
            }
        }

        // <--- Evento #4: "Leave". ---> //
        /*
           Este evento sólo se "activará" cuando el cursor del Mouse se encuentre fuera de
           la caja de texto.
        */
        private void txtPass_Leave(object sender, EventArgs e){
            if (txtPass.Text == ""){
                txtPass.Text = "PASSWORD"; // Texto a mostrar.
                txtPass.ForeColor = Color.FromArgb(64, 64, 64); // Color de texto.
                txtPass.UseSystemPasswordChar = false; // Esta condición permite que la contraseña se visualice.
            }
        }

        // <--- Evento #5: "MouseDown". ---> //
        /*
          Este evento nos permitirá mover el formulario desde la barra de título.
        */
        private void Login_MouseDown(object sender, MouseEventArgs e){
            // Llamada a los métodos.
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        // <--- Evento #6: "MouseDown". ---> //
        /*
          Este evento nos permitirá mover el formulario desde la barra de título.
        */
        private void panelLogo_MouseDown(object sender, MouseEventArgs e){
            // Llamada a los métodos.
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        // <--- Evento #7: "Validated". ---> //
        private void txtUser_Validated(object sender, EventArgs e){
            // Condición que funciona sólo si el "txtUser" aún no tiene escrito el nombre de usuario.
            /* 
               La instrucción ".Trim()" sirve para quitar todos los espacios en blanco,
               ya sea antes o después del texto. En otras palabras, si el usuario da espacios antes 
               o después de escribir su nombre, dichos espacios no se mostrarán en la salida de datos.
            */
            if (txtUser.Text.Trim() == "USERNAME"){
                errorPUsername.SetError(txtUser, "Para continuar, debe escribir su nombre de usuario..."); // Este es el error que se mostrará al momento de ubicar el cursor en el ícono de error.
                txtUser.Focus(); // ".Focus()" tiene la función de no permitir al usuario cambiar de textbox hasta que llene el textbox correspondiente.
            }
            else{
                errorPUsername.Clear(); // Cuando el usuario haya escrito su "username", el error desaparecerá.
            }
        }

        // <--- Evento #8: "Validated". ---> //
        private void txtPass_Validated(object sender, EventArgs e){
            // Condición que funciona sólo si el "txtPass" no tiene escrita la contraseña correspondiente".
            /* 
               La instrucción ".Trim()" sirve para quitar todos los espacios en blanco,
               ya sea antes o después del texto. En otras palabras, si el usuario da espacios antes 
               o después de escribir su nombre, dichos espacios no se mostrarán en la salida de datos.
            */
            if (txtPass.Text.Trim() == "PASSWORD"){
                errorPPassword.SetError(txtPass, "Debe escribir su contraseña para continuar..."); // Este es el error que se mostrará al momento de ubicar el cursor en el ícono de error.
                txtPass.Focus(); // ".Focus()" tiene la función de no permitir al usuario cambiar de textbox hasta que llene el textbox correspondiente.
            }
            else{
                errorPPassword.Clear(); // Cuando el usuario haya escrito su "password", el error desaparecerá.
            }
        }

        // <--- Evento #9: "LinkClicked". ---> //
        /*
           Este evento se programó para que, al momento de que el usuario quiera registrarse
           y dé click en el link de registro, se abra el formulario de registro.
        */
        private void linkLabelSignUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e){
            // Llamada al formulario de registro.
            RegistrerUsers RegisterForm = new RegistrerUsers(); // Creación de un objeto de la clase "RegistrerUsers".
            RegisterForm.Show(); // ".Show()" permitirá mostrar el formulario de registro.
            this.Hide(); // ".Hide() ocultará el formulario actual (Login).
        }

        // <--- Evento #10: "LinkClicked". ---> //
        /*
           Este evento se programó para que, al momento de que el usuario quiera recuperar
           su contraseña y dé click en el link de recuperación, se abra el formulario correspondiente.
        */
        private void linkLabelForgotPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e){
            // Llamada al formulario de la recuperación de contraseña.
            ForgotPassword frmForgotPass = new ForgotPassword(); // Creación de un objeto de la clase "ForgotPassword".
            frmForgotPass.Show(); // ".Show()" permitirá mostrar el formulario de recuperación.
            this.Hide(); // ".Hide() ocultará el formulario actual (Login).
        }

        // <---------------------------------------> //
        // <---------- BOTONES / BUTTONS ----------> //
        // <---------------------------------------> //

        // <--- Botón "btnClose". ---> //
        private void btnClose_Click(object sender, EventArgs e){
            if(MessageBox.Show("¿Seguro que quiere salir de la aplicación?", "Salida del programa.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes){
                Application.Exit(); // Salida de la aplicación.
            }
        }

        // <--- Botón "btnMinimize". ---> //
        private void btnMinimize_Click(object sender, EventArgs e){
            this.WindowState = FormWindowState.Minimized; // Permite minimizar la ventana.
        }


        // <--- Botón "Login". ---> //
        private void btnLogin_Click(object sender, EventArgs e){
            // Declaración de variables.
            /*
               Los datos ingresados por el usuario en las textbox se asignarán a las variables correspondientes.
            */
            string usuario = txtUser.Text; // Dato correspondiente a la caja de texto "txtUser".
            string password = txtPass.Text; // Dato correspondiente a la caja de texto "txtPass".

            try{
                Control ctrl = new Control(); // Creación de un objeto de la clase "Control".

                /*
                   Declaración de una variable de tipo "string" que almacenará la respuesta generada por el método
                   "ctrlLogin()".
                */
                string respuesta = ctrl.crtlLogin(usuario, password);
                /*
                   Condición que sólo es válida sí y sólo sí la respuesta tiene algún mensaje. Significa que, el
                   usuario cometió algún error al ingresar sus datos.
                */
                if (respuesta.Length > 0){ 
                    // MessageBox a mostrar.
                    MessageBox.Show(respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                /*
                   Condición que sólo es válida sí y sólo sí el usuario ingresa sus datos correctamente.
                */
                else{
                    // Llamada al formulario de la aplicación principal.
                    Form1 PrincipalForm = new Form1(); // Creación de un objeto de la clase "Form1".
                    PrincipalForm.Show(); // ".Show()" permitirá mostrar la aplicación principal.
                    this.Hide(); // ".Hide() ocultará el formulario actual (Login).
                }
            }

            /*
               Este "catch" sólo se ejecutará si el usuario deja algún textbox vacío.
            */
            catch(Exception ex){
                // MessageBox a mostrar.
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
