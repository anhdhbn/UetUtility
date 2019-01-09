using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UetGrade.UCommon
{
    public interface ICommand { }
    public class BaseEntity
    {

        public List<string> Errors = new List<string>();

        public void AddError(string Value)
        {
            Errors.Add(Value);
        }

        public string ConvertToUnsign(string str)
        {
            if (string.IsNullOrEmpty(str))
                return null;
            string[] signs = new string[] {
                "aAeEoOuUiIdDyY",
                "áàạảãâấầậẩẫăắằặẳẵ",
                "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
                "éèẹẻẽêếềệểễ",
                "ÉÈẸẺẼÊẾỀỆỂỄ",
                "óòọỏõôốồộổỗơớờợởỡ",
                "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
                "úùụủũưứừựửữ",
                "ÚÙỤỦŨƯỨỪỰỬỮ",
                "íìịỉĩ",
                "ÍÌỊỈĨ",
                "đ",
                "Đ",
                "ýỳỵỷỹ",
                "ÝỲỴỶỸ"
            };
            for (int i = 1; i < signs.Length; i++)
            {
                for (int j = 0; j < signs[i].Length; j++)
                {
                    str = str.Replace(signs[i][j], signs[0][i - 1]);
                }
            }

            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                if (c.Equals(' ') || ('A' <= c && c <= 'Z') || ('a' <= c && c <= 'z') || c.Equals(',') || c.Equals('.') || ('0' <= c && c <= '9'))
                {
                    continue;
                }
                else
                {
                    str = str.Replace(c, ' ');
                }
            }
            return str;
        }

        public BaseEntity() { }
        public BaseEntity(Base obj)
        {
            Common<Base>.Copy(obj, this);
        }
    }


    public interface IFilterEntity
    {
        int Take { get; set; }
        int Skip { get; set; }
        string SortBy { get; set; }
        SortType? SortType { get; set; }
    }

    public class FilterEntity : IFilterEntity
    {
        public Guid? CurrentUserId;
        public int Take { get; set; }
        public int Skip { get; set; }
        public string SortBy { get; set; }
        public SortType? SortType { get; set; }
        public FilterEntity()
        {
            if (Take == 0) Take = 10;
            if (Skip == 0) Skip = 0;
            if (string.IsNullOrEmpty(SortBy)) SortBy = "Cx";
            if (!SortType.HasValue) SortType = UetGrade.UCommon.SortType.ASC;
        }
    }
    public enum SortType
    {
        NONE = 0,
        DESC = 1,
        ASC = 2
    }
}
