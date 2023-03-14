using BusinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DataAccess
{
    public class MemberDAO
    {
        private static List<MemberObject> memberObjects = new List<MemberObject>()
        {
            new MemberObject(1, "Quy Quoc", "quocpqhe163061@fpt.edu.vn", "Shironeko02", "Hanoi", "VietNam"),
            new MemberObject(2, "Duc Anh", "anhpdhs163432@fpt.edu.vn", "Anhpd01", "HCM", "VietNam"),
            new MemberObject(3, "Thanh Cong", "congnthe123213@fpt.edu.vn", "Congno1", "Hanoi", "VietNam"),
            new MemberObject(4, "Peter ", "peterparker@harvard.us", "p123456", "Cambridge", "US"),
            new MemberObject(5, "Jame Bone ", "1", "1", "Cambridge", "US"),
            new MemberObject(6, "My Mieu", "MieuMhs163011@fpt.edu.vn", "MM123456", "Hanoi", "VietNam")
        };

        //Use singleton
        private static MemberDAO instance = null;
        private static readonly object instanceLock = new object();
        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }

        public List<MemberObject> GetMemberObjects => memberObjects; 

        public MemberObject GetMemberObjectById(int memberId)
        {
            MemberObject memberObject = memberObjects.SingleOrDefault(pro => pro.GetMemberId() == memberId);
            if (memberObject == null)
            {
                throw new Exception("Not found any member!");
            }
            return memberObject;
        }

        public List<MemberObject> GetMemberObjectByName(string name) {
            List<MemberObject> memberObjectsByName = new List<MemberObject>();
            var result = from memberObject in memberObjects
                                  where memberObject.GetMemberName().ToLower().Contains(name.ToLower())
                                  select memberObject;
            foreach (var memberObject in result )
            {
                memberObjectsByName.Add(memberObject);
            }
            return memberObjectsByName;
        }

        public List<MemberObject> GetMemberObjectByCity(string city)
        {
            List<MemberObject> memberObjectsByCity = new List<MemberObject>();
            var result = from memberObject in memberObjects
                         where memberObject.GetCity().Equals(city)
                         select memberObject;
            foreach (var memberObject in result)
            {
                memberObjectsByCity.Add(memberObject);
            }
            if (memberObjectsByCity.Count == 0)
            {
                throw new Exception("Not found any member!");
            }
            return memberObjectsByCity;
        }

        public List<MemberObject> GetMemberObjectByCountry(string country)
        {
            List<MemberObject> memberObjectsByCountry = new List<MemberObject>();
            var result = from memberObject in memberObjects
                         where memberObject.GetCountry().Equals(country)
                         select memberObject;
            foreach (var memberObject in result)
            {
                memberObjectsByCountry.Add(memberObject);
            }
            if (memberObjectsByCountry.Count == 0)
            {
                throw new Exception("Not found any member!");
            }
            return memberObjectsByCountry;
        }

        public List<MemberObject> GetMemberObjectByNameDescending()
        {
            List<MemberObject> memberObjectsByName = new List<MemberObject>();
            var result = from memberObject in memberObjects
                         orderby memberObject.GetMemberName() descending
                         select memberObject;
            foreach (var memberObject in result)
            {
                memberObjectsByName.Add(memberObject);
            }
            if (memberObjectsByName.Count == 0)
            {
                throw new Exception("Not found any member!");
            }
            return memberObjectsByName;
        }

        public MemberObject Authenticate(string email, string password)
        {

            var result = from memberObject in memberObjects
                         where memberObject.GetEmail() == email
                         where memberObject.GetPassword() == password
                         select memberObject;
            MemberObject memberObjectAuth = result.FirstOrDefault();
            if (memberObjectAuth != null)
            {
                return memberObjectAuth;
            }
            else
            {
                throw new Exception("Email or password wrong");
            }
        }

        public bool SavePassword(int memberId, string password)
        {
            MemberObject member = GetMemberObjectById(memberId);
            member.SetPassword(password);
            return true;
        } 
    }
}


