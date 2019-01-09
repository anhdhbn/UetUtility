using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using UetGrade.Models;
using UetGrade.Modules.MConnectorSg;
using UetGrade.Modules.MGrade;
using UetGrade.Modules.MSystemConfig;
using UetGrade.RequestUtility;
using UetGrade.UCommon;

namespace UetGrade.BackgoundThread
{
    public class GradeThread : BackgroundService
    {
        private IGradeService gradeService;
        private IConnectorSgService connectorSgService;
        private ISystemConfigService  systemConfig;
        public GradeThread(IGradeService gradeService, ISystemConfigService systemConfig, IConnectorSgService connectorSgService)
        {
            this.gradeService = gradeService;
            this.systemConfig = systemConfig;
            this.connectorSgService = connectorSgService;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (true)
            {
                List<Grade> newGrades = new List<Grade>();
                RequestUtility.GradeRequest request = new RequestUtility.GradeRequest();
                var result = request.GetListGrade();
                
                if (result != null)
                {
                    if (result.Count == 0) continue;
                    var result2 = result.Select(x => new Grade()
                    {
                        Name = x[0],
                        Code = x[1],
                        Link = x[2],
                        Time = x[3] == null ? DateTime.Parse("1970/01/01") : DateTime.Parse(x[3]),
                        Status = !string.IsNullOrEmpty(x[2])
                    }).ToList();
                    gradeService.Update(result2);
                    var currentTime = systemConfig.GetCurrentTime();
                    foreach (var grade in result2)
                    {
                        if(grade.Time > currentTime)
                        {
                            newGrades.Add(grade);
                        }
                    }
                    
                    Process(newGrades);
                    systemConfig.ChangeTime((DateTime)result2[0].Time);
                }
                Thread.Sleep(1 * 60 * 1000);
            }

        }

        public void Process(List<Grade> newGrades)
        {
            foreach (var grade in newGrades)
            {
                List<Student> students = connectorSgService.GetStudentsFromGrade(grade);
                foreach (var student in students)
                {
                    Atrribute a1 = new Atrribute()
                    {
                        Name = "namegrade",
                        Value = $"{grade.Name} ({grade.Code}) "
                    };
                    string link = $"http://112.137.129.30/viewgrade/{grade.Link}";
                    string LinkEncode = System.Net.WebUtility.UrlEncode(link);
                    string linkShorten = BitlyRequest.Get(LinkEncode);
                    Atrribute a2 = new Atrribute()
                    {
                        Name = "link",
                        Value = linkShorten.Trim()
                    };
                    ChatfuelRequest.Post(student.MessageId, "SendLinkGrade", null, a1, a2);
                }
            }
        }
    }
}
