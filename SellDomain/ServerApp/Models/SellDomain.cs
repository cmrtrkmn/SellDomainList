using System;
using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models
{
    public class SellDomain
    {
        [Key]
        public int DomainId{get;set;}
        public string DomainName{get;set;}
        public bool AvalibilityStatus{get;set;}
        public DateTime LastDateOfCheck{get;set;}
        public DateTime DateOfExpire{get;set;}
        public string Secret {get;set;}
    }
}