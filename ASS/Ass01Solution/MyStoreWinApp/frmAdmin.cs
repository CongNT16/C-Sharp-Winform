using BusinessObject;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyStoreWinApp
{
    public partial class frmAdmin : Form
    {
        private IMemberRepository memberRepository = new MemberRepository();
        private BindingSource source;
        public frmAdmin()
        {
            InitializeComponent();
        }

        private void frmAdmin_Load(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            btnSave.Enabled = false;
            //dgvMembers.CellDoubleClick+= dgvMembers_CellDoubleClick;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {

        }

        private void LoadMembers(List<MemberObject> members)
        {
            try
            {
                source = new BindingSource();
                source.DataSource = memberRepository.GetMemberObjects();
                txtMemberId.DataBindings.Clear();
                txtMemberName.DataBindings.Clear();
                txtEmail.DataBindings.Clear();
                txtPassword.DataBindings.Clear();
                cbCity.DataBindings.Clear();
                cbCountry.DataBindings.Clear();


                txtMemberId.DataBindings.Add("Text", source, "memberId");
                txtMemberName.DataBindings.Add("Text", source, "memberName");
                txtEmail.DataBindings.Add("Text", source, "email");
                txtPassword.DataBindings.Add("Text", source, "password");
                cbCity.DataBindings.Add("Text", source, "city");
                cbCountry.DataBindings.Add("Text", source, "country");

                DataTable memberObjectsTb = new DataTable();
                memberObjectsTb.Columns.Add("MemberID", typeof(int));
                memberObjectsTb.Columns.Add("Member Name", typeof(string));
                memberObjectsTb.Columns.Add("Email", typeof(string));
                memberObjectsTb.Columns.Add("Password", typeof(string));
                memberObjectsTb.Columns.Add("City", typeof(string));
                memberObjectsTb.Columns.Add("Country", typeof(string));

                foreach (MemberObject member in members)
                {
                    memberObjectsTb.Rows.Add(member.GetMemberId(), member.GetMemberName(), member.GetEmail(), member.GetPassword(), member.GetCity(), member.GetCountry());
                }
                dgvMembers.DataSource = memberObjectsTb;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load members");
            }
        }

        private void txtSearchMemberId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    int memberId = int.Parse(txtSearchMemberId.Text);
                    MemberObject memberObject = memberRepository.GetMemberObjectById(memberId);
                    List<MemberObject> members = new List<MemberObject>();
                    members.Add(memberObject);
                    LoadMembers(members);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void txtSearchName_TextChanged(object sender, EventArgs e)
        {
            string memberName = txtSearchName.Text;
            List<MemberObject> members = memberRepository.GetMemberObjectByName(memberName);
            LoadMembers(members);
        }

        private void cbCityFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string city = cbCityFilter.Text;
                LoadMembers(memberRepository.GetMemberObjectByCity(city));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbCountryFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string country = cbCountryFilter.Text;
                LoadMembers(memberRepository.GetMemberObjectByCountry(country));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSavePW_Click(object sender, EventArgs e)
        {
            int memberId = int.Parse(txtMemberId.Text);
            string newPassword = txtPassword.Text;
            if (memberRepository.SavePassword(memberId, newPassword))
            {
                MessageBox.Show("Save Password Success.");
            }
            else
            {
                MessageBox.Show("Save Password Fail.");
            }
        }

        //private void dgvMembers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    try
        //    {
        //        source = new BindingSource();
        //        source.DataSource = memberRepository.GetMemberObjects();
        //        txtMemberId.DataBindings.Clear();
        //        txtMemberName.DataBindings.Clear();
        //        txtEmail.DataBindings.Clear();
        //        txtPassword.DataBindings.Clear();
        //        cbCity.DataBindings.Clear();
        //        cbCountry.DataBindings.Clear();

              
        //        txtMemberId.DataBindings.Add("Text", source, "memberId");
        //        txtMemberName.DataBindings.Add("Text", source, "memberName");
        //        txtEmail.DataBindings.Add("Text", source, "email");
        //        txtPassword.DataBindings.Add("Text", source, "password");
        //        cbCity.DataBindings.Add("Text", source, "city");
        //        cbCountry.DataBindings.Add("Text", source, "country");
        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Load");
        //    }
        //}

        private void ClearText()
        {
            txtMemberId.Text = string.Empty;
            txtMemberName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPassword.Text = string.Empty;
            cbCity.Text = string.Empty;
            cbCountry.Text = string.Empty;
        }
    }
}
