﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MessageNest.Forms
{
    public partial class FrmAdmin : Form
    {
        public FrmAdmin()
        {
            InitializeComponent();

            OpenChildForm(new FrmAdminInfo());
        }

        #region Call Child Forms

        private Form currentForm = null;

        public void OpenChildForm(Form childForm)
        {
            if (currentForm != null)
            {
                currentForm.Close();
            }
            
            currentForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            PnlChildForm.Controls.Add(childForm);
            PnlChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        #endregion

        #region Botones

        private void BtnCloseAdmin_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnCloseAdmin_MouseHover(object sender, EventArgs e)
        {
            BtnCloseAdmin.BackColor = Color.Red;
            BtnCloseAdmin.Image = Properties.Resources.exit;
        }

        private void BtnCloseAdmin_MouseLeave(object sender, EventArgs e)
        {
            BtnCloseAdmin.BackColor = Color.FromArgb(40, 40, 40);
            BtnCloseAdmin.Image = Properties.Resources.close_button;
        }

        private void BtnAdminInfo_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmAdminInfo());
        }

        private void BtnModifyAdmin_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmEditAdmin());
        }

        private void BtnAddUsr_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmAddUser());
        }

        private void BtnSrchUsr_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmSearchUser());
        }

        private void BtnLogOut_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿Está seguro que quiere salir?", "Confirmación", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                FrmLogin frmLogin = new FrmLogin();

                this.Hide();
                frmLogin.Show();
            }
        }

        #endregion

        #region Arrastrar Form

        [DllImport("user32.Dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.Dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void PnlTopAdmin_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0xA1, 0x2, 0);
        }

        #endregion
    }
}
