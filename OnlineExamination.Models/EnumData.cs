using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExamination.Models
{
    public class EnumData
    {
        public enum UserRole
        {
           UnKnown = 0, 
           Admin = 1,
           Jobseeker = 2
        }

        public enum CandidateLevel
        { 
          Fresher = 1,
          Senior = 2,
          Expert = 3
        }

        public enum CandiateResult
        { 
          Pass = 1,
          Fail = 2
        }
    }
}
