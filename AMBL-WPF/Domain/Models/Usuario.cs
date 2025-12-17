using System;
using System.Collections.Generic;
using System.Text;

namespace AMBL_WPF.Domain.Models
{
    public class Usuario
    {
        public int Id { get; set; }           
        public string Name { get; set; }    
        public string Password { get; set; }   
        public string Email { get; set; }      
    }
}

