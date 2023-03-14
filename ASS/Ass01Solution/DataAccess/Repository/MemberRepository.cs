using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public List<MemberObject> GetMemberObjects()
        {
            return MemberDAO.Instance.GetMemberObjects;
        }

        public MemberObject Authenticate(string email, string password)
        {
            return MemberDAO.Instance.Authenticate(email, password);
        }

        public List<MemberObject> GetMemberObjectByCity(string city)
        {
            return MemberDAO.Instance.GetMemberObjectByCity(city);
        }

        public List<MemberObject> GetMemberObjectByCountry(string country)
        {
            return MemberDAO.Instance.GetMemberObjectByCountry(country);
        }

        public MemberObject GetMemberObjectById(int memberObjectId)
        {
            return MemberDAO.Instance.GetMemberObjectById(memberObjectId);
        }

        public List<MemberObject> GetMemberObjectByName(string name)
        {
            return MemberDAO.Instance.GetMemberObjectByName(name);
        }

        public List<MemberObject> GetMemberObjectByNameDescending()
        {
            return MemberDAO.Instance.GetMemberObjectByNameDescending();
        }

        public bool SavePassword(int memberId, string password)
        {
            return MemberDAO.Instance.SavePassword(memberId, password);
        }
    }
}
