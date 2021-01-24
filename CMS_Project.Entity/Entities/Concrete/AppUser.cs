using CMS_Project.Entity.Entities.Interfaces;
using CMS_Project.Entity.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS_Project.Entity.Entities.Concrete
{
    // IdentityUser => A class Microsoft has prepared for us, a class that provides us with many structures so that we can use it quickly in user-related operations. User Role offers ready-made structures for login registrition operations. Since this class contains the "Id" column of its own tables, we did not print Id to our entities from the IBaseEntity.cs interface as you are used to.
    public class AppUser: IdentityUser, IBaseEntity
    {

        // Features that the IdentityUser class does not contain but need in the project can be added here.
        public string Occupation { get; set; }

        private DateTime _createDate = DateTime.Now;
        public DateTime CreateDate { get => _createDate; set => _createDate = value; }

        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        private Status _status = Status.Active;
        public Status Status { get => _status; set => _status = value; }
    }
}
