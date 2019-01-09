using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UetGrade.Modules.MConnectorSg;
using UetGrade.Modules.MGrade;

namespace UetGrade.Modules.MChatfuel
{
    [Route("api")]
    public class ChatfuelController : CommonController
    {
        

        private IConnectorSgService connectorSgService;
        private IGradeService gradeService;
        public ChatfuelController(IConnectorSgService connectorSgService, IGradeService gradeService)
        {
            this.connectorSgService = connectorSgService;
            this.gradeService = gradeService;
        }
        

        [Route("SaveStudent"), HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public void SaveStudent()
        {
            string MessageId = Request.Form["messenger user id"];
            string classes = Request.Form["MaHocPhan"];
            List<string> classes2 = classes.Split(',').ToList();
            connectorSgService.Update(MessageId, classes2.ToList());
            // save messid
            // nếu trùng update mã học phần
        }

        [Route("CheckClass"), HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public void CheckClass()
        {
            string MessageId = Request.Form["messenger user id"];
            string classes = Request.Form["MaHocPhan"];
            List<string> classes2 = classes.Split(',').ToList();
            List<string> result = new List<string>();
            List<string> success = new List<string>();
            List<string> failed = new List<string>();
            foreach (var grade in classes2)
            {
                result.Add(gradeService.GetName(grade));
            }
            bool flag = false; int i = 0;
            foreach (var item in result)
            {
                if (string.IsNullOrEmpty(item))
                {
                    failed.Add($"Không tìm thấy môn {classes2[i]}, vui lòng nhập đúng học phần" );
                    flag = true;
                }
                else
                {
                    success.Add($"{classes2[i]} - {item}");
                }
                i++;
            }
            if(flag)
            {
                // send lỗi
                RequestUtility.ChatfuelRequest.Post(MessageId, "SendMessToUser", "Có lỗi xảy ra");
                foreach (var f in failed)
                {
                    RequestUtility.ChatfuelRequest.Post(MessageId, "SendMessToUser", f);
                }

                RequestUtility.ChatfuelRequest.Post(MessageId, "ReceiveResultGrade");
            }
            else
            {
                foreach (var s in success)
                {
                    RequestUtility.ChatfuelRequest.Post(MessageId, "SendMessToUser", s);
                }
                //ConfirmGrade
                RequestUtility.ChatfuelRequest.Post(MessageId, "ConfirmGrade");
                // send confirm
            }
        }
    }
}
