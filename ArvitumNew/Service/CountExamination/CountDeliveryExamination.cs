using ArvitumNew.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace ArvitumNew.Service.CountExamination
{
    public class CountDeliveryExamination
    {
        public int Count { get; }
        public string Color { get; }
        private const int _examinationStatusId = 8;

        public CountDeliveryExamination(IHttpContextAccessor httpContextAccessor, ApplicationDbContext db)
        {
            try
            {
                string userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                int idWorkPlase = db.Users.FirstOrDefault(u => u.Id == userId).WorkPlaceId;

                IQueryable<Examination> examinations;
                examinations = db.Examinations.Where(e => e.Activ == true);
                examinations = examinations.Include(e => e.Сustomer);
                examinations = examinations.Include(e => e.Сustomer.WorkPlace);
                examinations = examinations.Where(e => e.Сustomer.WorkPlaceId == idWorkPlase);
                int? count = examinations.Where(e => e.ExaminationStatusId == _examinationStatusId).Count();

                string color="";
                if (count == null || count == 0)
                { color = "badge"; }
                else if(count >= 1 && count <= 5)
                { color = "badge badge-success"; }
                else if(count > 5 && count <= 10)
                { color = "badge badge-warning"; }
                else if (count >  10)
                { color = "badge badge-important"; }

                Count = count ?? 0;
                Color = color;
            }
            catch
            {
                Count = 0;
                Color = "badge";
            }
        }
    }
}
