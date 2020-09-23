using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication10.Model;
using WebApplication10.Models;

namespace WebApplication10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private ApplicationContext db;
        public ValuesController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        [Route("GetAll/{CmdText}")]
        public IEnumerable<Values> GetAllValues(string CmdText)
        {
            var hostname = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            if (CmdText != "")
            {
                return ValuesList(CmdText, hostname);
            }
            return new List<Values> { new Values { CmdText = "error" } };
        }
        public List<Values> ValuesList(string CmdText, string UserIp)
        {
            

            var prod = new List<Values>()
            {
                new Values() { CmdInput = CmdText, CmdText = runCommand(CmdText), CmdUser = UserIp}
            };

            var commands = db.Set<Values>();
           
            commands.Add(prod.FirstOrDefault());
            db.SaveChanges();
            return prod;
        }
        static string runCommand(string CmdText)
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = "/c " + CmdText;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                Console.WriteLine(output);
                string err = process.StandardError.ReadToEnd();
                Console.WriteLine(err);
                process.WaitForExit();
                if (err != "")
                {
                    return "<p style=' color: #FF2D00'>" + err + "</p>";
                }
                return "<p style=' color: #0BA014'>" + output + "</p>";
            }
            catch (Exception ex)
            { 
            }
            return "0" + "err";
        }
        [HttpGet]
        [Route("GetHistory")]
        public IEnumerable<Values> GetHistory(string UserIp)
        {
            var hostname = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            var commands = db.Set<Values>();
            

            List<Values> select = commands.Where(s => s.CmdUser == hostname).Select(s => s).ToList();

            return select;
        }

    }

}
