using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IMemberRepository
    {
        List<MemberObject> GetMemberObjects();
        MemberObject GetMemberObjectById(int memberObjectId);
        MemberObject Authenticate(string email, string password);
        List<MemberObject> GetMemberObjectByName(string name);
        List<MemberObject> GetMemberObjectByCity(string city);
        List<MemberObject> GetMemberObjectByCountry(string country);
        List<MemberObject> GetMemberObjectByNameDescending();

        bool SavePassword(int memberId, string password);
    }
}