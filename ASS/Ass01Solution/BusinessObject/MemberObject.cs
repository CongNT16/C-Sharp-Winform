using System;

namespace BusinessObject
{
    public class MemberObject
    {
        private int memberId;
        private string memberName;
        private string email;
        private string password;
        private string city;
        private string country;
       
        public MemberObject()
        {

        }

        public MemberObject(int memberId, string memberName, string email, string password, string city, string country)
        {
            this.memberId = memberId;
            this.memberName = memberName;
            this.email = email;
            this.password = password;
            this.city = city;
            this.country = country;
        }

        public int GetMemberId() { return memberId; }
        public string GetMemberName() { return memberName; }
        public string GetEmail() { return email; }
        public string GetPassword() { return password; }
        public string GetCity() { return city; }
        public string GetCountry() { return country; }
        public void SetMemberId(int memberId)
        {
            this.memberId = memberId;
        }
        public void SetMemberName(string memberName)
        {
            this.memberName = memberName;
        }
        public void SetEmail(string email)
        {
            this.email = email;
        }
        public void SetPassword(string password)
        {
            this.password = password;
        }
        public void SetCity(string city)
        {
            this.city = city;
        }
        public void SetCountry(string country)
        {
            this.country = country;
        }

        //override
        //public string ToString()
        //{
        //    return memberId + memberName;
        //}
    }
}
