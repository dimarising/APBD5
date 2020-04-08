using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using CW.Middlewares;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CW.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            string path = "./requestsLog.txt";

            string s = "";

            s += "------------------------------------------------------\n";
            s += "Nazwa methody: " + httpContext.Request.Method + "\n";
            s += "Path: " + httpContext.Request.Path + "\n";
            s += "Body: " + httpContext.Request.ContentType + "\n";
            s += "Query: " + httpContext.Request.QueryString.Value + "\n";
            s += "------------------------------------------------------\n";


            Writer(path, s);
            await _next(httpContext);


        }

        private void Writer(string path, string s)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(s);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
