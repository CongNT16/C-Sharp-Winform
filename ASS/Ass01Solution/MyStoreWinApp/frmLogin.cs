using BusinessObject;
using DataAccess.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyStoreWinApp
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        public IMemberRepository memberRepository = new MemberRepository();

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Admin admin = new();
                using (StreamReader sr = File.OpenText("appsettings.json"))
                {
                    var obj = sr.ReadToEnd();
                    admin = JsonConvert.DeserializeObject<Admin>(obj);
                }

                string email = txtEmail.Text;
                string password = txtPassword.Text;

                if (email.Equals(admin.email) && password.Equals(admin.password))
                {
                    frmAdmin frmAdmin = new frmAdmin();
                    if (frmAdmin.ShowDialog() == DialogResult.OK)
                    {
                        Close();
                    }
                }
                else { 
                    MemberObject memberObject = memberRepository.Authenticate(email, password);
                    frmMemberManagement frmMemberManagement = new frmMemberManagement(memberObject);
                    if (frmMemberManagement.ShowDialog() == DialogResult.OK)
                    {
                        Close();
                    }
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Login fail");
            }
             
        }
    }
}
