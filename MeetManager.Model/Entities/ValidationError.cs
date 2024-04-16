using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetManager.Model
{
    public class ValidationError
    {
        public ValidationError(string msg) 
        { 
            Message = msg;
        }
        public string Message { get; set; }
        //public string ErrorNumber { get; set; }
        //public string ErrorName { get; set; }
    }
}
