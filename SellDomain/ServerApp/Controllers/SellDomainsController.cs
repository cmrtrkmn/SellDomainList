using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServerApp.Data;
using ServerApp.DTO;
using ServerApp.Models;
using SQLitePCL;

namespace ServerApp.Controllers
{
    //localhost:5001/api/SellDomain
    [ApiController]
    [Route("api/[controller]")]
    public class SellDomainsController : ControllerBase
    {
      private readonly SellDomainContext _context;
      public SellDomainsController(SellDomainContext context)
      {
        _context=context;
      }  

      [HttpGet]
      public async Task<ActionResult> GetDomains()
      {
        var domains=await _context
        .SellDomains
        .Select(d=>DomainToDTO (d))
        .ToListAsync();
        return Ok(domains);
      }
      [HttpGet("{domainName}")]
      public async Task<IActionResult> GetDomain(string domainName)
      {
        string url = "https://rdap.nicproxy.com/domain/";
        if(domainName!=null)
        {url=url+domainName;}
        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url); 
                if(response!=null&&response.IsSuccessStatusCode)   
                {response.EnsureSuccessStatusCode(); // HTTP hatalarını kontrol et
                string jsonResponse = await response.Content.ReadAsStringAsync();

                // JSON'u işle
                var responseObj = JsonConvert.DeserializeObject<RootObject>(jsonResponse);
                var lastChangedEvent = responseObj.events.Find(e => e.eventAction == "last changed");
                var expirationEvent = responseObj.events.Find(e => e.eventAction == "expiration");
                SellDomain d=new SellDomain();
                d.DomainId=0;
                d.DomainName=domainName;
                d.AvalibilityStatus=false;
                
                // Eğer bulunursa, değerleri yazdır
                if (lastChangedEvent != null)
                {
                  DateTime lastDateTime;
                  if(DateTime.TryParseExact(lastChangedEvent.eventDate,"yyyy-MM-dd HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out lastDateTime))
                    {d.LastDateOfCheck=lastDateTime;}
                }
                if (expirationEvent != null)
                {
                  DateTime expirationEventDateTime;
                  if(DateTime.TryParseExact(expirationEvent.eventDate,"yyyy-MM-dd HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out expirationEventDateTime))
                    {d.DateOfExpire=expirationEventDateTime;}
                }
                return Ok(DomainToDTO(d));
                }
                else
                {
                  return NotFound();
                }            
                
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"HTTP isteği sırasında hata oluştu: {e.Message}");
                return NotFound();
            }
        }
              
      } 
      // JSON'dan dönüşüm için sınıflar
      public class Event
      {
        public string eventAction { get; set; }
        public string eventDate { get; set; }
      }
      public class RootObject
      {
        public List<Event> events { get; set; }
      }
      [HttpPost]
      public async Task<IActionResult> CreateDomain(SellDomain entity)
      {
           _context.SellDomains.Add(entity);
           await _context.SaveChangesAsync();
           return CreatedAtAction(nameof(GetDomain),new {id=entity.DomainId},DomainToDTO(entity));
      }
      [HttpPut("{id}")]
      public async Task<IActionResult> UpdateDomain(int id,SellDomain entity)
      {
           if(id!=entity.DomainId)
           {
            return BadRequest();
           }
           var domain=await _context.SellDomains.FindAsync(id);
           if(domain==null)
           {
            return NotFound();
           }
           domain.DomainName=entity.DomainName;
           domain.AvalibilityStatus=entity.AvalibilityStatus;
           domain.DateOfExpire=entity.DateOfExpire;
           domain.LastDateOfCheck=entity.LastDateOfCheck;

           try{
            await _context.SaveChangesAsync();
           }
           catch(Exception e)
           {
            return NotFound();
           }
          return NoContent(); 
      }

      [HttpDelete("{id}")]
      public async Task<IActionResult> DeleteDomain(int id)
      {
        var domain=await  _context.SellDomains.FindAsync(id);

        if(domain==null)
        {
          NotFound();
        }
        _context.SellDomains.Remove(domain);
        await _context.SaveChangesAsync();
        return NoContent();
      }
      private static SellDomainDTO DomainToDTO(SellDomain d)
      {
        return new SellDomainDTO()
        {
          DomainName=d.DomainName,
          AvalibilityStatus=d.AvalibilityStatus,
          LastDateOfCheck=d.LastDateOfCheck,
          DateOfExpire=d.DateOfExpire
        };
      }
    }
}
