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
    
    public partial class WhereNegationEvent : Event
    {
        public bool Sent { get; set; }
    
        public virtual WhereSolicitation WhereSolicitation { get; set; }
    }
}
