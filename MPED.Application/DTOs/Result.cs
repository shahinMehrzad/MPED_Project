using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPED.Application.DTOs
{
    public class Result: IResult 
    {
        public Result()
        {
        }

        public Result(string message)
        {
            Succeeded = false;
            Message = message;
        }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }        
    }
}
