using System;
using Microsoft.VisualBasic;

namespace ServerApp.DTO
{
    public class SellDomainDTO
    {
        public int DomainId{get;set;}
        public string DomainName{get;set;}
        public bool AvalibilityStatus{get;set;}
        public DateTime LastDateOfCheck{get;set;}
        public DateTime DateOfExpire{get;set;}
        
    }
}