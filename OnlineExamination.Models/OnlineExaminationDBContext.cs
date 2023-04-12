using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnlineExamination.Models;


namespace OnlineExamination.Models
{
    public class OnlineExaminationDBContext : DbContext, IDisposable
    {
        public OnlineExaminationDBContext()
        {

        }

        public OnlineExaminationDBContext(DbContextOptions<OnlineExaminationDBContext> options) : base(options)
        {
        }

        public virtual DbSet<CandidateResult> CandidateResult { get; set; }
        public virtual DbSet<Answer> Answer { get; set; }
        public virtual DbSet<Questions> Questions { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<UserLogin> UserLogin { get; set; }
        public virtual DbSet<Technology> Technology { get; set; }
    }
}
