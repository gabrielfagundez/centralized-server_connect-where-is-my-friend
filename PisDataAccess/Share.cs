//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PisDataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Share
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int UserId1 { get; set; }
        public System.DateTime Date { get; set; }
        public bool Active { get; set; }
    
        public virtual User UserWith { get; set; }
        public virtual User UserFrom { get; set; }
    }
}
