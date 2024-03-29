//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PisDataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public User()
        {
            this.Session = new HashSet<Session>();
            this.Event = new HashSet<Event>();
            this.FriendsOf = new HashSet<User>();
            this.FriendsFrom = new HashSet<User>();
            this.ShareWith = new HashSet<Share>();
            this.ShareFrom = new HashSet<Share>();
        }
    
        public int Id { get; set; }
        public string FacebookId { get; set; }
        public string LinkedInId { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Session> Session { get; set; }
        public virtual UserPosition UserPosition { get; set; }
        public virtual ICollection<Event> Event { get; set; }
        public virtual ICollection<User> FriendsOf { get; set; }
        public virtual ICollection<User> FriendsFrom { get; set; }
        public virtual ICollection<Share> ShareWith { get; set; }
        public virtual ICollection<Share> ShareFrom { get; set; }
    }
}
