using BusinessObject;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyStoreWinApp
{
    public partial class frmMemberManagement : Form
    {
        private MemberObject memberInfo;
        private IMemberRepository memberRepository = new MemberRepository();
        public frmMemberManagement(MemberObject memberInfo)
        {
            this.memberInfo = memberInfo;
            InitializeComponent();
        }

        private void frmMemberManagement_Load(object sender, EventArgs e)
        {
            txtMemberId.Text = memberInfo.GetMemberId().ToString();
            txtMemberName.Text = memberInfo.GetMemberName();
            txtEmail.Text = memberInfo.GetEmail();
            txtPassword.Text = memberInfo.GetPassword();
            txtCity.Text = memberInfo.GetCity();
            txtCountry.Text = memberInfo.GetCountry();

            txtMemberId.Enabled = false;
            txtMemberName.Enabled = false;
            txtEmail.Enabled = false;
            txtCity.Enabled = false;
            txtCountry.Enabled = false;
        }

        private void LoadMembers(List<MemberObject> members)
        {
            try
            {
                DataTable memberObjectsTb = new DataTable();
                memberObjectsTb.Columns.Add("MemberID", typeof(int));
                memberObjectsTb.Columns.Add("Member Name", typeof(string));
                memberObjectsTb.Columns.Add("Member Email", typeof(string));
                memberObjectsTb.Columns.Add("Member City", typeof(string));
                memberObjectsTb.Columns.Add("Member Country", typeof(string));

                foreach (MemberObject member in members)
                {
                    memberObjectsTb.Rows.Add(member.GetMemberId(), member.GetMemberName(), member.GetEmail(), member.GetCity(), member.GetCountry());
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
                } catch (Exception ex)
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
            const string REGEX_PASSWORD = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,20}$";
            int memberId = int.Parse(txtMemberId.Text);
            string newPassword = txtPassword.Text;
            if (!Regex.IsMatch(newPassword, REGEX_PASSWORD))
            {
                MessageBox.Show(
                    "1.Password must contain at least one digit [0-9].\r\n" +
                    "2.Password must contain at least one lowercase Latin character [a-z].\r\n" +
                    "3.Password must contain at least one uppercase Latin character [A-Z].\r\n" +
                    "4.Password must contain at least one special character like # ? ! @ $ % ^ & * - .\r\n" +
                    "5.Password must contain a length of at least 8 characters and a maximum of 20 characters."
                );
            } else if (memberRepository.SavePassword(memberId, newPassword)) {
                MessageBox.Show("Save Password Success.");
            } else
            {
                MessageBox.Show("Save Password Fail.");
            }
        }
    }   
}
